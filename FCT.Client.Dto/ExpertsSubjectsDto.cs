using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Специализация эксперта")]
    public class ExpertsSubjectsDto : DtoBase, IEquatable<ExpertsSubjectsDto>, IDtoWithSubject, IDtoWithExpert
    {
        [XmlIgnore] public string RegionName  { get { return RegionDto.Return(x => x.RegionName, "---"); } }

        [Description("Эксперт")]
        [XmlIgnore] public string ExpertName  { get { return ExpertDto.Return(x => x.FIO, "---"); } }

        [Description("Предмет")]
        [XmlIgnore]public string SubjectName { get { return SubjectDto.Return(c => c.SubjectName, "---"); } }

        #region NonSerializable

        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public ExpertsDto ExpertDto { get; set; }
        [XmlIgnore] public SubjectsDto SubjectDto { get; set; }

        #endregion

		public Guid Expert { get; set; }
		public int SubjectCode { get; set; }
		public int Region { get; set; }

        #region IEquatable<ExpertsSubjectsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ExpertsSubjectsDto)) return false;
            return Equals((ExpertsSubjectsDto) obj);
        }

        public bool Equals(ExpertsSubjectsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Expert.Equals(Expert) && 
                other.SubjectCode == SubjectCode &&
                other.Region == Region;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Expert.ToString().GetHashCode();
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
