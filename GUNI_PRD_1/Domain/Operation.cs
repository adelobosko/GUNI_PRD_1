using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public abstract class Operation
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Version MechanicVersion { get; protected set; }

        public virtual ControlOperationResult Execute(Elevator elevator)
        {
            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.EXECUTED,
                Messages = new List<string>() {$"BASE HANDLER: Operation {ID} {Name} is executed."}
            };
        }
    }
}