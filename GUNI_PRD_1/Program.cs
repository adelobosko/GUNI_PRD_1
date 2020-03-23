using System;
using System.Collections.Generic;
using System.Transactions;

namespace GUNI_PRD_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu()
            {
                Items = new List<MenuItem>()
                {
                    //Elevators
                    new MenuItem()
                    {
                        Name = "Select elevator:",
                        Items = new List<MenuItem>()
                        {
                            new MenuItem()
                            {
                                Name = "GetInfo",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }

                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    Console.WriteLine(elevator);
                                }
                            },
                            new MenuItem()
                            {
                                Name = "SetController",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }
                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    ElevatorControl elevatorControl;
                                    var elevatorControls = (List<ElevatorControl>)@this.Items[2].MenuObject.Object;
                                    var input = "";

                                    selectControlLabel:
                                    Console.Clear();
                                    Console.WriteLine($"What control you want to set into {elevator.Name} eleavtor?");
                                    for (var i = 0; i < elevatorControls.Count; i++)
                                    {
                                        Console.WriteLine($"  {i} - {elevatorControls[i].ModelName}");
                                    }
                                    Console.WriteLine("  b - Back");
                                    input = Console.ReadLine();
                                    if (input == "b")
                                    {
                                        return;
                                    }
                                    try
                                    {
                                        elevatorControl = elevatorControls[Convert.ToInt32(input)];
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($" *** Select right variant!");
                                        Console.WriteLine($" * Press any key to continue...");
                                        Console.ReadKey();
                                        goto selectControlLabel;
                                    }
                                    
                                    Console.WriteLine(elevator.SetController(elevatorControl));
                                }
                            },
                            new MenuItem()
                            {
                                Name = "OpenDoor",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }

                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    Console.WriteLine(elevator.OpenDoor());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "CloseDoor",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }

                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    Console.WriteLine(elevator.CloseDoor());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "MoveUp",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }

                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    Console.WriteLine(elevator.MoveUp());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "MoveDown",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }

                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    Console.WriteLine(elevator.MoveDown());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "Stop",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }

                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    Console.WriteLine(elevator.Stop());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "CallHelper",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(Elevator).Name)
                                    {
                                        Console.WriteLine("It is not elevator type!");
                                    }

                                    var elevator = (Elevator)Convert.ChangeType(obj, type);
                                    Console.WriteLine(elevator.CallHelper());
                                }
                            }
                        },
                        Action = delegate(Menu @this, object obj, Type type)
                        {
                            var menuItem = (MenuItem)Convert.ChangeType(obj, type);
                            var input = "";
                            Elevator elevator;
                            var listElevators = (List<Elevator>)Convert.ChangeType(menuItem.MenuObject.Object, menuItem.MenuObject.Type);

                            startShowElevatorsLabel:
                            Console.Clear();
                            Console.WriteLine("Select elevator:");
                            for (var i = 0; i < listElevators.Count; i++)
                            {
                                Console.WriteLine($"  {i} - {listElevators[i].Name}");
                            }

                            Console.WriteLine("  b - Back");
                            input = Console.ReadLine();
                            if (input == "b")
                            {
                                return;
                            }
                            try
                            {
                                elevator = listElevators[Convert.ToInt32(input)];

                                startElevatorOperationsLabel:
                                Console.Clear();
                                Console.WriteLine($"{elevator.Name} is selected. Choose the operation:");
                                for (var i = 0; i < menuItem.Items.Count; i++)
                                {
                                    Console.WriteLine($"  {i} - {menuItem.Items[i].Name}");
                                }
                                Console.WriteLine("  b - Back");
                                input = Console.ReadLine();
                                if (input == "b")
                                {
                                    return;
                                }

                                try
                                {
                                    menuItem.Items[Convert.ToInt32(input)].Action.Invoke(@this, elevator, typeof(Elevator));
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($" *** Can not execute {input} operation id.");
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                goto startElevatorOperationsLabel;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Can not execute {input} operation id.");
                                Console.WriteLine($" * Press any key to continue...");
                                Console.ReadKey();
                            }
                            goto startShowElevatorsLabel;
                        },
                        MenuObject = new MenuObject()
                        {
                            Description = "Elevators",
                            Type = typeof(List<Elevator>),
                            Object = new List<Elevator>()
                        }
                    },
                    //Create elevator
                    new MenuItem()
                    {
                        Name = "Create elevator:",
                        Items = new List<MenuItem>(),
                        Action = delegate(Menu @this, object obj, Type type)
                        {
                            var menuItem = (MenuItem)Convert.ChangeType(obj, type);
                            var input = "";
                            var elevatorName = "";
                            var elevatorMaxFloor = 1;
                            Version elevatorMechanicVersion;
                            var elevatorPassword = "";


                            inputElevatorNameLabel:
                            Console.Clear();
                            Console.WriteLine("  b - Back\r\nInput elevator name:");
                            input = Console.ReadLine();
                            if (input == "b")
                            {
                                return;
                            }
                            elevatorName = input;

                            inputElevatorMaxFloorLabel:
                            Console.Clear();
                            Console.WriteLine("  b - Back\r\nInput elevator max floor:");
                            input = Console.ReadLine();
                            if (input == "b")
                            {
                                return;
                            }
                            try
                            {
                                elevatorMaxFloor = Convert.ToInt32(input);
                                if (elevatorMaxFloor < 1 || elevatorMaxFloor > 100)
                                {
                                    Console.WriteLine($" *** Write correct floor. (1-100)");
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                    goto inputElevatorMaxFloorLabel;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Write correct floor. (1-100)");
                                Console.WriteLine($" * Press any key to continue...");
                                Console.ReadKey();
                                goto inputElevatorMaxFloorLabel;
                            }

                            inputElevatorVersionLabel:
                            Console.Clear();
                            Console.WriteLine("  b - Back\r\nInput elevator mechanic version 1.1.0.0 as standard:");
                            input = Console.ReadLine();
                            if (input == "b")
                            {
                                return;
                            }
                            try
                            {
                                elevatorMechanicVersion = new Version(input);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Write correct version. Example: 1.1.0.0");
                                Console.WriteLine($" * Press any key to continue...");
                                Console.ReadKey();
                                goto inputElevatorVersionLabel;
                            }

                            inputElevatorPasswordLabel:
                            Console.Clear();
                            Console.WriteLine("  b - Back\r\nInput elevator password:");
                            input = Console.ReadLine();
                            if (input == "b")
                            {
                                return;
                            }
                            elevatorPassword = input;

                            var elevator = new Elevator(elevatorName, elevatorMaxFloor, elevatorMechanicVersion, elevatorPassword);

                            ((List<Elevator>)@this.Items[0].MenuObject.Object).Add(elevator);

                            Console.WriteLine($"Created {elevator}");
                        },
                        MenuObject = new MenuObject()
                    },
                    //Controls
                    new MenuItem()
                    {
                        Name = "Select control:",
                        Items = new List<MenuItem>()
                        {
                            new MenuItem()
                            {
                                Name = "GetInfo",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl) obj;
                                    Console.WriteLine(elevatorControl);
                                    switch (elevatorControl)
                                    {
                                        case RemoteElevatorControl _:
                                            Console.WriteLine($"Control type {typeof(RemoteElevatorControl).Name}");
                                            break;
                                        case KeypadElevatorControl _:
                                            Console.WriteLine($"Control type {typeof(KeypadElevatorControl).Name}");
                                            break;
                                    }
                                }
                            },
                            new MenuItem()
                            {
                                Name = "InputPassword",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl)Convert.ChangeType(obj, type);
                                    if (elevatorControl is RemoteElevatorControl remoteControl)
                                    {
                                        Console.WriteLine("Input password:");
                                        Console.WriteLine(remoteControl.InputPassword(Console.ReadLine()));
                                        return;
                                    }
                                    Console.WriteLine("Elevator control is not support this operation");
                                }
                            },
                            new MenuItem()
                            {
                                Name = "OpenDoor",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl)obj;
                                    Console.WriteLine(elevatorControl.OpenDoor());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "CloseDoor",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl)obj;
                                    Console.WriteLine(elevatorControl.CloseDoor());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "MoveUp",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl)obj;
                                    Console.WriteLine(elevatorControl.MoveUp());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "MoveDown",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl) obj;
                                    Console.WriteLine(elevatorControl.MoveDown());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "Stop",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl) obj;
                                    Console.WriteLine(elevatorControl.Stop());
                                }
                            },
                            new MenuItem()
                            {
                                Name = "CallHelper",
                                Items = new List<MenuItem>(),
                                MenuObject = null,
                                Action = delegate(Menu @this, object obj, Type type)
                                {
                                    if (type.Name != typeof(ElevatorControl).Name)
                                    {
                                        Console.WriteLine("It is not elevatorControl type!");
                                    }

                                    var elevatorControl = (ElevatorControl) obj;
                                    Console.WriteLine(elevatorControl.CallHelper());
                                }
                            }
                        },
                        Action = delegate(Menu @this, object obj, Type type)
                        {
                            var menuItem = (MenuItem)Convert.ChangeType(obj, type);
                            var input = "";
                            ElevatorControl elevatorControl;
                            var listElevatorControls = (List<ElevatorControl>)Convert.ChangeType(menuItem.MenuObject.Object, menuItem.MenuObject.Type);

                            startShowElevatorsLabel:
                            Console.Clear();
                            Console.WriteLine("Select elevator:");
                            for (var i = 0; i < listElevatorControls.Count; i++)
                            {
                                Console.WriteLine($"  {i} - {listElevatorControls[i].ModelName}");
                            }

                            Console.WriteLine("  b - Back");
                            input = Console.ReadLine();
                            if (input == "b")
                            {
                                return;
                            }
                            try
                            {
                                elevatorControl = listElevatorControls[Convert.ToInt32(input)];

                                elevatorControlOperationsLabel:
                                Console.Clear();
                                Console.WriteLine($"{elevatorControl.ModelName} is selected. Choose the operation:");
                                for (var i = 0; i < menuItem.Items.Count; i++)
                                {
                                    Console.WriteLine($"  {i} - {menuItem.Items[i].Name}");
                                }
                                Console.WriteLine("  b - Back");
                                input = Console.ReadLine();
                                if (input == "b")
                                {
                                    return;
                                }

                                try
                                {
                                    menuItem.Items[Convert.ToInt32(input)].Action.Invoke(@this, elevatorControl, typeof(ElevatorControl));
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($" *** Can not execute {input} operation id.");
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                goto elevatorControlOperationsLabel;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Can not execute {input} operation id.");
                                Console.WriteLine($" * Press any key to continue...");
                                Console.ReadKey();
                            }
                            goto startShowElevatorsLabel;
                        },
                        MenuObject = new MenuObject()
                        {
                            Description = "Controls",
                            Type = typeof(List<ElevatorControl>),
                            Object = new List<ElevatorControl>()
                        }
                    },
                    //Create control
                    new MenuItem()
                    {
                        Name = "Create control:",
                        Items = new List<MenuItem>(),
                        Action = delegate(Menu @this, object obj, Type type)
                        {
                            var menuItem = (MenuItem)Convert.ChangeType(obj, type);
                            var input = "";
                            ElevatorControl elevatorControl;
                            var elevatorControlTypes = new[]
                            {
                                typeof(RemoteElevatorControl).Name,
                                typeof(KeypadElevatorControl).Name
                            };
                            var elevatorControlType = "";
                            var elevatorControlModelName = "";
                            Elevator elevator = null;
                            var elevatorControlPassword = "";


                            inputElevatorControlTypeLabel:
                            Console.Clear();
                            Console.WriteLine("Input elevator control type:");
                            for (var i = 0; i < elevatorControlTypes.Length; i++)
                            {
                                Console.WriteLine($"  {i} - {elevatorControlTypes[i]}");
                            }
                            Console.WriteLine($"  b - Back");
                            input = Console.ReadLine();
                            if (input == "b")
                            {
                                return;
                            }
                            try
                            {
                                elevatorControlType = elevatorControlTypes[Convert.ToInt32(input)];
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Is not correct operation id");
                                Console.WriteLine($" * Press any key to continue...");
                                Console.ReadKey();
                                goto inputElevatorControlTypeLabel;
                            }


                            inputElevatorControlModelNameLabel:
                            Console.Clear();
                            Console.WriteLine("Input elevator control model name:");
                            elevatorControlModelName = Console.ReadLine();

                            inputElevatorControlPasswordLabel:
                            Console.Clear();
                            Console.WriteLine("Input elevator control password:");
                            elevatorControlPassword = Console.ReadLine();

                            if (typeof(RemoteElevatorControl).Name == elevatorControlType)
                            {
                                elevatorControl = new RemoteElevatorControl(elevatorControlModelName, DateTime.Now, null, elevatorControlPassword);
                            }
                            else
                            {
                                elevatorControl = new KeypadElevatorControl(elevatorControlModelName, DateTime.Now, null);
                            }

                            ((List<ElevatorControl>)@this.Items[2].MenuObject.Object).Add(elevatorControl);

                            Console.WriteLine($"Created {elevatorControl}");
                        },
                        MenuObject = new MenuObject()
                    }
                }
            };

            menu.ShowInConsole();
        }
    }
}
