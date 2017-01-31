using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithStationWorker
    {
        Guid StationWorker { get; set; }
        StationWorkersDto StationWorkerDto { get; set; }
    }
}