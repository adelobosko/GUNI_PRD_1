﻿using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public class StopCommand : Operation
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public Version MechanicVersion { get; }

        public StopCommand()
        {
            ID = new Guid();
            Name = this.GetType().FullName;
            MechanicVersion = new Version("1.2.0.0");
        }

        public override ControlOperationResult Execute(Elevator elevator)
        {
            var baseResult = base.Execute(elevator);

            if (elevator.MechanicVersion < this.MechanicVersion)
            {
                return new ControlOperationResult()
                {
                    Status = ControlOperationStatus.DECLINED,
                    Messages = new List<string>(baseResult.Messages)
                    {
                        $"Elevator is not supported command \"{Name}\".",
                        $"Elevator version is \"{elevator.MechanicVersion}\".",
                        $"Command version is \"{this.MechanicVersion}\"."
                    }
                };
            }

            return ((IMechanical)elevator).MechanicalStop();
        }
    }
}