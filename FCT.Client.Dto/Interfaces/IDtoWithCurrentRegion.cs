using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithCurrentRegion
    {
        Guid CurrentRegion { get; set; }
        CurrentRegionDto CurrentRegionDto { get; set; }
    }
}