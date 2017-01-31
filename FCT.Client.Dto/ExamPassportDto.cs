using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
	[Serializable]
    [Description("Сведения о уведомлениях участников ГИА(ЕГЭ)")]
    public class ExamPassportDto : RegionDtoBase, IEquatable<ExamPassportDto>, IDtoWithExam, IDtoWithParticipantExam
	{
        [XmlElement]
        public override int Region { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, "---"); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return ExamDto.Return(x => x.ToString(), "---"); } }

        #region NonSerializable

        [XmlIgnore] public ExamsDto ExamDto { get; set; }
        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }
        [XmlIgnore] public ParticipantsExamsDto ParticipantExamDto { get; set; }
        public Guid ParticipantExamId { get { return ParticipantExamDto != null ? ParticipantExamDto.DtoID : Guid.Empty; } set { } }

        [XmlIgnore]
        public ParticipantsExamsDto Dirty_ParticipantExam
        {
            get { return new ParticipantsExamsDto { Participant = Participant, Exam = Exam, Region = Region}; }
        }

        #endregion

        public int ParticipantsExamsPropertyId { get; set; }
		public Guid Participant { get; set; }
		public int Exam { get; set; }

        [XmlIgnore] public ExamPassType ExamPassStatus { get; set; }
        public int ExamPassStatusInt
        {
            set { ExamPassStatus = (ExamPassType)value; }
            get { return (int)ExamPassStatus; }
        }
        [Description("Состояние уведомлений участников")]
        [XmlIgnore] public string ExamPassStatusName { get { return ExamPassStatus.GetDescription(); } }

        [Description("Дата печати")]
        public DateTime? ExamPassLastPrintDate { get; set; }

        #region IEquatable<ParticipantsExamsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ExamPassportDto);
        }

	    public bool Equals(ExamPassportDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;

            bool result = true;

            result &= other.Exam == Exam;
            result &= other.ExamPassStatusInt == ExamPassStatusInt;
            result &= other.Participant.Equals(Participant);

	        return result;           
        }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
                int result = 17;
                result = result*37 + Participant.ToString().GetHashCode();
                result = result*37 + Exam.GetHashCode();
                result = result*37 + ExamPassStatusInt.GetHashCode();
                return result;
	        }
	    }

        public override int CompareTo(object obj)
        {
            var other = obj as ExamPassportDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Participant.Equals(Participant), "Участник");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.ExamPassStatus == ExamPassStatus, "Состояние уведомлений участников");
            result &= CheckChanges(other.ExamPassLastPrintDate == ExamPassLastPrintDate, "Дата печати");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;
        }

        #endregion
    }
}
