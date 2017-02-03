CREATE TABLE [loader].[ac_Appeals](
	[REGION] [int] NOT NULL,
	[AppealID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[HumanTestFK] [uniqueidentifier] NOT NULL,
	[AppealType] [int] NOT NULL,
	[DeclinedByCommittee] [bit] NOT NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[InProcess] [datetime] NULL,
	[AppealCondition] [int] NOT NULL,
	[WorkStation] [int] NOT NULL,
	[AppealComment] [varchar](2000) NULL,
 CONSTRAINT [PK_ac_Appeals] PRIMARY KEY CLUSTERED 
(
	[AppealID] ASC,
	[REGION] ASC
))

CREATE TABLE [loader].[ac_AppealTasks](
	[REGION] [int] NOT NULL,
	[AppealTaskID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[AppealFK] [uniqueidentifier] NOT NULL,
	[TaskType] [int] NOT NULL,
	[TaskNumber] [int] NOT NULL,
	[OldValue] [varchar](100) NULL,
	[NewValue] [varchar](100) NULL,
 CONSTRAINT [PK_ac_AppealTasks] PRIMARY KEY CLUSTERED 
(
	[AppealTaskID] ASC
))
CREATE TABLE [loader].[ac_Changes](
	[REGION] [int] NOT NULL,
	[ChangeID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[AppealFK] [uniqueidentifier] NOT NULL,
	[OldPrimaryMark] [int] NOT NULL,
	[OldMark100] [int] NOT NULL,
	[OldMark5] [int] NOT NULL,
	[NewPrimaryMark] [int] NOT NULL,
	[NewMark100] [int] NOT NULL,
	[NewMark5] [int] NOT NULL,
	[Info] [varbinary](256) NULL,
 CONSTRAINT [PK_ac_Changes] PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC,
	[AppealFK] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[prnf_CertificatePrintMain](
	[REGION] [int] NOT NULL,
	[CertificatePrintMainID] [uniqueidentifier] NOT NULL,
	[ParticipantFK] [uniqueidentifier] NOT NULL,
	[CertificateBlob] [image] NULL,
	[AreaCode] [int] NULL,
	[SchoolCode] [int] NULL,
	[Punkt] [varchar](50) NULL,
	[Wave] [int] NOT NULL,
	[LicenseNumber] [varchar](50) NOT NULL,
	[PrintTime] [varchar](50) NULL,
	[Surname] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[SecondName] [varchar](50) NULL,
	[DocumentSeries] [varchar](50) NULL,
	[DocumentNumber] [varchar](50) NULL,
	[Sex] [bit] NOT NULL,
	[Graduate] [bit] NOT NULL,
	[LicenseDouble] [bit] NOT NULL,
	[Reserve1] [varchar](255) NOT NULL,
	[Reserve2] [varchar](255) NULL,
	[Reserve3] [varchar](255) NULL,
	[Reserve4] [varchar](255) NULL,
	[Reserve5] [varchar](255) NOT NULL,
 CONSTRAINT [PK_prn_CertificatePrintMain] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[CertificatePrintMainID] ASC
))
CREATE TABLE [loader].[rbd_Address](
	[AddressID] [uniqueidentifier] NOT NULL,
	[REGION] [int] NOT NULL,
	[ZipCode] [varchar](16) NOT NULL,
	[LocalityTypeID] [int] NOT NULL,
	[LocalityName] [varchar](255) NOT NULL,
	[StreetTypeID] [int] NOT NULL,
	[StreetName] [varchar](255) NOT NULL,
	[BuildingTypeID] [int] NOT NULL,
	[BuildingNumber] [varchar](255) NOT NULL,
	[TownshipID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_Address] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
))
CREATE TABLE [loader].[rbd_Areas](
	[AreaID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Region] [int] NOT NULL,
	[AreaCode] [int] NOT NULL,
	[AreaName] [varchar](255) NOT NULL,
	[LawAddress] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
	[ChargeFIO] [varchar](150) NULL,
	[Phones] [varchar](80) NULL,
	[Mails] [varchar](255) NULL,
	[WWW] [varchar](255) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[AreaID] ASC,
	[Region] ASC
))
CREATE TABLE [loader].[rbd_Auditoriums](
	[REGION] [int] NOT NULL,
	[AuditoriumID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[StationID] [uniqueidentifier] NOT NULL,
	[AuditoriumCode] [varchar](4) NOT NULL,
	[AuditoriumName] [varchar](255) NOT NULL,
	[RowsCount] [int] NOT NULL,
	[ColsCount] [int] NOT NULL,
	[OrganizerOrder] [int] NOT NULL,
	[DeleteType] [int] NOT NULL,
	[LimitPotencial] [int] NOT NULL,
	[Imported] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[ExamForm] [int] NOT NULL,
	[VideoControl] [bit] NOT NULL,
	[IsLab] [bit] NOT NULL,
 CONSTRAINT [PK_rbd_Auditoriums] PRIMARY KEY CLUSTERED 
(
	[AuditoriumID] ASC,
	[REGION] ASC
),
 CONSTRAINT [AK_KEY_2_RBD_AUDI] UNIQUE NONCLUSTERED 
(
	[AuditoriumID] ASC,
	[StationID] ASC
))
CREATE TABLE [loader].[rbd_AuditoriumsSubjects](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[AuditoriumID] [uniqueidentifier] NOT NULL,
	[REGION] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_AuditoriumsSubjects_1] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[ID] ASC
))
CREATE TABLE [loader].[rbd_CurrentRegion](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[REGION] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[RCOIName] [varchar](255) NOT NULL,
	[RCOILawAddress] [varchar](max) NOT NULL,
	[RCOIAddress] [varchar](max) NOT NULL,
	[RCOIProperty] [varchar](255) NOT NULL,
	[RCOIDPosition] [varchar](255) NOT NULL,
	[RCOIDFio] [varchar](255) NOT NULL,
	[RCOIPhones] [varchar](255) NOT NULL,
	[RCOIFaxs] [varchar](255) NOT NULL,
	[RCOIEMails] [varchar](255) NOT NULL,
	[GEKAddress] [varchar](255) NULL,
	[GEKDFio] [varchar](255) NOT NULL,
	[GEKPhones] [varchar](255) NOT NULL,
	[GEKFaxs] [varchar](255) NOT NULL,
	[GEKEMails] [varchar](255) NOT NULL,
	[EGEWWW] [varchar](255) NOT NULL,
 CONSTRAINT [PK_RBD_CURRENTREGION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
))
CREATE TABLE [loader].[rbd_CurrentRegionAddress](
	[CurrentRegionAddressID] [uniqueidentifier] NOT NULL,
	[REGION] [int] NOT NULL,
	[CurrentRegionID] [uniqueidentifier] NOT NULL,
	[AddressID] [uniqueidentifier] NOT NULL,
	[AddressTypeID] [int] NOT NULL,
	[AddressKind] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_CurrentRegionAddress] PRIMARY KEY CLUSTERED 
(
	[CurrentRegionAddressID] ASC
))
CREATE TABLE [loader].[rbd_Experts](
	[ExpertID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[REGION] [int] NOT NULL,
	[ExpertCode] [int] NOT NULL,
	[Surname] [varchar](80) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[SecondName] [varchar](80) NULL,
	[DocumentSeries] [varchar](9) NULL,
	[DocumentNumber] [varchar](12) NOT NULL,
	[DocumentTypeCode] [int] NOT NULL,
	[Sex] [bit] NULL,
	[EduTypeFK] [int] NULL,
	[EduKindFK] [int] NULL,
	[Seniority] [int] NOT NULL,
	[PrecedingYear] [int] NOT NULL,
	[BirthYear] [varchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Positions] [varchar](255) NOT NULL,
	[SchoolID] [uniqueidentifier] NULL,
	[NotSchoolJob] [varchar](255) NULL,
	[ThirdVerifyAcc] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[GovernmentID] [uniqueidentifier] NULL,
	[InConflictCommission] [bit] NOT NULL,
	[Qualification] [varchar](255) NULL,
	[ExpertCategoryID] [int] NOT NULL,
 CONSTRAINT [PK_Experts] PRIMARY KEY CLUSTERED 
(
	[ExpertID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[rbd_ExpertsExams](
	[REGION] [int] NOT NULL,
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ExpertID] [uniqueidentifier] NOT NULL,
	[ExamGlobalID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[StationsExamsID] [uniqueidentifier] NULL,
	[CheckFormOnExam] [int] NOT NULL,
 CONSTRAINT [PK_rbd_ExpertsExams] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[ID] ASC
))
CREATE TABLE [loader].[rbd_ExpertsSubjects](
	[REGION] [int] NOT NULL,
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ExpertID] [uniqueidentifier] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[CheckForm] [int] NOT NULL,
	[ExpertCategoryID] [int] NOT NULL,
	[ThirdVerifyAcc] [bit] NOT NULL,
 CONSTRAINT [PK_rbd_ExpertsSubjects] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[ID] ASC
),
 CONSTRAINT [IX_rbd_ExpertsSubjects] UNIQUE NONCLUSTERED 
(
	[ExpertID] ASC,
	[SubjectCode] ASC
))
CREATE TABLE [loader].[rbd_Governments](
	[GovernmentID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[RegionID] [uniqueidentifier] NOT NULL,
	[REGION] [int] NOT NULL,
	[GovernmentCode] [int] NOT NULL,
	[GovernmentName] [varchar](255) NOT NULL,
	[LawAddress] [varchar](255) NOT NULL,
	[Address] [varchar](255) NULL,
	[ChargeFIO] [varchar](150) NOT NULL,
	[Phones] [varchar](80) NOT NULL,
	[Mails] [varchar](80) NOT NULL,
	[WWW] [varchar](255) NULL,
	[ChargePosition] [varchar](150) NOT NULL,
	[Faxes] [varchar](80) NOT NULL,
	[GType] [int] NOT NULL,
	[SpecialistFIO] [varchar](150) NOT NULL,
	[SpecialistMails] [varchar](80) NULL,
	[SpecialistPhones] [varchar](255) NOT NULL,
	[DeleteType] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[TimeZoneId] [int] NULL,
 CONSTRAINT [PK_Governments] PRIMARY KEY CLUSTERED 
(
	[GovernmentID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[rbd_ParticipantProperties](
	[PropertyId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ParticipantId] [uniqueidentifier] NOT NULL,
	[Property] [int] NOT NULL,
	[PValue] [varchar](255) NULL,
	[Region] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_ParticipantProperties] PRIMARY KEY CLUSTERED 
(
	[Region] ASC,
	[PropertyId] ASC
))
CREATE TABLE [loader].[rbd_Participants](
	[ParticipantID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Region] [int] NOT NULL,
	[ParticipantCode] [varchar](16) NULL,
	[Surname] [varchar](80) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[SecondName] [varchar](80) NULL,
	[DocumentSeries] [varchar](9) NULL,
	[DocumentNumber] [varchar](12) NOT NULL,
	[DocumentTypeCode] [int] NOT NULL,
	[Sex] [bit] NOT NULL,
	[Gia] [int] NOT NULL,
	[GiaAccept] [int] NOT NULL,
	[pClass] [varchar](50) NOT NULL,
	[BirthDay] [datetime] NOT NULL,
	[Reserve1] [varchar](255) NULL,
	[Reserve2] [varchar](255) NULL,
	[DeleteType] [int] NOT NULL,
	[LimitPotencial] [int] NOT NULL,
	[ParticipantDouble] [uniqueidentifier] NULL,
	[FinishRegion] [int] NULL,
	[ParticipantCategoryFK] [int] NOT NULL,
	[SchoolRegistration] [uniqueidentifier] NOT NULL,
	[SchoolOutcoming] [uniqueidentifier] NULL,
	[Study] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[CitizenshipID] [int] NOT NULL,
	[SchoolOutcomingName] [varchar](255) NULL,
 CONSTRAINT [PK_Participants] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC,
	[Region] ASC
))
CREATE TABLE [loader].[rbd_ParticipantsExamPStation](
	[PExamPlacesOnStationID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PlacesID] [uniqueidentifier] NOT NULL,
	[AuditoriumID] [uniqueidentifier] NOT NULL,
	[StationExamAuditoryID] [uniqueidentifier] NOT NULL,
	[StationsExamsID] [uniqueidentifier] NOT NULL,
	[ParticipantsExamsOnStationID] [uniqueidentifier] NOT NULL,
	[Region] [int] NOT NULL,
	[IsManual] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_RBD_PARTICIPANTSEXAMPLACES] PRIMARY KEY CLUSTERED 
(
	[PExamPlacesOnStationID] ASC,
	[Region] ASC
),
 CONSTRAINT [placeParticipant] UNIQUE NONCLUSTERED 
(
	[PlacesID] ASC,
	[ParticipantsExamsOnStationID] ASC,
	[Region] ASC
),
 CONSTRAINT [placeStationsExamsIDParticipant] UNIQUE NONCLUSTERED 
(
	[StationsExamsID] ASC,
	[ParticipantsExamsOnStationID] ASC,
	[Region] ASC
))
CREATE TABLE [loader].[rbd_ParticipantsExams](
	[REGION] [int] NOT NULL,
	[ParticipantID] [uniqueidentifier] NOT NULL,
	[ExamGlobalID] [int] NOT NULL,
	[ParticipantsExamsID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[ExamFormatCode] [int] NULL,
 CONSTRAINT [PK_rbd_ParticipantsExams_1] PRIMARY KEY CLUSTERED 
(
	[ParticipantsExamsID] ASC
),
 CONSTRAINT [AK_KEY_2_RBD_PART2] UNIQUE NONCLUSTERED 
(
	[ExamGlobalID] ASC,
	[ParticipantsExamsID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[rbd_ParticipantsExamsOnStation](
	[ParticipantsExamsOnStationID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ExamGlobalID] [int] NOT NULL,
	[ParticipantsExamsID] [uniqueidentifier] NOT NULL,
	[StationsExamsID] [uniqueidentifier] NOT NULL,
	[Region] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[SessionID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_rbd_ParticipantsExamsOnStation] PRIMARY KEY CLUSTERED 
(
	[ParticipantsExamsOnStationID] ASC,
	[Region] ASC
),
 CONSTRAINT [AK_KEY_2_RBD_PART] UNIQUE NONCLUSTERED 
(
	[ParticipantsExamsOnStationID] ASC,
	[StationsExamsID] ASC
),
 CONSTRAINT [IX_rbd_ParticipantsExamsOnStation_1] UNIQUE NONCLUSTERED 
(
	[ExamGlobalID] ASC,
	[ParticipantsExamsID] ASC,
	[Region] ASC
))
CREATE TABLE [loader].[rbd_ParticipantsProfSubject](
	[REGION] [int] NOT NULL,
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ParticipantID] [uniqueidentifier] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_ParticipantsProfilingSubject] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[ID] ASC
))
CREATE TABLE [loader].[rbd_ParticipantsSubject](
	[REGION] [int] NOT NULL,
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ParticipantID] [uniqueidentifier] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_ParticipantsSubject] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[ID] ASC
))
CREATE TABLE [loader].[rbd_Places](
	[PlacesID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[REGION] [int] NOT NULL,
	[AuditoriumID] [uniqueidentifier] NOT NULL,
	[Row] [int] NOT NULL,
	[Col] [int] NOT NULL,
	[IsBad] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[PlaceType] [int] NOT NULL,
 CONSTRAINT [PK_rbdc_Places] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[PlacesID] ASC
),
 CONSTRAINT [AK_KEY_2_RBDC_PLA] UNIQUE NONCLUSTERED 
(
	[PlacesID] ASC,
	[AuditoriumID] ASC
),
 CONSTRAINT [PLACEROWCOLAUDITORY] UNIQUE NONCLUSTERED 
(
	[AuditoriumID] ASC,
	[Row] ASC,
	[Col] ASC
))
CREATE TABLE [loader].[rbd_SchoolAddress](
	[SchoolAddressID] [uniqueidentifier] NOT NULL,
	[REGION] [int] NOT NULL,
	[SchoolID] [uniqueidentifier] NOT NULL,
	[AddressID] [uniqueidentifier] NOT NULL,
	[AddressTypeID] [int] NOT NULL,
	[AddressKind] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_SchoolAddress] PRIMARY KEY CLUSTERED 
(
	[SchoolAddressID] ASC
))
CREATE TABLE [loader].[rbd_Schools](
	[SchoolID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[GovernmentID] [uniqueidentifier] NOT NULL,
	[SchoolCode] [int] NOT NULL,
	[SchoolName] [varchar](1024) NULL,
	[SchoolKindFK] [smallint] NOT NULL,
	[SchoolPropertyFk] [smallint] NOT NULL,
	[AreaFK] [uniqueidentifier] NOT NULL,
	[TownTypeFK] [int] NOT NULL,
	[LawAddress] [varchar](max) NOT NULL,
	[Address] [varchar](max) NULL,
	[DPosition] [varchar](150) NOT NULL,
	[FIO] [varchar](150) NOT NULL,
	[Phones] [varchar](80) NOT NULL,
	[Faxs] [varchar](80) NULL,
	[Mails] [varchar](255) NOT NULL,
	[People11] [int] NOT NULL,
	[People9] [int] NOT NULL,
	[ChargeFIO] [varchar](150) NOT NULL,
	[WWW] [varchar](255) NULL,
	[LicNum] [varchar](80) NULL,
	[LicRegNo] [varchar](80) NULL,
	[LicIssueDate] [varchar](10) NULL,
	[LicFinishingDate] [varchar](10) NULL,
	[AccCertNum] [varchar](255) NULL,
	[AccCertRegNo] [varchar](255) NULL,
	[AccCertIssueDate] [varchar](10) NULL,
	[AccCertFinishingDate] [varchar](10) NULL,
	[isVirtualSchool] [bit] NULL,
	[SReserve1] [varchar](255) NULL,
	[SReserve2] [varchar](255) NULL,
	[DeleteType] [int] NOT NULL,
	[IsTOM] [bit] NOT NULL,
	[REGION] [int] NOT NULL,
	[ShortName] [varchar](255) NULL,
	[TownshipFK] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[SchoolFlags] [int] NOT NULL,
 CONSTRAINT [PK_RBD_SCHOOLS] PRIMARY KEY CLUSTERED 
(
	[SchoolID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[rbd_StationExamAuditory](
	[StationExamAuditoryID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Region] [int] NOT NULL,
	[StationsExamsID] [uniqueidentifier] NOT NULL,
	[StationID] [uniqueidentifier] NOT NULL,
	[AuditoriumID] [uniqueidentifier] NOT NULL,
	[PlacesCount] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[IsPreparation] [bit] NULL,
	[ExamFormatCode] [int] NULL,
	[IsAutoAppoint] [bit] NOT NULL,
 CONSTRAINT [PK_RBD_STATIONWORKEREXAMAUDITO] PRIMARY KEY CLUSTERED 
(
	[StationExamAuditoryID] ASC
),
 CONSTRAINT [AK_KEY_2_RBD_STAT3] UNIQUE NONCLUSTERED 
(
	[StationExamAuditoryID] ASC,
	[StationsExamsID] ASC,
	[AuditoriumID] ASC
))
CREATE TABLE [loader].[rbd_StationForm](
	[StationFormId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[StationFormType] [int] NOT NULL,
	[Region] [int] NOT NULL,
	[RegistrationCode] [int] NOT NULL,
	[GovermentCode] [int] NOT NULL,
	[StationCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [smalldatetime] NOT NULL,
	[CurrentPage] [int] NOT NULL,
	[CountPage] [int] NOT NULL,
	[GovermentID] [uniqueidentifier] NULL,
	[StationID] [uniqueidentifier] NULL,
	[ExamGlobalID] [int] NULL,
	[IsTOM] [bit] NOT NULL,
	[ProjectName] [varchar](100) NOT NULL,
	[IDCode] [varchar](20) NULL,
	[ProjectBatchId] [int] NOT NULL,
 CONSTRAINT [PK_rbd_StationForm132] PRIMARY KEY CLUSTERED 
(
	[StationFormId] ASC,
	[Region] ASC
))
CREATE TABLE [loader].[rbd_StationFormAct](
	[StationFormActId] [uniqueidentifier] NOT NULL,
	[Region] [int] NOT NULL,
	[RegistrationCode] [int] NULL,
	[GovermentCode] [int] NOT NULL,
	[StationCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [smalldatetime] NOT NULL,
	[GovermentID] [uniqueidentifier] NULL,
	[StationID] [uniqueidentifier] NULL,
	[ExamGlobalID] [int] NULL,
	[ProjectName] [varchar](100) NOT NULL,
	[ProjectBatchId] [int] NOT NULL,
	[CertificateNum] [varchar](20) NOT NULL,
	[StartTime] [varchar](5) NOT NULL,
	[EndTime] [varchar](5) NOT NULL,
	[IDCode] [varchar](20) NOT NULL,
	[B01] [bit] NULL,
	[B02] [bit] NULL,
	[B03] [bit] NULL,
	[B04] [bit] NULL,
	[B05] [bit] NULL,
	[B01Other] [bit] NULL,
	[B02Other] [bit] NULL,
	[B03Other] [bit] NULL,
	[B04Other] [bit] NULL,
	[B05Other] [bit] NULL,
	[B1] [bit] NULL,
	[B2] [bit] NULL,
	[B3] [bit] NULL,
	[B4] [bit] NULL,
	[B5] [bit] NULL,
	[B6] [bit] NULL,
	[B7] [bit] NULL,
	[B8] [bit] NULL,
	[B9] [bit] NULL,
	[B10] [bit] NULL,
	[B11] [bit] NULL,
	[B12] [bit] NULL,
	[B13] [bit] NULL,
	[B14] [bit] NULL,
	[B15] [bit] NULL,
	[B16] [bit] NULL,
	[B17] [bit] NULL,
	[B18] [bit] NULL,
	[B19] [bit] NULL,
	[B20] [bit] NULL,
	[B21] [bit] NULL,
	[B22] [bit] NULL,
	[B23] [bit] NULL,
	[B24] [bit] NULL,
	[B25] [bit] NULL,
	[B26] [bit] NULL,
	[B27] [bit] NULL,
	[B28] [bit] NULL,
	[B29] [bit] NULL,
	[B30] [bit] NULL,
	[Comments] [bit] NULL,
 CONSTRAINT [PK_rbd_StationFormAct] PRIMARY KEY CLUSTERED 
(
	[StationFormActId] ASC
))
CREATE TABLE [loader].[rbd_StationFormAuditoryFields](
	[StationFormAuditoryFieldsId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[StationFormId] [uniqueidentifier] NOT NULL,
	[Region] [int] NOT NULL,
	[StationFormsFieldsHelperId] [uniqueidentifier] NOT NULL,
	[AuditoriumCode] [varchar](4) NOT NULL,
	[AuditoriumID] [uniqueidentifier] NULL,
	[FieldValue1] [int] NULL,
	[FieldValue2] [varchar](1024) NULL,
	[FieldValue3] [image] NULL,
 CONSTRAINT [PK_rbd_StationFormAuditoryFields] PRIMARY KEY CLUSTERED 
(
	[StationFormAuditoryFieldsId] ASC,
	[Region] ASC
))
CREATE TABLE [loader].[rbd_StationFormFields](
	[StationFormFieldsId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[StationFormId] [uniqueidentifier] NOT NULL,
	[Region] [int] NOT NULL,
	[StationFormsFieldsHelperId] [uniqueidentifier] NOT NULL,
	[FieldValue] [int] NULL,
	[FieldDesc] [varchar](255) NULL,
 CONSTRAINT [PK_rbd_StationFormFields] PRIMARY KEY CLUSTERED 
(
	[StationFormFieldsId] ASC,
	[Region] ASC
))
CREATE TABLE [loader].[rbd_Stations](
	[StationID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[REGION] [int] NOT NULL,
	[AreaID] [uniqueidentifier] NOT NULL,
	[StationCode] [int] NOT NULL,
	[StationName] [varchar](255) NOT NULL,
	[StationAddress] [varchar](1000) NULL,
	[SchoolFK] [uniqueidentifier] NULL,
	[GovernmentID] [uniqueidentifier] NOT NULL,
	[sVolume] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[Phones] [varchar](255) NULL,
	[Mails] [varchar](255) NOT NULL,
	[PCenterID] [uniqueidentifier] NULL,
	[IsTOM] [bit] NOT NULL,
	[DeleteType] [int] NOT NULL,
	[AuditoriumsCount] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[ExamForm] [int] NOT NULL,
	[VideoControl] [bit] NOT NULL,
	[AddressID] [uniqueidentifier] NULL,
	[StationFlags] [int] NOT NULL,
	[TimeZoneId] [int] NULL,
 CONSTRAINT [PK_Stations] PRIMARY KEY CLUSTERED 
(
	[StationID] ASC
))
CREATE TABLE [loader].[rbd_StationsExams](
	[REGION] [int] NOT NULL,
	[StationsExamsID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[StationID] [uniqueidentifier] NOT NULL,
	[ExamGlobalID] [int] NOT NULL,
	[PlacesCount] [int] NULL,
	[LockOnStation] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[IsAutoAppoint] [bit] NOT NULL,
 CONSTRAINT [PK_RBD_STATIONSEXAMS] PRIMARY KEY CLUSTERED 
(
	[StationsExamsID] ASC,
	[REGION] ASC
),
 CONSTRAINT [AK_KEY_2_RBD_STAT2] UNIQUE NONCLUSTERED 
(
	[StationsExamsID] ASC,
	[StationID] ASC
),
 CONSTRAINT [AK_KEY_3_RBD_STAT] UNIQUE NONCLUSTERED 
(
	[StationsExamsID] ASC,
	[ExamGlobalID] ASC
))
CREATE TABLE [loader].[rbd_StationWorkerOnExam](
	[REGION] [int] NOT NULL,
	[StationWorkerOnExamID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SWorkerPositionID] [int] NOT NULL,
	[StationsExamsID] [uniqueidentifier] NOT NULL,
	[AuditoriumID] [uniqueidentifier] NULL,
	[StationExamAuditoryID] [uniqueidentifier] NULL,
	[StationWorkerOnStationID] [uniqueidentifier] NOT NULL,
	[StationId] [uniqueidentifier] NOT NULL,
	[StationWorkerId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[OrganizationRolesID] [uniqueidentifier] NULL,
	[SWorkerRoleID] [int] NULL,
 CONSTRAINT [PK_RBD_SWORKERPOSITIONSONSTATI] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[StationWorkerOnExamID] ASC
),
 CONSTRAINT [WORKER_EXAM_POSITION] UNIQUE NONCLUSTERED 
(
	[SWorkerPositionID] ASC,
	[StationsExamsID] ASC,
	[StationWorkerOnStationID] ASC
)) 
CREATE TABLE [loader].[rbd_StationWorkerOnStation](
	[StationWorkerOnStationID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[StationId] [uniqueidentifier] NOT NULL,
	[StationWorkerId] [uniqueidentifier] NOT NULL,
	[WorkerType] [int] NOT NULL,
	[SWorkerPositionID] [int] NOT NULL,
	[Region] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_RBD_STATIONWORKERONSTATION] PRIMARY KEY CLUSTERED 
(
	[StationWorkerOnStationID] ASC
),
 CONSTRAINT [AK_KEY_2_RBD_STAT] UNIQUE NONCLUSTERED 
(
	[StationId] ASC,
	[StationWorkerId] ASC
),
 CONSTRAINT [IX_rbd_StationWorkerOnStation] UNIQUE NONCLUSTERED 
(
	[StationId] ASC,
	[StationWorkerId] ASC,
	[StationWorkerOnStationID] ASC
))
CREATE TABLE [loader].[rbd_StationWorkers](
	[StationWorkerID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[REGION] [int] NOT NULL,
	[DocumentTypeCode] [int] NULL,
	[StationWorkerCode] [int] NULL,
	[Surname] [varchar](80) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[SecondName] [varchar](80) NULL,
	[DocumentSeries] [varchar](9) NULL,
	[DocumentNumber] [varchar](12) NOT NULL,
	[Sex] [bit] NULL,
	[BirthYear] [int] NOT NULL,
	[SchoolPosition] [varchar](255) NULL,
	[NotSchoolJob] [varchar](255) NULL,
	[DeleteType] [int] NOT NULL,
	[GovernmentID] [uniqueidentifier] NULL,
	[SchoolID] [uniqueidentifier] NULL,
	[WorkerPositionID] [int] NULL,
	[Imported] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
	[PrecedingYear] [int] NULL,
	[Seniority] [int] NULL,
	[EducationTypeID] [int] NULL,
	[SWorkerCategory] [varchar](255) NULL,
	[CertificateKeyID] [uniqueidentifier] NULL,
	[Phones] [varchar](255) NULL,
	[Mails] [varchar](255) NULL,
 CONSTRAINT [PK_StationWorkers] PRIMARY KEY CLUSTERED 
(
	[StationWorkerID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[rbd_StationWorkersAccreditation](
	[SWorkerAccreditationID] [uniqueidentifier] NOT NULL,
	[Region] [int] NOT NULL,
	[StationWorkerID] [uniqueidentifier] NOT NULL,
	[GovernmentID] [uniqueidentifier] NULL,
	[NotGovernmentAccreditation] [varchar](255) NULL,
	[DocumentNumber] [varchar](255) NOT NULL,
	[IsFamily] [int] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ImportCreateDate] [datetime] NULL,
	[ImportUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_rbd_StationWorkersAccreditation] PRIMARY KEY CLUSTERED 
(
	[SWorkerAccreditationID] ASC
))
CREATE TABLE [loader].[rbd_StationWorkersSubjects](
	[REGION] [int] NOT NULL,
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[StationWorkerID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_rbd_StationWorkersSubjects] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[ID] ASC
)) 
CREATE TABLE [loader].[res_Answers](
	[REGION] [int] NOT NULL,
	[AnswerID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[HumanTestFK] [uniqueidentifier] NOT NULL,
	[TaskTypeCode] [int] NOT NULL,
	[TaskNumber] [int] NOT NULL,
	[AnswerValue] [varchar](100) NULL,
	[ReplaceValue] [varchar](100) NULL,
	[CategoryValue] [int] NOT NULL,
 CONSTRAINT [PK_res_Answers] PRIMARY KEY CLUSTERED 
(
	[HumanTestFK] ASC,
	[REGION] ASC,
	[TaskTypeCode] ASC,
	[TaskNumber] ASC
)) 
CREATE TABLE [loader].[res_Complects](
	[REGION] [int] NOT NULL,
	[ComplectID] [uniqueidentifier] NOT NULL,
	[Barcode_AB] [varchar](100) NULL,
	[Barcode_C] [varchar](100) NULL,
	[Barcode_R] [varchar](100) NULL,
	[Barcode_D] [varchar](100) NULL,
	[SheetFK_AB] [uniqueidentifier] NULL,
	[SheetFK_C] [uniqueidentifier] NULL,
	[SheetFK_R] [uniqueidentifier] NULL,
	[SheetFK_D] [uniqueidentifier] NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[ZoneCode] [int] NOT NULL,
	[StuffCode] [varchar](100) NULL,
	[VariantCode] [int] NOT NULL,
	[IsTom] [bit] NOT NULL,
	[ComplectType] [tinyint] NULL,
	[ExchangedID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_resprot_Complects] PRIMARY KEY CLUSTERED 
(
	[ComplectID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[res_HumanTests](
	[REGION] [int] NOT NULL,
	[HumanTestID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ParticipantFK] [uniqueidentifier] NOT NULL,
	[PackageFK] [uniqueidentifier] NOT NULL,
	[FileName] [varchar](100) NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[StationCode] [int] NOT NULL,
	[AuditoriumCode] [varchar](100) NULL,
	[VariantCode] [int] NOT NULL,
	[ProcessCondition] [int] NOT NULL,
	[LicenseCondition] [int] NOT NULL,
	[ReplicationCondition] [int] NOT NULL,
	[Reserve01] [varchar](255) NULL,
	[Reserve02] [varchar](255) NULL,
	[Reserve03] [varchar](255) NULL,
	[Reserve04] [varchar](255) NULL,
	[Reserve05] [varchar](255) NULL,
	[Reserve06] [varchar](255) NULL,
	[Reserve07] [varchar](255) NULL,
	[Reserve08] [varchar](255) NULL,
	[Reserve09] [varchar](255) NULL,
	[Reserve10] [varchar](255) NULL,
 CONSTRAINT [PK_res_HumanTests] PRIMARY KEY CLUSTERED 
(
	[HumanTestID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[res_Marks](
	[REGION] [int] NOT NULL,
	[HumanTestID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PrimaryMark] [int] NOT NULL,
	[PercentMark] [int] NOT NULL,
	[Mark100] [int] NOT NULL,
	[Mark5] [int] NOT NULL,
	[MarkX] [int] NOT NULL,
	[Rating] [float] NOT NULL,
	[PrimaryMarkA] [int] NOT NULL,
	[TestResultA] [varchar](100) NULL,
	[PrimaryMarkB] [int] NOT NULL,
	[TestResultB] [varchar](100) NULL,
	[PrimaryMarkC] [int] NOT NULL,
	[TestResultC] [varchar](100) NULL,
	[PrimaryMarkD] [int] NOT NULL,
	[TestResultD] [varchar](100) NULL,
 CONSTRAINT [PK_res_Marks] PRIMARY KEY CLUSTERED 
(
	[HumanTestID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[res_SubComplects](
	[REGION] [int] NOT NULL,
	[SubComplectID] [uniqueidentifier] NOT NULL,
	[Barcode_KIM] [varchar](100) NULL,
	[Barcode_R] [varchar](100) NULL,
	[Barcode_D] [varchar](100) NULL,
	[SheetFK_R] [uniqueidentifier] NULL,
	[SheetFK_D] [uniqueidentifier] NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[ZoneCode] [int] NOT NULL,
	[StuffCode] [varchar](100) NULL,
	[VariantCode] [int] NOT NULL,
	[IsTom] [bit] NOT NULL,
 CONSTRAINT [PK_resprot_SubComplects] PRIMARY KEY CLUSTERED 
(
	[SubComplectID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[res_SubTests](
	[REGION] [int] NOT NULL,
	[SubtestID] [uniqueidentifier] NOT NULL,
	[HumanTestID] [uniqueidentifier] NOT NULL,
	[ParticipantFK] [uniqueidentifier] NOT NULL,
	[PackageFK] [uniqueidentifier] NULL,
	[SubtestType] [int] NOT NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[SubtestName] [nvarchar](50) NOT NULL,
	[ExamDate] [nvarchar](11) NOT NULL,
	[VariantCode] [int] NOT NULL,
	[ProcessCondition] [int] NOT NULL,
	[ReplicationCondition] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Tasks] [varbinary](256) NULL,
	[PrimaryMark] [int] NULL,
	[Mark] [int] NULL,
	[Minimum] [int] NULL,
 CONSTRAINT [PK_res_Subtests_1] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC,
	[SubtestID] ASC,
	[HumanTestID] ASC,
	[SubtestType] ASC,
	[SubtestName] ASC
))
CREATE TABLE [loader].[sht_Alts](
	[AltID] [uniqueidentifier] NOT NULL,
	[REGION] [smallint] NOT NULL,
	[SheetFK] [uniqueidentifier] NOT NULL,
	[Barcode] [nchar](13) NOT NULL,
	[ProtocolCode] [varchar](50) NULL,
	[ExpertFK] [uniqueidentifier] NULL,
	[ExpertCode] [int] NOT NULL,
	[IsThird] [smallint] NULL,
	[Alt] [smallint] NULL,
	[DeleteType] [smallint] NULL,
	[Reserve01] [varchar](50) NULL,
	[Reserve02] [varchar](50) NULL,
 CONSTRAINT [PK_sht_Alts] PRIMARY KEY CLUSTERED 
(
	[AltID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[sht_FinalMarks_C](
	[REGION] [int] NOT NULL,
	[MarkID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SheetFK] [uniqueidentifier] NOT NULL,
	[TaskNumber] [int] NOT NULL,
	[MarkValue] [varchar](100) NULL,
 CONSTRAINT [PK_sht_FinalMarks_C] PRIMARY KEY NONCLUSTERED 
(
	[MarkID] ASC
))
CREATE TABLE [loader].[sht_FinalMarks_D](
	[REGION] [int] NOT NULL,
	[MarkID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SheetFK] [uniqueidentifier] NOT NULL,
	[TaskNumber] [int] NOT NULL,
	[MarkValue] [varchar](100) NULL,
 CONSTRAINT [PK_sht_FinalMarks_D] PRIMARY KEY NONCLUSTERED 
(
	[MarkID] ASC
))
CREATE TABLE [loader].[sht_Marks_AB](
	[REGION] [int] NOT NULL,
	[MarkID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SheetFK] [uniqueidentifier] NULL,
	[TaskTypeCode] [int] NOT NULL,
	[TaskNumber] [int] NOT NULL,
	[AnswerValue] [varchar](100) NULL,
	[ReplaceValue] [varchar](100) NULL,
 CONSTRAINT [PK_sht_Marks_AB] PRIMARY KEY NONCLUSTERED 
(
	[MarkID] ASC
))
CREATE TABLE [loader].[sht_Marks_C](
	[REGION] [int] NOT NULL,
	[MarkID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SheetFK] [uniqueidentifier] NOT NULL,
	[ProtocolFile] [varchar](100) NULL,
	[ProtocolCode] [varchar](100) NULL,
	[ProtocolCRC] [varchar](100) NULL,
	[ThirdCheck] [bit] NOT NULL,
	[RowNumber] [int] NOT NULL,
	[TaskNumber] [int] NOT NULL,
	[MarkValue] [varchar](100) NULL,
 CONSTRAINT [PK_sht_Marks_C] PRIMARY KEY NONCLUSTERED 
(
	[MarkID] ASC
))
CREATE TABLE [loader].[sht_Marks_D](
	[REGION] [int] NOT NULL,
	[MarkID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SheetFK] [uniqueidentifier] NOT NULL,
	[ProtocolFile] [varchar](100) NULL,
	[ProtocolCode] [varchar](100) NULL,
	[ProtocolCRC] [varchar](100) NULL,
	[ThirdCheck] [bit] NOT NULL,
	[RowNumber] [int] NOT NULL,
	[TaskNumber] [int] NOT NULL,
	[MarkValue] [varchar](100) NULL,
 CONSTRAINT [PK_sht_Marks_D] PRIMARY KEY NONCLUSTERED 
(
	[MarkID] ASC
))
CREATE TABLE [loader].[sht_Packages](
	[REGION] [int] NOT NULL,
	[PackageID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[FileName] [varchar](100) NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[StationCode] [int] NOT NULL,
	[AuditoriumCode] [varchar](100) NULL,
	[Condition] [int] NOT NULL,
	[CreateTime] [varchar](19) NULL,
	[UpdateTime] [varchar](19) NULL,
	[SheetsCount] [int] NOT NULL,
	[IsExported] [bit] NOT NULL,
	[ProjectName] [varchar](100) NULL,
	[ProjectBatchID] [int] NOT NULL,
	[ProjectBatchName] [varchar](100) NULL,
 CONSTRAINT [PK_sht_Packages_ID] PRIMARY KEY NONCLUSTERED 
(
	[PackageID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[sht_Sheets_AB](
	[REGION] [int] NOT NULL,
	[SheetID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PackageFK] [uniqueidentifier] NULL,
	[FileName] [varchar](100) NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[StationCode] [int] NOT NULL,
	[AuditoriumCode] [varchar](100) NULL,
	[Barcode] [varchar](100) NULL,
	[CRC] [varchar](100) NULL,
	[ImageNumber] [int] NOT NULL,
	[VariantCode] [int] NOT NULL,
	[HasSignature] [bit] NOT NULL,
	[Condition] [int] NOT NULL,
	[ProjectBatchID] [int] NOT NULL,
	[Reserve01] [varchar](255) NULL,
	[Reserve02] [varchar](255) NULL,
	[Reserve03] [varchar](255) NULL,
	[Reserve04] [varchar](255) NULL,
	[Reserve05] [varchar](255) NULL,
	[Reserve06] [varchar](255) NULL,
	[Reserve07] [varchar](255) NULL,
	[Reserve08] [varchar](255) NULL,
	[Reserve09] [varchar](255) NULL,
	[Reserve10] [varchar](255) NULL,
 CONSTRAINT [PK_sht_Sheets_AB] PRIMARY KEY NONCLUSTERED 
(
	[SheetID] ASC
))
CREATE TABLE [loader].[sht_Sheets_C](
	[REGION] [int] NOT NULL,
	[SheetID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PackageFK] [uniqueidentifier] NOT NULL,
	[FileName] [varchar](100) NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[StationCode] [int] NOT NULL,
	[AuditoriumCode] [varchar](100) NULL,
	[Barcode] [varchar](100) NULL,
	[CRC] [varchar](1000) NULL,
	[SheetCode] [int] NOT NULL,
	[ResponsibleCode] [varchar](100) NULL,
	[IsEmpty] [bit] NOT NULL,
	[ImageNumber] [int] NOT NULL,
	[VariantCode] [int] NOT NULL,
	[ImagePaths] [varchar](1000) NULL,
	[ProtocolCodes] [varchar](1000) NULL,
	[Condition] [int] NOT NULL,
	[ProjectBatchID] [int] NOT NULL,
	[DeleteType] [smallint] NULL,
	[Reserve01] [varchar](255) NULL,
	[Reserve02] [varchar](255) NULL,
	[Reserve03] [varchar](255) NULL,
	[Reserve04] [varchar](255) NULL,
	[Reserve05] [varchar](255) NULL,
	[Reserve06] [varchar](255) NULL,
	[Reserve07] [varchar](255) NULL,
	[Reserve08] [varchar](255) NULL,
	[Reserve09] [varchar](255) NULL,
	[Reserve10] [varchar](255) NULL,
 CONSTRAINT [PK_sht_Sheets_C] PRIMARY KEY NONCLUSTERED 
(
	[REGION] ASC,
	[SheetID] ASC
))
CREATE TABLE [loader].[sht_Sheets_D](
	[REGION] [int] NOT NULL,
	[SheetID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PackageFK] [uniqueidentifier] NOT NULL,
	[FileName] [varchar](100) NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[StationCode] [int] NOT NULL,
	[AuditoriumCode] [varchar](100) NULL,
	[Barcode] [varchar](100) NULL,
	[CRC] [varchar](1000) NULL,
	[SheetCode] [int] NOT NULL,
	[ResponsibleCode] [varchar](100) NULL,
	[IsEmpty] [bit] NOT NULL,
	[ImageNumber] [int] NOT NULL,
	[VariantCode] [int] NOT NULL,
	[ImagePaths] [varchar](1000) NULL,
	[ProtocolCodes] [varchar](1000) NULL,
	[Condition] [int] NOT NULL,
	[ProjectBatchID] [int] NOT NULL,
	[DeleteType] [smallint] NULL,
	[Reserve01] [varchar](255) NULL,
	[Reserve02] [varchar](255) NULL,
	[Reserve03] [varchar](255) NULL,
	[Reserve04] [varchar](255) NULL,
	[Reserve05] [varchar](255) NULL,
	[Reserve06] [varchar](255) NULL,
	[Reserve07] [varchar](255) NULL,
	[Reserve08] [varchar](255) NULL,
	[Reserve09] [varchar](255) NULL,
	[Reserve10] [varchar](255) NULL,
 CONSTRAINT [PK_sht_Sheets_D] PRIMARY KEY NONCLUSTERED 
(
	[REGION] ASC,
	[SheetID] ASC
))
CREATE TABLE [loader].[sht_Sheets_R](
	[REGION] [int] NOT NULL,
	[SheetID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PackageFK] [uniqueidentifier] NOT NULL,
	[FileName] [varchar](100) NULL,
	[RegionCode] [int] NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExamDate] [varchar](100) NULL,
	[StationCode] [int] NOT NULL,
	[AuditoriumCode] [varchar](100) NULL,
	[Barcode] [varchar](100) NULL,
	[CRC] [varchar](100) NULL,
	[ParticipantID] [uniqueidentifier] NOT NULL,
	[SchoolCode] [int] NOT NULL,
	[Surname] [varchar](100) NULL,
	[Name] [varchar](100) NULL,
	[SecondName] [varchar](100) NULL,
	[DocumentSeries] [varchar](100) NULL,
	[DocumentNumber] [varchar](100) NULL,
	[DocumentHash] [varchar](100) NULL,
	[Sex] [int] NOT NULL,
	[ImageNumber] [int] NOT NULL,
	[VariantCode] [int] NOT NULL,
	[HasSignature] [bit] NOT NULL,
	[Condition] [int] NOT NULL,
	[ProjectBatchID] [int] NOT NULL,
	[Reserve01] [varchar](255) NULL,
	[Reserve02] [varchar](255) NULL,
	[Reserve03] [varchar](255) NULL,
	[Reserve04] [varchar](255) NULL,
	[Reserve05] [varchar](255) NULL,
	[Reserve06] [varchar](255) NULL,
	[Reserve07] [varchar](255) NULL,
	[Reserve08] [varchar](255) NULL,
	[Reserve09] [varchar](255) NULL,
	[Reserve10] [varchar](255) NULL,
 CONSTRAINT [PK_sht_Sheets_R] PRIMARY KEY CLUSTERED 
(
	[SheetID] ASC,
	[REGION] ASC
))
CREATE TABLE [loader].[dats_Groups](
	[REGION] [int] NOT NULL,
	[ScalingGroupID] [uniqueidentifier] NOT NULL,
	[GroupTypeCode] [int] NOT NULL,
	[GroupName] [nvarchar](93) NOT NULL,
	[TestTypeCode] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ScaleMarkMinimum] [int] NOT NULL,
	[GroupMarkMinimum] [int] NULL
) ON [PRIMARY]
CREATE TABLE [loader].[dats_Borders](
	[REGION] [int] NOT NULL,
	[BorderID] [uniqueidentifier] NOT NULL,
	[ScalingGroupFK] [uniqueidentifier] NULL,
	[PrimaryMark] [int] NOT NULL,
	[ScaleMark] [int] NOT NULL
) ON [PRIMARY]
