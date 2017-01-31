namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithWorkerPosition
    {
        int WorkerPositionCode { get; set; }
        SWorkerPositionsDto WorkerPositionDto { get; set; }
    }
}