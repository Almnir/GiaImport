using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithStationExam
    {
        Guid Station { get; set; }
        int Exam { get; set; }
        StationsExamsDto StationExamDto { get; set; }
        StationsExamsDto Dirty_StationExam { get; }
    }
}