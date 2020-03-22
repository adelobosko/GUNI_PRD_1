﻿using System;

namespace GUNI_PRD_1
{
    public abstract class ElevatorControl : IControllable
    {
        public string ModelName { get; set; }
        public string CompanyName { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Elevator Elevator { get;  set; }
        
        protected ElevatorControl(string companyName, string modelName, DateTime releaseDate, Elevator elevator)
        {
            this.CompanyName = companyName;
            this.ModelName = modelName;
            this.ReleaseDate = releaseDate;
            this.Elevator = elevator;

            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"Elevator {ModelName} created by {CompanyName} in {ReleaseDate}";
        }


        protected abstract ControlOperationResult ElevatorOperationHandler(Operation operation);
        public abstract ControlOperationResult OpenDoor();
        public abstract ControlOperationResult CloseDoor();
        public abstract ControlOperationResult MoveUp();
        public abstract ControlOperationResult MoveDown();
        public abstract ControlOperationResult Stop();
        public abstract ControlOperationResult CallHelper();
    }
}