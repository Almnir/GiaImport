using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;

namespace FCT.Client.Dto
{
    /// <summary>
    /// ПРЕДМЕТЫ РАБОТНИКОВ ППЭ
    /// </summary>
    [Serializable]
    [Description("Предмет работника ППЭ")]
    public class StationWorkersSubjectsDto : DtoBase, IEquatable<StationWorkersSubjectsDto>, IDtoWithSubject, IDtoWithStationWorker
    {
        [XmlIgnore] public string RegionName { get { return RegionDto.Return(x => x.RegionName, "---"); } }

        [Description("Работник ППЭ")]
        [XmlIgnore] public string StationWorkerName { get { return StationWorkerDto.Return(x => x.FIO, "---"); } }

        [Description("Предмет")]
        [XmlIgnore]public string SubjectName { get { return SubjectDto.Return(c => c.SubjectName, "---"); } }

        [CsvColumn(Name = "Код общеобразовательного предмета", FieldIndex = 2)]
        [XmlElement("Subject")]
        public int SubjectCode { get; set; }
		
        [CsvColumn(Name = "Guid работника ППЭ", FieldIndex = 1)]
        public Guid StationWorker{ get; set; }

        public int Region { get; set; }

        #region NonSerialized

        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public StationWorkersDto StationWorkerDto { get; set; }
        [XmlIgnore] public SubjectsDto SubjectDto { get; set; }

        #endregion

        #region IEquatable<StationWorkersSubjectsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StationWorkersSubjectsDto)) return false;
            return Equals((StationWorkersSubjectsDto) obj);
        }

        public bool Equals(StationWorkersSubjectsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.StationWorker.Equals(StationWorker) && 
                other.Region == Region && 
                other.SubjectCode == SubjectCode;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + SubjectCode.GetHashCode();
                result = result*37 + StationWorker.ToString().GetHashCode();
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
