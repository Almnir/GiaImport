using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип здания")]
    public class BuildingTypeDto : DtoBase, IEquatable<BuildingTypeDto>
    {
        public int BuildingTypeID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int BuildingTypeCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string BuildingTypeName { get; set; }

        [CsvColumn(Name = "Краткое наименование", FieldIndex = 3)]
        public string BuildingTypeShName { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<BuildingTypeDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(BuildingTypeDto)) return false;
            return Equals((BuildingTypeDto)obj);
        }

        public bool Equals(BuildingTypeDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.BuildingTypeID == BuildingTypeID;
        }

        public override int GetHashCode()
        {
            unchecked { return BuildingTypeID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as BuildingTypeDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.BuildingTypeID == BuildingTypeID, "Id");
            result &= CheckChanges(other.BuildingTypeCode == BuildingTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.BuildingTypeName, BuildingTypeName), "Наименование");
            result &= CheckChanges(StringEquals(other.BuildingTypeShName, BuildingTypeShName), "Краткое наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion

        public override string ToString()
        {
            return BuildingTypeName;
        }
    }
}
