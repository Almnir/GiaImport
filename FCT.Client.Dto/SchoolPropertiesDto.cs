using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип собственности")]
    public class SchoolPropertiesDto : DtoBase, IEquatable<SchoolPropertiesDto>
    {
        public short SchoolPropertyID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public short SchoolPropertyCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string SchoolPropertyName { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<SchoolPropertiesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SchoolPropertiesDto)) return false;
            return Equals((SchoolPropertiesDto) obj);
        }

        public bool Equals(SchoolPropertiesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.SchoolPropertyID == SchoolPropertyID;
        }

        public override int GetHashCode()
        {
            unchecked { return SchoolPropertyID.GetHashCode(); }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as SchoolPropertiesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.SchoolPropertyID == SchoolPropertyID, "Id");
            result &= CheckChanges(other.SchoolPropertyCode == SchoolPropertyCode, "Код");
            result &= CheckChanges(StringEquals(other.SchoolPropertyName, SchoolPropertyName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
     }
}
