using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithSchool
    {
        Guid School { get; set; }
        SchoolsDto SchoolDto { get; set; }
    }
}