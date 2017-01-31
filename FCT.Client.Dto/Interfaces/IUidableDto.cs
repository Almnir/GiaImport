using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FCT.Client.Dto.Interfaces
{
    public interface IUidableDto
    {
        [XmlIgnore] string UID { get; set; }
    }
}
