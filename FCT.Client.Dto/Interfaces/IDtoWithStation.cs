using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithStation
    {
        Guid Station { get; set; }
        StationsDto StationDto { get; set; }
    }
}