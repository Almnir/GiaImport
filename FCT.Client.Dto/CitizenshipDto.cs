using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Гражданство")]
    public class CitizenshipDto : DtoBase, IEquatable<CitizenshipDto>
    {
        public int CitizenshipID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int CitizenshipCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string CitizenshipName { get; set; }
        
        public int SortBy { get; set; }

        #region IEquatable<CitizenshipDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CitizenshipDto)) return false;
            return Equals((CitizenshipDto)obj);
        }

        public bool Equals(CitizenshipDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.CitizenshipID == CitizenshipID;
        }

        public override int GetHashCode()
        {
            unchecked { return CitizenshipID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as CitizenshipDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.CitizenshipID == CitizenshipID, "Id");
            result &= CheckChanges(other.CitizenshipCode == CitizenshipCode, "Код");
            result &= CheckChanges(StringEquals(other.CitizenshipName, CitizenshipName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
