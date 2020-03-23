using System;
using System.Collections.Generic;
using System.Transactions;

namespace GUNI_PRD_1
{
    public class MenuObject
    {
        public string Description { get; set; }
        public object Object { get; set; }
        public Type Type { get; set; }
    }

    public class MenuItem
    {
        public List<MenuItem> Items { get; set; }
        public string Name { get; set; }
        public Action<Menu, object, Type> Action { get; set; }

        public MenuObject MenuObject { get; set; }

        public MenuItem()
        {
            Items = new List<MenuItem>();
            MenuObject = new MenuObject();
        }
    }

    public class Menu
    {
        public List<MenuItem> Items { get; set; }

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public void ShowInConsole()
        {
            var selectedOperation = "";
            MenuItem menuItem;

        startShowInConsoleLabel:
            Console.Clear();
            Console.WriteLine($"Select option:");
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"  {i} - {Items[i].Name}");
            }
            Console.WriteLine($"  e - Close program");
            try
            {
                selectedOperation = Console.ReadLine();
                if (selectedOperation == "e")
                {
                    return;
                }
                menuItem = Items[Convert.ToInt32(selectedOperation)];
                menuItem.Action.Invoke(this, Items[Convert.ToInt32(selectedOperation)], typeof(MenuItem));
            }
            catch (Exception ex)
            {
                Console.WriteLine($" *** Can not execute {selectedOperation} operation id.");
                Console.WriteLine($" * Press any key to continue...");
                Console.ReadKey();
            }
            goto startShowInConsoleLabel;
        }

        public override string ToString()
        {
            return "";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu()
            {
                Items = new List<MenuItem>()
                {
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
                                    var selectedOperation = "";

                                    selectControlLabel:
                                    Console.Clear();
                                    Console.WriteLine($"What control you want to set into {elevator.Name} eleavtor?");
                                    for (var i = 0; i < elevatorControls.Count; i++)
                                    {
                                        Console.WriteLine($"  {i} - {elevatorControls[i].ModelName}");
                                    }
                                    Console.WriteLine("  b - Back");
                                    selectedOperation = Console.ReadLine();
                                    if (selectedOperation == "b")
                                    {
                                        return;
                                    }
                                    try
                                    {
                                        elevatorControl = elevatorControls[Convert.ToInt32(selectedOperation)];
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
                            var selectedOperation = "";
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
                            selectedOperation = Console.ReadLine();
                            if (selectedOperation == "b")
                            {
                                return;
                            }
                            try
                            {
                                elevator = listElevators[Convert.ToInt32(selectedOperation)];

                                startElevatorOperationsLabel:
                                Console.Clear();
                                Console.WriteLine($"{elevator.Name} is selected. Choose the operation:");
                                for (var i = 0; i < menuItem.Items.Count; i++)
                                {
                                    Console.WriteLine($"  {i} - {menuItem.Items[i].Name}");
                                }
                                Console.WriteLine("  b - Back");
                                selectedOperation = Console.ReadLine();
                                if (selectedOperation == "b")
                                {
                                    return;
                                }

                                try
                                {
                                    menuItem.Items[Convert.ToInt32(selectedOperation)].Action.Invoke(@this, elevator, typeof(Elevator));
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($" *** Can not execute {selectedOperation} operation id.");
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                goto startElevatorOperationsLabel;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Can not execute {selectedOperation} operation id.");
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
                    new MenuItem()
                    {
                        Name = "Create elevator:",
                        Items = new List<MenuItem>(),
                        Action = delegate(Menu @this, object obj, Type type)
                        {
                            var menuItem = (MenuItem)Convert.ChangeType(obj, type);
                            var selectedOperation = "";
                            var elevatorName = "";
                            var elevatorMaxFloor = 1;
                            Version elevatorMechanicVersion;
                            var elevatorPassword = "";


                            inputElevatorNameLabel:
                            Console.Clear();
                            Console.WriteLine("Input elevator name:");
                            elevatorName = Console.ReadLine();

                            inputElevatorMaxFloorLabel:
                            Console.Clear();
                            Console.WriteLine("Input elevator max floor:");
                            try
                            {
                                elevatorMaxFloor = Convert.ToInt32(Console.ReadLine());
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
                            Console.WriteLine("Input elevator mechanic version 1.1.0.0 as standard:");
                            try
                            {
                                elevatorMechanicVersion = new Version(Console.ReadLine());
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
                            Console.WriteLine("Input elevator password:");
                            try
                            {
                                elevatorPassword = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Write correct version. Example: 1.1.0.0");
                                Console.WriteLine($" * Press any key to continue...");
                                Console.ReadKey();
                                goto inputElevatorPasswordLabel;
                            }

                            var elevator = new Elevator(elevatorName, elevatorMaxFloor, elevatorMechanicVersion, elevatorPassword);

                            ((List<Elevator>)@this.Items[0].MenuObject.Object).Add(elevator);

                            Console.WriteLine($"Created {elevator}");
                        },
                        MenuObject = new MenuObject()
                    },
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
                            var selectedOperation = "";
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
                            selectedOperation = Console.ReadLine();
                            if (selectedOperation == "b")
                            {
                                return;
                            }
                            try
                            {
                                elevatorControl = listElevatorControls[Convert.ToInt32(selectedOperation)];

                                elevatorControlOperationsLabel:
                                Console.Clear();
                                Console.WriteLine($"{elevatorControl.ModelName} is selected. Choose the operation:");
                                for (var i = 0; i < menuItem.Items.Count; i++)
                                {
                                    Console.WriteLine($"  {i} - {menuItem.Items[i].Name}");
                                }
                                Console.WriteLine("  b - Back");
                                selectedOperation = Console.ReadLine();
                                if (selectedOperation == "b")
                                {
                                    return;
                                }

                                try
                                {
                                    menuItem.Items[Convert.ToInt32(selectedOperation)].Action.Invoke(@this, elevatorControl, typeof(ElevatorControl));
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($" *** Can not execute {selectedOperation} operation id.");
                                    Console.WriteLine($" * Press any key to continue...");
                                    Console.ReadKey();
                                }
                                goto elevatorControlOperationsLabel;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" *** Can not execute {selectedOperation} operation id.");
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

            
            
            Console.ReadKey();
        }
    }
}
