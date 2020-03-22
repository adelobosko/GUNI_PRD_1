using System;

namespace GUNI_PRD_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var keypadElevatorControl = new KeypadElevatorControl("Noname and company", "next level 2000", DateTime.Now, null);

            Console.WriteLine(keypadElevatorControl.MoveUp());

            // same
            // var oldElevator = new Elevator("Old", 5, keypadElevatorControl, null);
            var oldElevator = new Elevator("Old", 5, new Version("1.1.0.0"));
            Console.WriteLine(oldElevator.MoveUp(keypadElevatorControl));

            Console.WriteLine(oldElevator.SetController(keypadElevatorControl));

            Console.WriteLine(oldElevator.MoveUp(keypadElevatorControl));
            Console.WriteLine(keypadElevatorControl.MoveUp());

            Console.ReadKey();
        }
    }
}
