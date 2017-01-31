using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD.Client.Dto;

namespace FCT.Client.Dto
{
    [Serializable][Description("Категория участника")]
	public class ParticipantCategoriesDto : DtoBase, IEquatable<ParticipantCategoriesDto>
    {
        public int CategoryID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int CategoryCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string CategoryName { get; set; }
        
        public int? SchoolType { get; set; }

        public int SortBy { get; set; }

        #region NonSerializable

        [XmlIgnore] public SchoolTypesDto SchoolTypeDto { get; set; }

        #endregion

        #region IEquatable<ParticipantCategoriesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ParticipantCategoriesDto)) return false;
            return Equals((ParticipantCategoriesDto) obj);
        }

        public bool Equals(ParticipantCategoriesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.CategoryID == CategoryID;
        }

        public override int GetHashCode()
        {
            unchecked { return CategoryID * 397; }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as ParticipantCategoriesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.CategoryID == CategoryID, "Id");
            result &= CheckChanges(other.CategoryCode == CategoryCode, "Код");
            result &= CheckChanges(StringEquals(other.CategoryName, CategoryName), "Наименование");
            result &= CheckChanges(other.SchoolType == SchoolType, "Тип школы");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
