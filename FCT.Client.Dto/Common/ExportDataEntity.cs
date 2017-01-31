using System;
using FCT.Client.Dto;
using RBD.Client.Interfaces;
using RBD.Client.Services.Import.DataSource;
using RBD.Common.Enums;

namespace RBD.Client.Common.Entity
{
    [Serializable]
    public class ExportDataEntity
    {
        public SenderInfo Sender { get; set; }
        public ExportDataDictionaryEntity Dictionary { get; set; }
        public ExportDataObjectEntity Sbor { get; set; }
        public PlanningExportDataObjectEntity Planning { get; set; }

        public ExportDataEntity()
        {
            Sender = new SenderInfo();
        }

        public ExportDataEntity(IKeyCode key, string applicationVersion, DateTime dateTimeNow)
        {
            Sender = new SenderInfo();

            if (key.IsRcoi)
                Sender.SenderType = ImportSenderType.RCOI;
            else if (key.IsMoyo)
                Sender.SenderType = ImportSenderType.MOYO;
            else if (key.IsSchool)
                Sender.SenderType = ImportSenderType.OY;

            Sender.SenderMOYO = key.MOYO;
            Sender.SenderOY = key.OY;
            Sender.ExportDate = dateTimeNow;
            Sender.Version = applicationVersion;
        }
    }

    /// <summary>
    /// СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ 
    /// СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ 
    /// СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ СПРАВОЧНИКИ 
    /// </summary>
    public class ExportDataDictionaryEntity
    {
        public ExportDataDictionaryEntity()
        {
            SWorkerPositions = new SWorkerPositionsDto[0];
            DocumentTypes = new DocumentTypesDto[0];
            SchoolProperties = new SchoolPropertiesDto[0];
            SchoolTypes = new SchoolTypesDto[0];
            TownTypes = new TownTypesDto[0];
            SchoolKinds = new SchoolKindsDto[0];
            Regions = new RegionsDto[0];
            SchemeVersions = new SchemeVersionsDto[0];
            Subjects = new SubjectsDto[0];
            Waves = new WavesDto[0];
            Exams = new ExamsDto[0];
            EducationTypes = new EducationTypesDto[0];
            EducationKinds = new EducationKindsDto[0];
            Township = new TownshipsDto[0];
            ParticipantCategory = new ParticipantCategoriesDto[0];
            Study = new StudyDto[0];
            OrganizationRoles = new OrganizationRolesDto[0];
            SearchTemplates = new SearchTemplatesDto[0];
            RegionRequisites = new RegionRequisitesDto[0];
            Citizenships = new CitizenshipDto[0];
            AddressTypes = new AddressTypeDto[0];
            LocalityTypes = new LocalityTypeDto[0];
            StreetTypes = new StreetTypeDto[0];
            BuildingTypes = new BuildingTypeDto[0];
            SWorkerRoles = new SWorkerRoleDto[0];
            TimeZones = new TimeZonesDto[0];
        }

        public SWorkerPositionsDto[] SWorkerPositions { get; set; }
        public DocumentTypesDto[] DocumentTypes { get; set; }
        public SchoolPropertiesDto[] SchoolProperties { get; set; }
        public SchoolTypesDto[] SchoolTypes { get; set; }
        public TownTypesDto[] TownTypes { get; set; }
        public SchoolKindsDto[] SchoolKinds { get; set; }
        public RegionsDto[] Regions { get; set; }
        public SchemeVersionsDto[] SchemeVersions { get; set; }
        public SubjectsDto[] Subjects { get; set; }
        public WavesDto[] Waves { get; set; }
        public ExamsDto[] Exams { get; set; }
        public EducationTypesDto[] EducationTypes { get; set; }
        public EducationKindsDto[] EducationKinds { get; set; }
        public TownshipsDto[] Township { get; set; }
        public ParticipantCategoriesDto[] ParticipantCategory { get; set; }
        public StudyDto[] Study { get; set; }
        public OrganizationRolesDto[] OrganizationRoles { get; set; }
        public SearchTemplatesDto[] SearchTemplates { get; set; }
        public RegionRequisitesDto[] RegionRequisites { get; set; }
        public CitizenshipDto[] Citizenships { get; set; }
        public AddressTypeDto[] AddressTypes { get; set; }
        public LocalityTypeDto[] LocalityTypes { get; set; }
        public StreetTypeDto[] StreetTypes { get; set; }
        public BuildingTypeDto[] BuildingTypes { get; set; }
        public SWorkerRoleDto[] SWorkerRoles { get; set; }
        public TimeZonesDto[] TimeZones { get; set; }
    }

    /// <summary>
    /// СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР 
    /// СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР 
    /// СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР СБОР 
    /// </summary>
    public class ExportDataObjectEntity
    {
        public ExportDataObjectEntity()
        {
            RegionSettings = new RegionSettingsDto[0];
            Experts = new ExpertsDto[0];
            ExpertsSubjects = new ExpertsSubjectsDto[0];
            Areas = new AreasDto[0];
            Schools = new SchoolsDto[0];
            Governments = new GovernmentsDto[0];
            CurrentRegion = new CurrentRegionDto();
            Participants = new ParticipantsDto[0];
            ParticipantsExams = new ParticipantsExamsDto[0];
            ExamPassports = new ExamPassportDto[0];
            ParticipantProperties = new ParticipantPropertiesDto[0];
            ParticipantsExamsHistory = new ParticipantsExamsHistoryDto[0];
            ParticipantsSubjects = new ParticipantsSubjectDto[0];
            ParticipantsProfilingSubjects = new ParticipantsProfilingSubjectDto[0];
            SchoolParticipants = new SchoolParticipantDto[0];
            Stations = new StationsDto[0];
            StationWorkers = new StationWorkersDto[0];
            Auditoriums = new AuditoriumsDto[0];
            Places = new PlacesDto[0];
            PCenters = new PCentersDto[0];
            StationWorkersSubjects = new StationWorkersSubjectsDto[0];
            StationWorkersAccreditations = new StationWorkersAccreditationDto[0];
            AuditoriumsSubjects = new AuditoriumsSubjectsDto[0];
            StationWorkerOnStation = new StationWorkerOnStationDto[0];
            CurrentRegionAddress = new CurrentRegionAddressDto[0];
            SchoolAddress = new SchoolAddressDto[0];
            Address = new AddressDto[0];
            ParticipantsExamsOnSchool = new ParticipantsExamsOnSchoolDto[0];
            CertificateKeys = new CertificateKeysDto[0];
            CodeRanges = new CodeRangesDto[0];

            SchoolsIds = new IdentityItem[0];
            ParticipantsIds = new IdentityItem[0];
            StationsIds = new IdentityItem[0];
            StationWorkersIds = new IdentityItem[0];
            ExpertsIds = new IdentityItem[0];
        }

        public RegionSettingsDto[] RegionSettings { get; set; }
        public ExpertsDto[] Experts { get; set; }
        public ExpertsSubjectsDto[] ExpertsSubjects { get; set; }
        public AreasDto[] Areas { get; set; }
        public SchoolsDto[] Schools { get; set; }
        public GovernmentsDto[] Governments { get; set; }
        public CurrentRegionDto CurrentRegion { get; set; }
        public ParticipantsDto[] Participants { get; set; }
        public ParticipantsExamsDto[] ParticipantsExams { get; set; }
        public ExamPassportDto[] ExamPassports { get; set; }
        public ParticipantPropertiesDto[] ParticipantProperties { get; set; }
        public ParticipantsExamsHistoryDto[] ParticipantsExamsHistory { get; set; }
        public ParticipantsSubjectDto[] ParticipantsSubjects { get; set; }
        public ParticipantsProfilingSubjectDto[] ParticipantsProfilingSubjects { get; set; }
        public SchoolParticipantDto[] SchoolParticipants { get; set; }
        public StationsDto[] Stations { get; set; }
        public StationWorkersDto[] StationWorkers { get; set; }
        public AuditoriumsDto[] Auditoriums { get; set; }
        public PlacesDto[] Places { get; set; }
        public PCentersDto[] PCenters { get; set; }
        public StationWorkersSubjectsDto[] StationWorkersSubjects { get; set; }
        public StationWorkersAccreditationDto[] StationWorkersAccreditations { get; set; }
        public AuditoriumsSubjectsDto[] AuditoriumsSubjects { get; set; }
        public StationWorkerOnStationDto[] StationWorkerOnStation { get; set; }
        public CurrentRegionAddressDto[] CurrentRegionAddress { get; set; }
        public SchoolAddressDto[] SchoolAddress { get; set; }
        public AddressDto[] Address { get; set; }
        public ParticipantsExamsOnSchoolDto[] ParticipantsExamsOnSchool { get; set; }
        public CertificateKeysDto[] CertificateKeys { get; set; }
        public CodeRangesDto[] CodeRanges { get; set; }

        public IdentityItem[] SchoolsIds { get; set; }
        public IdentityItem[] ParticipantsIds { get; set; }
        public IdentityItem[] StationsIds { get; set; }
        public IdentityItem[] StationWorkersIds { get; set; }
        public IdentityItem[] ExpertsIds { get; set; }
    }

    /// <summary>
    /// ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ 
    /// ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ 
    /// ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ ПЛАНИРОВАНИЕ 
    /// </summary>
    public class PlanningExportDataObjectEntity
    {
        public PlanningExportDataObjectEntity()
        {
            ParticipantsVerbalExamPlacesOnStation = new ParticipantsVerbalExamPlacesOnStationDto[0];
            ParticipantsExamPlacesOnStation = new ParticipantsExamPlacesOnStationDto[0];
            ParticipantsExamsOnStation = new ParticipantsExamsOnStationDto[0];
            StationExamAuditory = new StationExamAuditoryDto[0];
            StationsExams = new StationsExamsDto[0];
            StationWorkerOnExam = new StationWorkerOnExamDto[0];
            ExpertsExams = new ExpertsExamsDto[0];
        }

        public Guid GovernmentId;
        public ParticipantsExamPlacesOnStationDto[] ParticipantsExamPlacesOnStation { get; set; }
        public ParticipantsVerbalExamPlacesOnStationDto[] ParticipantsVerbalExamPlacesOnStation { get; set; }
        public ParticipantsExamsOnStationDto[] ParticipantsExamsOnStation { get; set; }
        public StationExamAuditoryDto[] StationExamAuditory { get; set; }
        public StationsExamsDto[] StationsExams { get; set; }
        public StationWorkerOnExamDto[] StationWorkerOnExam { get; set; }
        public ExpertsExamsDto[] ExpertsExams { get; set; }
    }

    public class IdentityItem : IEquatable<IdentityItem>
    {
        /* Идентификатор объекта */
        public Guid DtoId { get; set; }
        /* Признак того, что объект выгружаем при полном импорте */
        public bool IsExportable { get; set; }
        /* Признак того, что объект принадлежит на верхнем уровне нашему МСУ или ОО */
        public bool IsMine { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(IdentityItem)) return false;
            return Equals((IdentityItem)obj);
        }

        public bool Equals(IdentityItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.DtoId.Equals(DtoId);
        }

        public override int GetHashCode()
        {
            unchecked { return DtoId.ToString().GetHashCode(); }
        }
    }
}