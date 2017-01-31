using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Часовые пояса")]
    public class TimeZonesDto : DtoBase, IEquatable<TimeZonesDto>
    {
        public int TimeZoneID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int TimeZoneCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string TimeZoneName { get; set; }

        [CsvColumn(Name = "Сдвиг", FieldIndex = 3)]
        public decimal TimeZoneNum { get; set; }
        
        public int SortBy { get; set; }

        #region IEquatable<TimeZonesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TimeZonesDto)) return false;
            return Equals((TimeZonesDto)obj);
        }

        public bool Equals(TimeZonesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TimeZoneID == TimeZoneID;
        }

        public override int GetHashCode()
        {
            unchecked { return TimeZoneID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as TimeZonesDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.TimeZoneID == TimeZoneID, "Id");
            result &= CheckChanges(other.TimeZoneCode == TimeZoneCode, "Код");
            result &= CheckChanges(StringEquals(other.TimeZoneName, TimeZoneName), "Наименование");
            result &= CheckChanges(other.TimeZoneNum == TimeZoneNum, "Сдвиг");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
