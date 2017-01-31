using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип улицы")]
    public class StreetTypeDto : DtoBase, IEquatable<StreetTypeDto>
    {
        public int StreetTypeID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int StreetTypeCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string StreetTypeName { get; set; }

        [CsvColumn(Name = "Краткое наименование", FieldIndex = 3)]
        public string StreetTypeShName { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<StreetTypeDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(StreetTypeDto)) return false;
            return Equals((StreetTypeDto)obj);
        }

        public bool Equals(StreetTypeDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.StreetTypeID == StreetTypeID;
        }

        public override int GetHashCode()
        {
            unchecked { return StreetTypeID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as StreetTypeDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.StreetTypeID == StreetTypeID, "Id");
            result &= CheckChanges(other.StreetTypeCode == StreetTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.StreetTypeName, StreetTypeName), "Наименование");
            result &= CheckChanges(StringEquals(other.StreetTypeShName, StreetTypeShName), "Краткое наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion

        public override string ToString()
        {
            return StreetTypeName;
        }
    }
}
