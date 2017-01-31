using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithAuditorium
    {
        Guid Station { get; set; }
        string AuditoriumCode { get; set; }
        AuditoriumsDto Dirty_Auditorium { get; }
        AuditoriumsDto AuditoriumDto { get; set; }
        AuditoriumSurrogateKey AuditoriumSurrogateKey { get; }
    }
}