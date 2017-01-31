using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto.Common
{
    public class GovernmentKeyDto : DtoCreateDateBase
    {
        public int GovernmentCode { get; set; }
        public string GovernmentName { get; set; }
        
        [XmlIgnore]
        public override int Region { get; set; }
    }

    public class SchoolKeyDto : DtoCreateDateBase
    {
        public int SchoolCode { get; set; }
        public string SchoolName { get; set; }

        [XmlIgnore]
        public override int Region { get; set; }
    }
}
