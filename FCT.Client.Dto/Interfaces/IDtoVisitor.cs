using FCT.Client.Dto.Consolidation;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoVisitor
    {
        void Visit(AltsDto dto);
        void Visit(SheetsCDto dto);
        void Visit(MarksCDto dto);
        void Visit(FinalMarksCDto dto);
        void Visit(ComplectsDto dto);
        void Visit(AnswersDto dto);
        void Visit(AppealsDto dto);
        void Visit(AppealTasksDto dto);
        void Visit(HumanTestsDto dto);
        void Visit(MarksDto dto);
        void Visit(PrnfCertificatePrintMainDto dto);
        void Visit(CurrentRegionDto dto);
        void Visit(AreasDto dto);
        void Visit(ExpertsDto dto);
        void Visit(GovernmentsDto dto);
        void Visit(ParticipantsDto dto);
        void Visit(SchoolsDto dto);
        void Visit(StationsDto dto);
    }
}
