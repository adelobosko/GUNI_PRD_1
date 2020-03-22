using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public abstract class Operation
    {
        Guid ID { get; set; }
        string Name { get; set; }
        Version MechanicVersion { get; }

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