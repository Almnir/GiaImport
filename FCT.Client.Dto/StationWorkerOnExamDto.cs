using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
	[Serializable][Description("Назначение работника на экзамен в ППЭ")]
    [BulkTable("rbd_StationWorkerOnExam", "StationWorkerOnExam", RootTagName = "ArrayOfStationWorkerOnExamDto")]
    public class StationWorkerOnExamDto : DtoCreateDateBase, IEquatable<StationWorkerOnExamDto>, IDtoWithStationWorker, IDtoWithStation, 
        IDtoWithWorkerPosition, IDtoWithStationExam, IDtoWithAuditorium, IUidableDto, IDtoCollectorAccepter
	{
        [BulkColumn("REGION")]
        [XmlElement]
        public override int Region { get; set; }

        [BulkColumn("StationWorkerOnExamID")]
	    public override Guid DtoID { get; set; }

        [XmlIgnore]private AuditoriumSurrogateKey _auditoriumSurrogateKey;
        [XmlIgnore]public AuditoriumSurrogateKey AuditoriumSurrogateKey
        {
            get
            {
                if (string.IsNullOrEmpty(AuditoriumCode)) return null;
                return _auditoriumSurrogateKey ?? (_auditoriumSurrogateKey = new AuditoriumSurrogateKey(Station, AuditoriumCode));
            }
            set { _auditoriumSurrogateKey = value; }
        }
        
        /* поиск аудитории по коду + ппэ */
        [XmlIgnore]
        private string _auditoriumCode;
        [CsvColumn(Name = "Код аудитории", FieldIndex = 3)]
        public string AuditoriumCode
        {
            get { return _auditoriumCode; }
            set { _auditoriumCode = value.ToAuditoriumCodeFormat(); }
        }

        [BulkColumn("SWorkerPositionID")]
#if GiaDataCollect
        [XmlElement("SWorkerPosition")]
#endif
        [CsvColumn(Name = "Код должности в ППЭ", FieldIndex = 5)]
		public int WorkerPositionCode { get; set; }

        [BulkColumn("StationId")]
        [CsvColumn(Name = "Guid ППЭ", FieldIndex = 2)]
		public Guid Station { get; set; }

        [BulkColumn("StationWorkerId")]
		[CsvColumn(Name = "Guid работника ППЭ", FieldIndex = 1)]
		public Guid StationWorker { get; set; }
		
		[CsvColumn(Name = "Код дня экзамена", FieldIndex = 4)]
		public int Exam { get; set; }

        [XmlIgnore] public bool IsAutoAppoint { get; set; }

        [XmlElement("LockOnStation")]
        public string IsAutoAppointSerialize
        {
            get { return IsAutoAppoint ? "1" : "0"; }
            set { IsAutoAppoint = XmlConvert.ToBoolean(value); }
        }

        [BulkColumn("CreateDate")]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 6, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn("UpdateDate")]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 7, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        #region NonSerializable

        [Description("Работник ППЭ")]
        [XmlIgnore] public string StationWorkerName { get { return StationWorkerDto.Return(x => x.FIO, StationWorkerUID); } }

        [Description("ППЭ")]
        [XmlIgnore] public string StationName { get { return StationDto.Return(x => x.ToString(), StationUID); } }

        [Description("Должность")]
        [XmlIgnore] public string WorkerPositionName { get { return WorkerPositionDto.Return(x => x.SWorkerPositionName, "---"); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return StationExamDto.With(x => x.ExamDto).Return(x => x.ToString(), Exam.ToString()); } }

        [Description("Аудитория")]
        [XmlIgnore] public string AuditoriumName { get { return AuditoriumDto.Return(x => x.ToString(), AuditoriumUID); } }

        [Description("Роль организатора вне аудитории")]
        [XmlIgnore] public string OrganizationRoleName { get { return OrganizationRoleDto.Return(x => x.OrgRoleName, "---"); } }

        [XmlIgnore]
	    public string WorkerGovernmentCode
	    {
	        get { return StationWorkerDto.With(c => c.GovernmentDto).Return(c => c.GovernmentCode.ToString(), "---"); }
	    }

        [XmlIgnore]
	    public string WorkerSchoolCode
	    {
	        get
	        {
                return StationWorkerDto.With(c => c.SchoolDto).Return(c => c.SchoolCode.ToString(), "---");

	        }
	    }

	    [XmlIgnore] public StationsExamsDto Dirty_StationExam
	    {
	        get { return new StationsExamsDto {Station = Station, Exam = Exam, Region = Region}; }
	    }

        [XmlIgnore] public SWorkerPositionsDto WorkerPositionDto { get; set; }
        [XmlIgnore] public StationsExamsDto StationExamDto { get; set; }
        [XmlIgnore] public AuditoriumsDto AuditoriumDto { get; set; }
        [XmlIgnore] public StationExamAuditoryDto StationExamAuditoryDto { get; set; }
        [XmlIgnore] public StationWorkerOnStationDto StationWorkerOnStationDto { get; set; }
        [XmlIgnore] public StationsDto StationDto { get; set; }
        [XmlIgnore] public StationWorkersDto StationWorkerDto { get; set; }
        [XmlIgnore] public OrganizationRolesDto OrganizationRoleDto { get; set; }
        [XmlIgnore] public SWorkerRoleDto SWorkerRoleDto { get; set; }
         
        public int WorkerPositionId {get { return WorkerPositionDto != null ? WorkerPositionDto.SWorkerPositionID : -1; } set {} }
        [BulkColumn("StationsExamsID")]
        public Guid StationExamId { get { return StationExamDto != null ? StationExamDto.DtoID : Guid.Empty; } set { } }
        [BulkColumn("StationExamAuditoryID")]
        public Guid? StationExamAuditoryId {get { return StationExamAuditoryDto != null ? StationExamAuditoryDto.DtoID : (Guid?)null; } set {} }
        [BulkColumn("StationWorkerOnStationID")]
        public Guid StationWorkerOnStationId { get { return StationWorkerOnStationDto != null ? StationWorkerOnStationDto.DtoID : Guid.Empty; } set { } }
        public Guid? OrganizationRole {get { return OrganizationRoleDto != null ? OrganizationRoleDto.DtoID : (Guid?)null; } set {} }

        [BulkColumn("AuditoriumID")]
        [XmlElement("Auditorium")]
        public Guid? AuditoriumId { get { return AuditoriumDto != null ? AuditoriumDto.DtoID : (Guid?)null; } set { } }

	    [XmlIgnore] public StationWorkerOnStationDto Dirty_StationWorkerOnStation
	    {
            get { return new StationWorkerOnStationDto { Station = Station, StationWorker = StationWorker, Region = Region }; }
	    }

        public StationExamAuditoryDto GetDirtyStationExamAuditory(Func<int,bool> isExamVoice)
        {
            if (string.IsNullOrEmpty(AuditoriumCode)) return null;
            var res = new StationExamAuditoryDto
            {
                AuditoriumCode = AuditoriumCode,
                Station = Station,
                Exam = Exam,
                Region = Region
            };
            res.IsPreparation = isExamVoice(Exam);

            return res;
        }

        [XmlIgnore]
        public AuditoriumsDto Dirty_Auditorium
        {
            get
            {
                if (string.IsNullOrEmpty(AuditoriumCode)) return null;
                return new AuditoriumsDto { Station = Station, AuditoriumCode = AuditoriumCode };
            }
        }

        #endregion

        [CsvColumn(Name = "Код роли организатора", FieldIndex = 8)]
        public int? OrganizationRoleCode { get; set; }

#if GiaDataCollect
        [XmlElement("SWorkerRole")]
#endif
        [BulkColumn("SWorkerRoleID")]
        [CsvColumn(Name = "Код роли работника в ППЭ", FieldIndex = 9)]
        public int? SWorkerRoleCode { get; set; }
        
        #region IEquatable<StationWorkerOnExamDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StationWorkerOnExamDto)) return false;
            return Equals((StationWorkerOnExamDto) obj);
        }

        public bool Equals(StationWorkerOnExamDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region && 
                other.StationWorker.Equals(StationWorker) && 
                other.Station.Equals(Station) && 
                other.Exam == Exam;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + StationWorker.ToString().GetHashCode();
                result = result*37 + Station.ToString().GetHashCode();
                result = result*37 + Exam.GetHashCode();
                result = result*37 + Region.GetHashCode();
                return result;
            }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as StationWorkerOnExamDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.WorkerPositionCode == WorkerPositionCode, "Должность");
            result &= CheckChanges(Equals(other.AuditoriumCode, AuditoriumCode), "Аудитория");
            result &= CheckChanges(Equals(other.OrganizationRoleCode, OrganizationRoleCode), "Роль организатора вне аудитории");
            result &= CheckChanges(other.Station.Equals(Station), "ППЭ");
            result &= CheckChanges(other.StationWorker.Equals(StationWorker), "Работник ППЭ");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");
            result &= CheckChanges(other.SWorkerRoleCode == SWorkerRoleCode, "Код роли работника в ППЭ");

            return result ? 0 : 1;            
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID
        {
            get { return string.Format("({0}, {1}, {2})", StationWorkerUID, StationUID, Exam); }
            set { }
        }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string AuditoriumUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationWorkerUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationExamUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationExamAuditoryUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationWorkerOnStationUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string OrganizationRolesUID { get; set; }

        #endregion

	    public override ImportGroup ImportGroup { get { return ImportGroup.Planning; } }

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }
    }
}
