using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Предмет по сокр. программе участника")]
    public class ParticipantsSubjectDto : RegionDtoBase, IEquatable<ParticipantsSubjectDto>, IDtoWithSubject
    {
        [XmlElement]
        public override int Region { get; set; }

        #region NonSerializable

        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }
        [XmlIgnore] public SubjectsDto SubjectDto { get; set; }

        #endregion

        public Guid Participant { get; set; }
        public int SubjectCode { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName{ get { return ParticipantDto.Return(x => x.FIO, "---"); } }

        [Description("Предмет")]
        [XmlIgnore] public string SubjectName { get { return SubjectDto.Return(c => c.SubjectName, "---"); } }

        #region IEquatable<ParticipantsSubjectDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ParticipantsSubjectDto)) return false;
            return Equals((ParticipantsSubjectDto) obj);
        }

        public bool Equals(ParticipantsSubjectDto other)
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
    }
}
