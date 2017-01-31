using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;

namespace FCT.Client.Dto
{
    [Serializable][Description("Специализация аудитории")]
    public class AuditoriumsSubjectsDto : RegionDtoBase, IEquatable<AuditoriumsSubjectsDto>, IDtoWithSubject, IDtoWithAuditorium
    {
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

        #region NonSerializable

        [XmlIgnore] public AuditoriumsDto AuditoriumDto { get; set; }
        [XmlIgnore] public SubjectsDto SubjectDto { get; set; }

        [XmlIgnore]
        public AuditoriumsDto Dirty_Auditorium
        {
            get { return new AuditoriumsDto { Station = Station, AuditoriumCode = AuditoriumCode }; }
        }

        [XmlElement("Auditorium")]
        public Guid AuditoriumId { get { return AuditoriumDto != null ? AuditoriumDto.DtoID : Guid.Empty; } set { } }

        #endregion

        [XmlElement("REGION")]
        public override int Region { get; set; }

        public int SubjectCode { get; set; }

        [Description("Аудитория")]
        [XmlIgnore]public string AuditoriumName { get { return AuditoriumDto.Return(x => x.ToString(), "---"); } }

        [Description("Предмет")]
        [XmlIgnore]public string SubjectName { get { return SubjectDto.Return(c => c.SubjectName, "---"); } }

        #region IEquatable<AuditoriumsSubjectsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AuditoriumsSubjectsDto)) return false;
            return Equals((AuditoriumsSubjectsDto) obj);
        }

        public bool Equals(AuditoriumsSubjectsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.SubjectCode == SubjectCode &&
                other.AuditoriumSurrogateKey.Equals(AuditoriumSurrogateKey) &&
                other.Region.Equals(Region);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + SubjectCode.GetHashCode();
                result = result * 37 + AuditoriumSurrogateKey.GetHashCode();
                result = result * 37 + Region.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
