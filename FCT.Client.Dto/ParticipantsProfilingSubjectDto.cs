using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Профильные предметы участника")]
    public class ParticipantsProfilingSubjectDto : RegionDtoBase, IEquatable<ParticipantsProfilingSubjectDto>, IDtoWithSubject
    {
        [XmlElement]
        public override int Region { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, "---"); } }

        [Description("Предмет")]
        [XmlIgnore]public string SubjectName { get { return SubjectDto.Return(c => c.SubjectName, "---"); } }

        #region NonSerializable

        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }
        [XmlIgnore] public SubjectsDto SubjectDto { get; set; }

        #endregion

        public Guid Participant { get; set; }
        public int SubjectCode { get; set; }

        #region IEquatable<ParticipantsSubjectDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ParticipantsProfilingSubjectDto)) return false;
            return Equals((ParticipantsProfilingSubjectDto)obj);
        }

        public bool Equals(ParticipantsProfilingSubjectDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region && 
                other.SubjectCode == SubjectCode && 
                other.Participant.Equals(Participant);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Participant.ToString().GetHashCode();
                result = result*37 + SubjectCode.GetHashCode();
                result = result*37 + Region.GetHashCode();
                return result;
            }
        }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}