namespace GUNI_PRD_1
{
    public interface IControllable
    {
        ControlOperationResult OpenDoor();
        ControlOperationResult CloseDoor();
        ControlOperationResult MoveUp();
        ControlOperationResult MoveDown();
        ControlOperationResult Stop();
        ControlOperationResult CallHelper();
    }
}