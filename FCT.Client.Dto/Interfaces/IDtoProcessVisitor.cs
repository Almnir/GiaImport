using FCT.Client.Dto.Consolidation;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoProcessVisitor<T>
    {
        T Visit(AltsDto dto);
        T Visit(SheetsCDto dto);
        T Visit(MarksCDto dto);
        T Visit(FinalMarksCDto dto);
        T Visit(ComplectsDto dto);
        T Visit(AnswersDto dto);
        T Visit(AppealsDto dto);
        T Visit(AppealTasksDto dto);
        T Visit(HumanTestsDto dto);
        T Visit(MarksDto dto);
        T Visit(PrnfCertificatePrintMainDto dto);
        T Visit(CurrentRegionDto dto);
        T Visit(AreasDto dto);
        T Visit(ExpertsDto dto);
        T Visit(GovernmentsDto dto);
        T Visit(ParticipantsDto dto);
        T Visit(SchoolsDto dto);
        T Visit(StationsDto dto);
        T Visit(ParticipantsVerbalExamPlacesOnStationDto dto);
        T Visit(ParticipantsExamPlacesOnStationDto dto);
        T Visit(ParticipantsProfilingSubjectDto dto);
        T Visit(ExpertsExamsDto dto);
        T Visit(ExpertsSubjectsDto dto);
        T Visit(StationWorkersSubjectsDto dto);
        T Visit(SchoolParticipantDto dto);
        T Visit(ParticipantsExamsDto dto);
        T Visit(ParticipantPropertiesDto dto);
        T Visit(PCentersDto dto);
        T Visit(StationWorkersAccreditationDto dto);
        T Visit(PlacesDto dto);
        T Visit(StationsExamsDto dto);
        T Visit(StationWorkerOnStationDto dto);
        T Visit(StationExamAuditoryDto dto);
        T Visit(AuditoriumsDto dto);
        T Visit(ParticipantsExamsOnStationDto dto);
        T Visit(StationWorkerOnExamDto dto);
        T Visit(StationWorkersDto dto);
        T Visit(AddressDto dto);
        T Visit(CurrentRegionAddressDto dto);
        T Visit(SchoolAddressDto dto);
        T Visit(ParticipantsExamsOnSchoolDto dto);
        T Visit(CertificateKeysDto dto);
        T Visit(DatsBordersDto dto);
        T Visit(DatsGroupsDto dto);
        T Visit(ReportJournalDto dto);
    }
}
