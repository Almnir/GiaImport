using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Реквизиты актов ОИВ")]
    public class RegionRequisitesDto : DtoCreateDateBase, IEquatable<RegionRequisitesDto>
    {
        [XmlElement]
        public override int Region { get; set; }

        public RequisiteTypes RequisiteType { get; set; }
        public int RequisiteTypeInt 
        {
            get { return (int)RequisiteType; }
            set { RequisiteType = (RequisiteTypes)value; }
        }
        public string RequisiteDetail { get; set; }

        #region IEquatable<RegionsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(RegionRequisitesDto)) return false;
            return Equals((RegionRequisitesDto)obj);
        }

        public bool Equals(RegionRequisitesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region &&
                other.RequisiteType == RequisiteType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 37 + Region.GetHashCode(); 
                result = result * 37 + RequisiteType.GetHashCode();
                return result;
            }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as RegionRequisitesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;
            result &= CheckChanges(other.Region == Region, "Код субъекта РФ, в котором находится ОО");
            result &= CheckChanges(other.RequisiteType == RequisiteType, "Тип акта");
            result &= CheckChanges(StringEquals(other.RequisiteDetail, RequisiteDetail), "Реквизиты акта");
            return result ? 0 : 1;
        }

        #endregion
    }
}
