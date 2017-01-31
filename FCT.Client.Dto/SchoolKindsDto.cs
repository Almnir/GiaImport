using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD.Client.Dto;

namespace FCT.Client.Dto
{
    [Serializable][Description("Вид школы")]
    public class SchoolKindsDto : DtoBase, IEquatable<SchoolKindsDto>
    {
        #region NonSerialized

        [XmlIgnore] public SchoolTypesDto SchoolTypeDto { get; set; }

        #endregion

        public short SchoolKindID { get; set; }
        public int SchoolKindCode { get; set; }
        public string SchoolKindName { get; set; }
        public int SchoolTypeID { get; set; }
        public int SortBy { get; set; }

        #region IEquatable<SchoolKindsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SchoolKindsDto)) return false;
            return Equals((SchoolKindsDto) obj);
        }

        public bool Equals(SchoolKindsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.SchoolKindID == SchoolKindID;
        }

        public override int GetHashCode()
        {
            unchecked { return SchoolKindID.GetHashCode(); }
        }    
    
        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as SchoolKindsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.SchoolKindID == SchoolKindID, "Id");
            result &= CheckChanges(other.SchoolKindCode == SchoolKindCode, "Код");
            result &= CheckChanges(StringEquals(other.SchoolKindName, SchoolKindName), "Наименование");
            result &= CheckChanges(other.SchoolTypeID == SchoolTypeID, "Тип школы");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
