using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithParticipantExam
    {
        ParticipantsExamsDto Dirty_ParticipantExam { get; }
        Guid Participant { get; set; }
        int Exam { get; set; }
        int Region { get; set; }
        ParticipantsExamsDto ParticipantExamDto { get; set; }
    }
}