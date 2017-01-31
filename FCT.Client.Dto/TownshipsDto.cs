using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FCT.Client.Dto
{
    [Serializable][Description("Район")]
    public class TownshipsDto : RegionDtoBase, IEquatable<TownshipsDto>
    {
        public int TownshipID { get; set; }
        public int OCATO { get; set; }
        public string TownshipName { get; set; }

        [XmlElement("REGION")]
        public override int Region { get; set; }

        public int? TimeZone { get; set; }

        #region IEquatable<TownshipsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TownshipsDto)) return false;
            return Equals((TownshipsDto) obj);
        }

        public bool Equals(TownshipsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TownshipID == TownshipID;
        }

        public override int GetHashCode()
        {
            unchecked { return TownshipID * 397; }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as TownshipsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.TownshipID == TownshipID, "Id");
            result &= CheckChanges(StringEquals(other.TownshipName, TownshipName), "Наименование");
            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.OCATO == OCATO, "ОКАТО");
            result &= CheckChanges(other.TimeZone == TimeZone, "Часовой пояс");

            return result ? 0 : 1;
        }

        #endregion

        public override string ToString()
        {
            return TownshipName;
        }
    }
}
