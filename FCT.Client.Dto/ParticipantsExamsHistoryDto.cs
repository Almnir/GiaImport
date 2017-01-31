using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
	[Serializable]
    [Description("История изменения экзаменов участников")]
    public class ParticipantsExamsHistoryDto : DtoCreateDateBase, IEquatable<ParticipantsExamsHistoryDto>, IDtoWithExam
	{
	    [XmlElement]
	    public override int Region { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, "---"); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return ExamDto.Return(x => x.ToString(), "---"); } }

        [Description("Блокировка")]
        [XmlIgnore] public string ActionTypeName { get { return ActionType.GetDescription(); } }

        #region NonSerializable

        [XmlIgnore] public ExamsDto ExamDto { get; set; }
        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }
        [XmlIgnore] public int ActionTypeProperty { set { ActionType = (PEActionType)value; } }

	    [XmlIgnore] public ParticipantsExamsDto Dirty_ParticipantExam
	    {
	        get { return new ParticipantsExamsDto {Participant = Participant, Exam = Exam, Region = Region}; }
	    }

        #endregion

        public int HistoryId { get; set; }
		public int Exam { get; set; }
        public Guid ParticipantExamId { get; set; }
		public Guid Participant { get; set; }
        
        [Description("Дата")]
        public DateTime HistoryDate { get; set; }
        public PEActionType ActionType { get; set; }

        #region IEquatable<ParticipantsExamsHistoryDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ParticipantsExamsHistoryDto);
        }

	    public bool Equals(ParticipantsExamsHistoryDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;

            bool result = true;

	        result &= other.Region == Region;
            result &= other.Exam == Exam;
            result &= other.ActionType == ActionType;
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
                result = result*37 + ActionType.GetHashCode();
                result = result*37 + Exam.GetHashCode();
                return result;
	        }
	    }

        #endregion

	    public override int CompareTo(object obj)
	    {
            var other = obj as ParticipantsExamsHistoryDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

	        result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Participant.Equals(Participant), "Участник");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.ActionType == ActionType, "Блокировка");
            result &= CheckChanges(other.HistoryDate == HistoryDate, "Дата");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;	        
	    }
	}
}
