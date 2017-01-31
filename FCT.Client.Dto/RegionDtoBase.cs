using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD;

namespace FCT.Client.Dto
{
    public abstract class RegionDtoBase : DtoBase, IDtoWithRegion
    {
        [XmlIgnore]
        public abstract int Region { get; set; }

        [XmlIgnore]
        public RegionsDto RegionDto { get; set; }

        [XmlIgnore]
        public virtual string RegionName { get { return RegionDto.Return(x => x.RegionName, "---"); } }
    }
}