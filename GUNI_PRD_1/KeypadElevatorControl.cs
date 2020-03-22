using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public class KeypadElevatorControl : ElevatorControl
    {
        public KeypadElevatorControl(string companyName, string modelName, DateTime releaseDate, Elevator elevator)
            : base(companyName, modelName, releaseDate, elevator)
        {
        }

        protected override ControlOperationResult ElevatorOperationHandler(Operation operation)
        {
            //Tracer.Log...
            if (Elevator == null)
            {
                return new ControlOperationResult()
                {
                    Status = ControlOperationStatus.DECLINED,
                    Messages = new List<string>()
                    {
                        "Control panel is not set into any elevator."
                    }
                };
            }

            return operation.Execute(this.Elevator);
        }

        public override ControlOperationResult OpenDoor()
        {
            return ElevatorOperationHandler(new OpenDoorCommand());
        }

        public override ControlOperationResult CloseDoor()
        {
            return ElevatorOperationHandler(new CloseDoorCommand());
        }

        public override ControlOperationResult MoveUp()
        {
            return ElevatorOperationHandler(new MoveUpCommand());
        }

        public override ControlOperationResult MoveDown()
        {
            return ElevatorOperationHandler(new MoveDownCommand());
        }

        public override ControlOperationResult Stop()
        {
            return ElevatorOperationHandler(new StopCommand());
        }

        public override ControlOperationResult CallHelper()
        {
            return ElevatorOperationHandler(new CallHelperCommand());
        }
    }
}