using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public class Elevator : IMechanical
    {
        private int _currentFloor;
        private ElevatorControl _elevatorControl;

        public int CurrentFloor
        {
            get => _currentFloor;
            set
            {
                if (value < 1)
                {
                    _currentFloor = 1;
                }

                else if (value > MaxFloor)
                {
                    _currentFloor = MaxFloor;
                }
                else
                {
                    _currentFloor = value;
                }
            }
        }

        public readonly int MaxFloor;
        public string Name { get; set; }
        public bool IsDoorOpened { get; private set; }

        public Version MechanicVersion { get; }
        public int MasterPassword { get; private set; }


        public Elevator(string name, int maxFloor, Version mechanicVersion, string masterPassword = "12345")
        {
            _currentFloor = 1;
            this.Name = name;
            this.MaxFloor = maxFloor > 0 ? maxFloor : 1;
            this.MechanicVersion = mechanicVersion == null ? new Version("1.1.0.0") : mechanicVersion;
            _elevatorControl = null;
            MasterPassword = masterPassword.GetHashCode();

            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return
                $"Elevator {Name}, maxFloors {MaxFloor}, mechanicVersion {MechanicVersion}, passwordHash {MasterPassword}";
        }

        public void CallSpecialist()
        {
            Console.WriteLine("The specialist is called. Wait for a while!");
        }


        public ControlOperationResult SetController(ElevatorControl elevatorControl)
        {
            if (elevatorControl != null)
            {
                _elevatorControl = elevatorControl;
                _elevatorControl.Elevator = this;

                return new ControlOperationResult()
                {
                    Status = ControlOperationStatus.EXECUTED,
                    Messages = new List<string>()
                    {
                        $"Controller {elevatorControl.ModelName} is set into elevator {this.Name}"
                    }
                };
            }

            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.DECLINED,
                Messages = new List<string>()
                {
                    "Controller is not found"
                }
            };
        }

        public ControlOperationResult OpenDoor()
        {
            if (_elevatorControl != null) return _elevatorControl.OpenDoor();

            CallSpecialist();
            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.DECLINED,
                Messages = new List<string>() {"Control is missed. Calling a specialist!"}
            };
        }

        public ControlOperationResult CloseDoor(ElevatorControl elevatorControl)
        {
            if (_elevatorControl != null) return _elevatorControl.CloseDoor();

            CallSpecialist();
            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.DECLINED,
                Messages = new List<string>() { "Control is missed. Calling a specialist!" }
            };
        }

        public ControlOperationResult MoveUp(ElevatorControl elevatorControl)
        {
            if (_elevatorControl != null) return _elevatorControl.MoveUp();

            CallSpecialist();
            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.DECLINED,
                Messages = new List<string>() { "Control is missed. Calling a specialist!" }
            };
        }

        public ControlOperationResult MoveDown(ElevatorControl elevatorControl)
        {
            if (_elevatorControl != null) return _elevatorControl.MoveDown();

            CallSpecialist();
            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.DECLINED,
                Messages = new List<string>() { "Control is missed. Calling a specialist!" }
            };
        }

        public ControlOperationResult Stop(ElevatorControl elevatorControl)
        {
            if (_elevatorControl != null) return _elevatorControl.Stop();

            CallSpecialist();
            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.DECLINED,
                Messages = new List<string>() { "Control is missed. Calling a specialist!" }
            };
        }

        public ControlOperationResult CallHelper(ElevatorControl elevatorControl)
        {
            if (_elevatorControl != null) return _elevatorControl.CallHelper();

            CallSpecialist();
            return new ControlOperationResult()
            {
                Status = ControlOperationStatus.DECLINED,
                Messages = new List<string>() { "Control is missed. Calling a specialist!" }
            };
        }

        private ControlOperationResult MechanicalMoveDown()
        {
            var result = new ControlOperationResult();
            if (CurrentFloor <= 1)
            {
                result.Status = ControlOperationStatus.DECLINED;
                result.Messages.Add($"Can not move down. Elevator is on {CurrentFloor} floor.");
            }
            else
            {
                CurrentFloor--;
                result.Status = ControlOperationStatus.EXECUTED;
                result.Messages.Add($"Elevator is moved down. Elevator is on {CurrentFloor} floor.");
            }

            return result;
        }

        ControlOperationResult IMechanical.MechanicalMoveDown()
        {
            return MechanicalMoveDown();
        }


        private ControlOperationResult MechanicalMoveUp()
        {
            var result = new ControlOperationResult();
            if (CurrentFloor >= MaxFloor)
            {
                result.Status = ControlOperationStatus.DECLINED;
                result.Messages.Add($"Can not move up. Elevator is on {CurrentFloor} floor.");
            }
            else
            {
                CurrentFloor++;
                result.Status = ControlOperationStatus.EXECUTED;
                result.Messages.Add($"Elevator is moved up. Elevator is on {CurrentFloor} floor.");
            }

            return result;
        }

        ControlOperationResult IMechanical.MechanicalMoveUp()
        {
            return MechanicalMoveUp();
        }


        private ControlOperationResult MechanicalCloseDoor()
        {
            var result = new ControlOperationResult();
            if (!IsDoorOpened)
            {
                result.Status = ControlOperationStatus.DECLINED;
                result.Messages.Add($"Doors are already closed.");
            }
            else
            {
                IsDoorOpened = false;
                result.Status = ControlOperationStatus.EXECUTED;
                result.Messages.Add($"Doors are closed.");
            }

            return result;
        }

        ControlOperationResult IMechanical.MechanicalCloseDoor()
        {
            return MechanicalCloseDoor();
        }


        private ControlOperationResult MechanicalOpenDoor()
        {
            var result = new ControlOperationResult();
            if (IsDoorOpened)
            {
                result.Status = ControlOperationStatus.DECLINED;
                result.Messages.Add($"Doors are already opened.");
            }
            else
            {
                IsDoorOpened = true;
                result.Status = ControlOperationStatus.EXECUTED;
                result.Messages.Add($"Doors are opened.");
            }

            return result;
        }

        ControlOperationResult IMechanical.MechanicalOpenDoor()
        {
            return MechanicalOpenDoor();
        }


        private ControlOperationResult MechanicalCallHelper()
        {
            var result = new ControlOperationResult();
            result.Status = ControlOperationStatus.EXECUTED;
            result.Messages.Add($"Helper is called.");
            return result;
        }

        ControlOperationResult IMechanical.MechanicalCallHelper()
        {
            return MechanicalCallHelper();
        }


        private ControlOperationResult MechanicalStop()
        {
            var result = new ControlOperationResult();
            result.Status = ControlOperationStatus.EXECUTED;
            result.Messages.Add($"Elevator is extra stopped.");
            return result;
        }

        ControlOperationResult IMechanical.MechanicalStop()
        {
            return MechanicalStop();
        }
    }
}