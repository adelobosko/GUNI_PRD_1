using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public class RemoteElevatorControl : ElevatorControl
    {
        public int MasterPassword { get; private set; }

        public RemoteElevatorControl(string modelName, DateTime releaseDate, Elevator elevator = null, string masterPassword = "12345")
            : base(modelName, releaseDate, elevator)
        {
            MasterPassword = masterPassword.GetHashCode();
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

            if (Elevator.MasterPassword != MasterPassword)
            {
                return new ControlOperationResult()
                {
                    Status = ControlOperationStatus.DECLINED,
                    Messages = new List<string>()
                    {
                        "Access denied."
                    }
                };
            }

            return operation.Execute(this.Elevator);
        }

        public ControlOperationResult InputPassword(string masterPassword)
        {
            MasterPassword = masterPassword.GetHashCode();
            if (Elevator.MasterPassword != MasterPassword)
            {
                return new ControlOperationResult()
                {
                    Status = ControlOperationStatus.EXECUTED,
                    Messages = new List<string>()
                    {
                        "Access denied."
                    }
                };
            }

            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.EXECUTED,
                Messages = new List<string>()
                {
                    "Access allowed."
                }
            };
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