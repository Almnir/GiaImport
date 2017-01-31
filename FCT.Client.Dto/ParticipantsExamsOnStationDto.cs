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
	[Serializable][Description("Назначение участника на экзамен в ППЭ")]
    [BulkTable("rbd_ParticipantsExamsOnStation", "ParticipantsExamsOnStation", RootTagName = "ArrayOfParticipantsExamsOnStationDto")]
    public class ParticipantsExamsOnStationDto : DtoCreateDateBase, IEquatable<ParticipantsExamsOnStationDto>, IDtoCollectorAccepter, IUidableDto, 
        IDtoWithExam, IDtoWithStationExam, IDtoWithParticipantExam
	{
        [BulkColumn("ParticipantsExamsOnStationID")]
        public override Guid DtoID { get; set; }

        [BulkColumn]
        [XmlElement]
		public override int Region { get; set; }

        [CsvColumn(Name = "Guid участника", FieldIndex = 1)]
		public Guid Participant { get; set; }

        [XmlIgnore][BulkColumn("ParticipantsExamsID")] public Guid ParticipantsExams { get; set; }
        [XmlIgnore][BulkColumn("StationsExamsID")] public Guid StationsExams { get; set; }
        
        [BulkColumn("ExamGlobalID")]
	    [CsvColumn(Name = "Код дня экзамена", FieldIndex = 2)]
		public int Exam { get; set; }

        [CsvColumn(Name = "Guid ППЭ", FieldIndex = 3)]
		public Guid Station { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 4, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 5, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

		public Guid PExamPlacesOnStationId { get; set; }
        [XmlIgnore] public bool LockOnStation { get; set; }

        [XmlIgnore]
        public int LockOnStationInt { set { LockOnStation = (value > 0); } }

        [XmlElement("LockOnStation")]
        public string LockOnStationSerialize
        {
            get { return LockOnStation ? "1" : "0"; }
            set { LockOnStation = XmlConvert.ToBoolean(value); }
        }

        #region NonSerializable

        [Description("МСУ участника")]
	    [XmlIgnore] public string ParticipantGovernment
	    {
	        get { return ParticipantExamDto.With(c => c.ParticipantDto).With(c => 
                c.SchoolRegistrationDto).With(c => c.GovernmentDto).Return(c => c.ToString(), "---"); }
	    }

        [XmlIgnore]
        public string ParticipantGovernmentCode
        {
            get
            {
                return ParticipantExamDto.With(c => c.ParticipantDto).With(c =>
                    c.SchoolRegistrationDto).With(c => c.GovernmentDto).Return(c => c.GovernmentCode.ToString(), "---");
            }
        }

        [XmlIgnore]
        public string ParticipantSchoolCode
        {
            get
            {
                return ParticipantExamDto.With(c => c.ParticipantDto).With(c =>
                    c.SchoolRegistrationDto).Return(c => c.SchoolCode.ToString(), "---");
            }
        }

	    [Description("Участник")]
	    [XmlIgnore] public string ParticipantName
	    {
	        get { return ParticipantExamDto.With(c => c.ParticipantDto).Return(c => c.FIO, "---"); }
	    }

        [Description("МСУ ППЭ")]
	    [XmlIgnore] public string StationGovernment
	    {
	        get { return StationExamDto.With(c => c.StationDto).With(c => c.GovernmentDto).Return(c => c.ToString(), "---"); }
	    }

        [Description("Наименование ППЭ")]
        [XmlIgnore] public string StationName { get { return StationExamDto.With(x => x.StationDto).Return(x => x.ToString(), "---"); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return ExamDto.Return(x => x.ToString(), "---"); } }

        [XmlIgnore] public ExamsDto ExamDto { get; set; }
        [XmlIgnore] public ParticipantsExamsDto ParticipantExamDto { get; set; }
        [XmlIgnore] public StationsExamsDto StationExamDto { get; set; }

	    [XmlIgnore] public ParticipantsExamsDto Dirty_ParticipantExam
	    {
	        get { return new ParticipantsExamsDto {Participant = Participant, Exam = Exam, Region = Region}; }
	    }

	    [XmlIgnore] public StationsExamsDto Dirty_StationExam
	    {
	        get { return new StationsExamsDto {Station = Station, Exam = Exam, Region = Region}; }
	    }

        public Guid ParticipantExamId { get { return ParticipantExamDto != null ? ParticipantExamDto.DtoID : Guid.Empty; } set {} }
        public Guid StationExamId { get { return StationExamDto != null ? StationExamDto.DtoID : Guid.Empty; } set {} }

        #endregion

        #region IEquatable<ParticipantsExamsOnStationDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ParticipantsExamsOnStationDto)) return false;
            return Equals((ParticipantsExamsOnStationDto) obj);
        }

        public bool Equals(ParticipantsExamsOnStationDto other)
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
            var other = obj as ParticipantsExamsOnStationDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

	        result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Participant.Equals(Participant), "Участник");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.Station == Station, "ППЭ");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;	        
	    }

	    #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID
        {
            get { return string.Format("({0}, {1}, {2})", ParticipantUID, StationUID, Exam); }
            set { }
        }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string ParticipantUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationUID { get; set; }

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
