using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithExpert
    {
        Guid Expert { get; set; }
        ExpertsDto ExpertDto { get; set; }
    }
}