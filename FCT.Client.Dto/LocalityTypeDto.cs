using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип населенного пункта")]
    public class LocalityTypeDto : DtoBase, IEquatable<LocalityTypeDto>
    {
        public int LocalityTypeID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int LocalityTypeCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string LocalityTypeName { get; set; }

        [CsvColumn(Name = "Краткое наименование", FieldIndex = 3)]
        public string LocalityTypeShName { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<LocalityTypeDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(LocalityTypeDto)) return false;
            return Equals((LocalityTypeDto)obj);
        }

        public bool Equals(LocalityTypeDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.LocalityTypeID == LocalityTypeID;
        }

        public override int GetHashCode()
        {
            unchecked { return LocalityTypeID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as LocalityTypeDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.LocalityTypeID == LocalityTypeID, "Id");
            result &= CheckChanges(other.LocalityTypeCode == LocalityTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.LocalityTypeName, LocalityTypeName), "Наименование");
            result &= CheckChanges(StringEquals(other.LocalityTypeShName, LocalityTypeShName), "Краткое наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion

        public override string ToString()
        {
            return LocalityTypeName;
        }
    }
}
