using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto
{
    [Serializable][Description("Регион")]
    public class RegionsDto : DtoBase, IEquatable<RegionsDto>
    {
        public int REGION { get; set; }
        public string RegionName { get; set; }

        #region IEquatable<RegionsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RegionsDto)) return false;
            return Equals((RegionsDto) obj);
        }

        public bool Equals(RegionsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.REGION == REGION;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return REGION*397;
            }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as RegionsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.REGION == REGION, "Код");
            result &= CheckChanges(StringEquals(other.RegionName, RegionName), "Наименование");

            return result ? 0 : 1;
        }

        #endregion
    }
}
