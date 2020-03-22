using System;

namespace GUNI_PRD_1
{
    public interface IMechanical
    {
        Version MechanicVersion { get; }
        ControlOperationResult MechanicalMoveDown();
        ControlOperationResult MechanicalMoveUp();
        ControlOperationResult MechanicalOpenDoor();
        ControlOperationResult MechanicalCloseDoor();
        ControlOperationResult MechanicalStop();
        ControlOperationResult MechanicalCallHelper();
    }
}