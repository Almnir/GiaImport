using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Enums;
using System.Xml;

namespace FCT.Client.Dto
{
	[Serializable][Description("Экзамен участника")]
    [BulkTable("rbd_ParticipantsExams", "ParticipantsExams", RootTagName = "ArrayOfParticipantsExamsDto")]
    public class ParticipantsExamsDto : DtoCreateDateBase, IEquatable<ParticipantsExamsDto>, IDtoCollectorAccepter, IUidableDto, IDtoWithExam
	{
        [BulkColumn("ParticipantsExamsID")]
        public override Guid DtoID { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, ParticipantUID); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return ExamDto.Return(x => x.ToString(), Exam.ToString()); } }

	    [Description("Форма ГИА")]
	    [XmlIgnore] public string GiaName { get { return ExamDto.Return(x => ((TestTypeCode)x.TestTypeCode).GetDescription(), "---"); } }

        #region NonSerializable

        [XmlIgnore] public ExamsDto ExamDto { get; set; }
        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }

        #endregion

        [BulkColumn("ParticipantID")]
		[CsvColumn(Name = "Guid участника", FieldIndex = 1)]
		public Guid Participant { get; set; }

        [BulkColumn("ExamGlobalID")]
		[CsvColumn(Name = "Код дня экзамена", FieldIndex = 2)]
		public int Exam { get; set; }

        [XmlElement]
        [BulkColumn("REGION")]
		public override int Region { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 3, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 4, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        [BulkColumn]
        [CsvColumn(Name = "", FieldIndex = 5, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime ImportCreateDate { get { return base.ImportCreateDate; } set { base.ImportCreateDate = value; } }

        [BulkColumn]
        [CsvColumn(Name = "", FieldIndex = 6, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime ImportUpdateDate { get { return base.ImportUpdateDate; } set { base.ImportUpdateDate = value; } }

        [BulkColumn]
        [XmlElement("IsDeleted")]
        public string IsDeletedSerialize
        {
            get { return IsDeleted ? "1" : "0"; }
            set { IsDeleted = XmlConvert.ToBoolean(value); }
        }

	    [XmlIgnore] public int? ExamPassStatusProperty { set { if (value != null) ExamPassStatusFlag = (ExamPassType)value; } }
        [XmlIgnore] public ExamPassType? ExamPassStatusFlag { get; set; }
        public int? ExamPassStatus { get { return ExamPassStatusFlag.HasValue ? (int)ExamPassStatusFlag.Value : (int?)null; } set {} }

        #region IEquatable<ParticipantsExamsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ParticipantsExamsDto);
        }

	    public bool Equals(ParticipantsExamsDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;

            bool result = true;

	        result &= other.Region == Region;
            result &= other.Exam == Exam;
            result &= other.Participant.Equals(Participant);

	        return result;           
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

	    #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID
        {
            get { return string.Format("({0}, {1})", ParticipantUID, Exam); }
            set { }
        }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string ParticipantUID { get; set; }

        #endregion

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
