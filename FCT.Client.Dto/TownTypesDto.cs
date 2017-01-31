using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип населенного пункта")]
    public class TownTypesDto : DtoBase, IEquatable<TownTypesDto>
    {
        public int TownTypeID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int TownTypeCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string TownTypeName { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<TownTypesDto> Members
              
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TownTypesDto)) return false;
            return Equals((TownTypesDto) obj);
        }

        public bool Equals(TownTypesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TownTypeID == TownTypeID;
        }

        public override int GetHashCode()
        {
            unchecked { return TownTypeID * 397; }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as TownTypesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.TownTypeID == TownTypeID, "Id");
            result &= CheckChanges(other.TownTypeCode == TownTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.TownTypeName, TownTypeName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
