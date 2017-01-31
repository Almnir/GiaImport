using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
	[Serializable]
    [Description("Рассадка участников")]
    [BulkTable("rbd_ParticipantsExamPStation", "ParticipantsExamPlacesOnStation", RootTagName = "ArrayOfParticipantsExamPlacesOnStationDto")]
    public class ParticipantsExamPlacesOnStationDto : RegionDtoBase, IEquatable<ParticipantsExamPlacesOnStationDto>,
        IDtoWithStationExam, IDtoWithAuditorium, IUidableDto, IDtoCollectorAccepter
	{
        [BulkColumn("Region")]
	    [XmlElement]
	    public override int Region { get; set; }

        [BulkColumn("PExamPlacesOnStationID")]
		public override Guid DtoID { get; set; }

        [XmlIgnore]private AuditoriumSurrogateKey _auditoriumSurrogateKey;
        [XmlIgnore]public AuditoriumSurrogateKey AuditoriumSurrogateKey
        {
            get { return _auditoriumSurrogateKey ?? (_auditoriumSurrogateKey = new AuditoriumSurrogateKey(Station, AuditoriumCode)); }
        }
        /* поиск аудитории по коду + ппэ */
        [XmlIgnore]
        private string _auditoriumCode;
        public string AuditoriumCode
        {
            get { return _auditoriumCode; }
            set { _auditoriumCode = value.ToAuditoriumCodeFormat(); }
        }

        public Guid Station { get; set; }
        public Guid Participant { get; set; }
        public int Exam { get; set; }
        [BulkColumn("IsManual")]
        [XmlIgnore] public bool IsManual { get; set; }

        [XmlElement("IsManual")]
        public string IsManualSerialize
        {
            get { return IsManual ? "1" : "0"; }
            set { IsManual = XmlConvert.ToBoolean(value); }
        }

	    [Description("Участник")]
	    [XmlIgnore] public string ParticipantName 
        { get { return ParticipantExamOnStationDto.With(c => c.ParticipantExamDto).Return(c => c.ParticipantName, ParticipantUID); } }

        [Description("Наименование ППЭ")]
        [XmlIgnore] public string StationName { get { return StationExamDto.With(x => x.StationDto).Return(x => x.ToString(), StationUID); } }

        [Description("Аудитория")]
        [XmlIgnore]public string AuditoriumName { get { return AuditoriumDto.Return(x => x.ToString(), AuditoriumUID); } }

        [Description("Ряд")]
        public int Row { get; set; }

        [Description("Место")]
        public int Col { get; set; }
        
        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return StationExamDto.With(x => x.ExamDto).Return(x => x.ToString(), Convert.ToString(Exam)); } }

        [XmlIgnore][Description("Принцип рассадки")]
        public string IsManualName { get { return IsManual ? "Ручная" : "Автоматическая"; } }

        #region NonSerializable

        [XmlIgnore] public PlacesDto PlaceDto { get; set; }
        [XmlIgnore] public AuditoriumsDto AuditoriumDto { get; set; }
        [XmlIgnore] public StationExamAuditoryDto StationExamAuditoryDto { get; set; }
        [XmlIgnore] public StationsExamsDto StationExamDto { get; set; }
        [XmlIgnore] public ParticipantsExamsOnStationDto ParticipantExamOnStationDto { get; set; }
        [XmlIgnore] public int IsManualProperty { set { IsManual = Convert.ToBoolean(value); } }

        [BulkColumn("StationExamAuditoryID")]
        public Guid StationExamAuditoryId { get { return StationExamAuditoryDto != null ? StationExamAuditoryDto.DtoID : Guid.Empty; } set {} }
        [BulkColumn("ParticipantsExamsOnStationID")]
        public Guid ParticipantExamOnStationId { get { return ParticipantExamOnStationDto != null ? ParticipantExamOnStationDto.DtoID : Guid.Empty; } set {} }
        [BulkColumn("StationsExamsID")]
        public Guid StationExamId { get { return StationExamDto != null ? StationExamDto.DtoID : Guid.Empty; } set {} }
        [BulkColumn("PlacesID")]
        public Guid PlaceId { get { return PlaceDto != null ? PlaceDto.DtoID : Guid.Empty; } set {} }
        
        //[XmlElement("Auditorium")]
        [BulkColumn("AuditoriumID")]
        public Guid AuditoriumId { get { return AuditoriumDto != null ? AuditoriumDto.DtoID : Guid.Empty; } set {} }

        public int RegistrationCode { get; set; }

        private DateTime _createDate;
        [Description("Дата создания")]
        public virtual DateTime? CreateDate
        {
            get { return _createDate; }
            set
            {
                if (value != null) _createDate = DateTime.SpecifyKind(value.Value.Date, DateTimeKind.Unspecified);
                else _createDate = DateTime.SpecifyKind(DateTime.Now.Date, DateTimeKind.Unspecified);
            }
        }

        private DateTime _updateDate;
        [Description("Дата изменения")]
		public virtual DateTime? UpdateDate
        {
            get { return _updateDate; }
            set
            {
                if (value != null) _updateDate = DateTime.SpecifyKind(value.Value.Date, DateTimeKind.Unspecified);
                else _updateDate = DateTime.SpecifyKind(DateTime.Now.Date, DateTimeKind.Unspecified);
            }
        }

	    [XmlIgnore] public ParticipantsExamsDto Dirty_ParticipantExam
	    {
	        get { return new ParticipantsExamsDto {Participant = Participant, Exam = Exam, Region = Region}; }
	    }

	    [XmlIgnore] public StationsExamsDto Dirty_StationExam
	    {
	        get { return new StationsExamsDto {Station = Station, Exam = Exam, Region = Region}; }
	    }

	    [XmlIgnore] public PlacesDto Dirty_Place
	    {
	        get 
            { 
                return new PlacesDto
	            {
	                AuditoriumCode = AuditoriumCode, 
                    Station = Station,
                    Row = Row, 
                    Col = Col, 
                    Region = Region
	            }; 
            }
	    }

        public delegate bool FuncIntToBool(int exam);

        public virtual StationExamAuditoryDto GetDirtyStationExamAuditory(Func<int,bool> isExamVoice)
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

	    [XmlIgnore] public ParticipantsExamsOnStationDto Dirty_ParticipantExamOnStation
	    {
	        get { return new ParticipantsExamsOnStationDto {Participant = Participant, Exam = Exam, Region = Region}; }
	    }

        [XmlIgnore]
        public AuditoriumsDto Dirty_Auditorium
        {
            get { return new AuditoriumsDto { Station = Station, AuditoriumCode = AuditoriumCode }; }
        }
        #endregion

        #region IEquatable<ParticipantsExamPlacesOnStationDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ParticipantsExamPlacesOnStationDto)) return false;
            return Equals((ParticipantsExamPlacesOnStationDto) obj);
        }

	    public bool Equals(ParticipantsExamPlacesOnStationDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return 
                other.Region == Region && 
                other.Participant.Equals(Participant) && 
                other.Exam == Exam;
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
                int result = 17;
                result = result*37 + Region.GetHashCode();
                result = result*37 + Participant.ToString().GetHashCode();
                result = result*37 + Exam.GetHashCode();
                return result;
	        }
	    }

        public override int CompareTo(object obj)
        {
            var other = obj as ParticipantsExamPlacesOnStationDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;
            
            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Station.Equals(Station), "ППЭ");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.Participant.Equals(Participant), "Участник");
            result &= CheckChanges(other.AuditoriumCode.Equals(AuditoriumCode), "Аудитория");
            result &= CheckChanges(other.Row == Row, "Ряд");
            result &= CheckChanges(other.Col == Col, "Место");
            result &= CheckChanges(other.IsManual.Equals(IsManual), "Ручная рассадка");
            result &= CheckChanges(other.RegistrationCode == RegistrationCode, "Код рассадки");
            return result ? 0 : 1;
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string PlaceUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string AuditoriumUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string ParticipantUID { get; set; }
       
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
