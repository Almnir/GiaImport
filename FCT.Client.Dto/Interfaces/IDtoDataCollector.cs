using FCT.Client.Dto.Consolidation;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoCollectorAccepter
    {
        void Collect(IDtoDataCollector collector);
    }

    public interface IDtoDataCollector
    {
        void Collect(AltsDto dto);
        void Collect(SheetsCDto dto);
        void Collect(MarksCDto dto);
        void Collect(FinalMarksCDto dto);
        void Collect(ComplectsDto dto);
        void Collect(AnswersDto dto);
        void Collect(AppealsDto dto);
        void Collect(AppealTasksDto dto);
        void Collect(HumanTestsDto dto);
        void Collect(MarksDto dto);
        void Collect(PrnfCertificatePrintMainDto dto);
        void Collect(CurrentRegionDto dto);
        void Collect(AreasDto dto);
        void Collect(ExpertsDto dto);
        void Collect(GovernmentsDto dto);
        void Collect(ParticipantsDto dto);
        void Collect(ParticipantsExamsDto dto);
        void Collect(ParticipantsExamsOnStationDto dto);
        void Collect(ParticipantPropertiesDto dto);
        void Collect(SchoolsDto dto);
        void Collect(StationsDto dto);
        void Collect(StationsExamsDto dto);
        void Collect(DatsBordersDto dto);
        void Collect(DatsGroupsDto dto);
        void Collect(AuditoriumsDto dto);
        void Collect(PlacesDto dto);
        void Collect(ParticipantsExamPlacesOnStationDto dto);
        void Collect(ExpertsExamsDto dto);
        void Collect(StationExamAuditoryDto dto);
        void Collect(StationWorkerOnExamDto dto);
        void Collect(StationWorkersDto dto);
        void Collect(StationWorkerOnStationDto dto);
    }
}
