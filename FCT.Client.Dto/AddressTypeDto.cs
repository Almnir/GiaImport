using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип адреса")]
    public class AddressTypeDto : DtoBase, IEquatable<AddressTypeDto>
    {
        public int AddressTypeID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int AddressTypeCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string AddressTypeName { get; set; }
        
        public int SortBy { get; set; }

        #region IEquatable<AddressTypeDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AddressTypeDto)) return false;
            return Equals((AddressTypeDto)obj);
        }

        public bool Equals(AddressTypeDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.AddressTypeID == AddressTypeID;
        }

        public override int GetHashCode()
        {
            unchecked { return AddressTypeID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as AddressTypeDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.AddressTypeID == AddressTypeID, "Id");
            result &= CheckChanges(other.AddressTypeCode == AddressTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.AddressTypeName, AddressTypeName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion

        public override string ToString()
        {
            return AddressTypeName;
        }
    }
}
