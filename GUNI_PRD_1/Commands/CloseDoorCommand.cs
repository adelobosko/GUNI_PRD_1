using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public class CloseDoorCommand : Operation
    {
        public CloseDoorCommand()
        {
            ID = Guid.NewGuid();
            Name = this.GetType().FullName;
            MechanicVersion = new Version("1.1.0.0");
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

            return ((IMechanical)elevator).MechanicalCloseDoor();
        }
    }
}