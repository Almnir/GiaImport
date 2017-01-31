using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;

namespace FCT.Client.Dto
{
	[Serializable][Description("Назначение участника на зачет")]
    public class ParticipantsExamsOnSchoolDto : DtoCreateDateBase, IEquatable<ParticipantsExamsOnSchoolDto>, IDtoWithExam, IDtoWithParticipantExam, IDtoWithSchool
	{
        [XmlElement]
        public override int Region { get; set; }

        public override Guid DtoID { get; set; }

		public Guid Participant { get; set; }
       
		public int Exam { get; set; }

		public Guid School { get; set; }

        [XmlIgnore]
        public Guid ParticipantExamId { get { return ParticipantExamDto != null ? ParticipantExamDto.DtoID : Guid.Empty; } set { } }

        #region NonSerializable

        [Description("МСУ участника")]
	    [XmlIgnore] public string ParticipantGovernment
	    {
	        get { return ParticipantExamDto.With(c => c.ParticipantDto).With(c => 
                c.SchoolRegistrationDto).With(c => c.GovernmentDto).Return(c => c.ToString(), "---"); }
	    }

	    [Description("Участник")]
	    [XmlIgnore] public string ParticipantName
	    {
	        get { return ParticipantExamDto.With(c => c.ParticipantDto).Return(c => c.FIO, "---"); }
	    }

        [Description("МСУ ОО")]
        [XmlIgnore]public string SchoolGovernment
	    {
	        get { return SchoolDto.With(c => c.GovernmentDto).Return(c => c.ToString(), "---"); }
	    }

        [Description("Наименование ОО")]
        [XmlIgnore] public string SchoolName { get { return SchoolDto.With(x => x.SchoolName).Return(x => x.ToString(), "---"); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return ExamDto.Return(x => x.ToString(), "---"); } }

        [XmlIgnore]
        public ParticipantsExamsDto Dirty_ParticipantExam
        {
            get { return new ParticipantsExamsDto { Participant = Participant, Exam = Exam, Region = Region }; }
        }

        [XmlIgnore] public ExamsDto ExamDto { get; set; }
        [XmlIgnore] public ParticipantsExamsDto ParticipantExamDto { get; set; }
        [XmlIgnore] public SchoolsDto SchoolDto { get; set; }

        #endregion

        #region IEquatable<ParticipantsExamsOnSchoolDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ParticipantsExamsOnSchoolDto)) return false;
            return Equals((ParticipantsExamsOnSchoolDto)obj);
        }

        public bool Equals(ParticipantsExamsOnSchoolDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.Region == Region &&
                other.Participant.Equals(Participant) &&
                other.Exam == Exam &&
                other.School.Equals(School);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Region.GetHashCode();
                result = result*37 + Participant.ToString().GetHashCode();
                result = result*37 + Exam.GetHashCode();
                result = result * 37 + School.ToString().GetHashCode();
                return result;
            }
        }

	    public override int CompareTo(object obj)
	    {
            var other = obj as ParticipantsExamsOnSchoolDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

	        result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Participant.Equals(Participant), "Участник");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.School == School, "ОО");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;	        
	    }

	    #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
	}
}
