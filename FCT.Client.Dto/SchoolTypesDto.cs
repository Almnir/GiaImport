using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип школы")]
    public class SchoolTypesDto : DtoBase, IEquatable<SchoolTypesDto>
    {
        public int SchoolTypeID { get; set; }
        public virtual int SchoolTypeCode { get; set; }
        public virtual string SchoolTypeName { get; set; }
        public int SortBy { get; set; }

        #region IEquatable<SchoolTypesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SchoolTypesDto)) return false;
            return Equals((SchoolTypesDto) obj);
        }

        public bool Equals(SchoolTypesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.SchoolTypeID == SchoolTypeID;
        }

        public override int GetHashCode()
        {
            unchecked { return SchoolTypeID * 397; }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as SchoolTypesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.SchoolTypeID == SchoolTypeID, "Id");
            result &= CheckChanges(other.SchoolTypeCode == SchoolTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.SchoolTypeName, SchoolTypeName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
