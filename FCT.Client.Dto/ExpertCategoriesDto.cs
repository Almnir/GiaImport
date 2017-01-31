using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Категории экспертов")]
    public class ExpertCategoriesDto : DtoBase, IEquatable<ExpertCategoriesDto>
    {
        public int ExpertCategoryID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int ExpertCategoryCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string ExpertCategoryName { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<ExpertCategoriesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ExpertCategoriesDto)) return false;
            return Equals((ExpertCategoriesDto)obj);
        }

        public bool Equals(ExpertCategoriesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.ExpertCategoryID == ExpertCategoryID;
        }

        public override int GetHashCode()
        {
            unchecked { return ExpertCategoryID * 397; }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as ExpertCategoriesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.ExpertCategoryID == ExpertCategoryID, "Id");
            result &= CheckChanges(other.ExpertCategoryCode == ExpertCategoryCode, "Код");
            result &= CheckChanges(StringEquals(other.ExpertCategoryName, ExpertCategoryName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
