using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Роль работника ППЭ")]
    public class SWorkerRoleDto : DtoBase, IEquatable<SWorkerRoleDto>
    {
        public int SWorkerRoleID { get; set; }

        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int SWorkerRoleCode { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string SWorkerRoleName { get; set; }

        [CsvColumn(Name = "Долность работника", FieldIndex = 3)]
        public int SWorkerPosition { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<SWorkerRoleDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SWorkerRoleDto)) return false;
            return Equals((SWorkerRoleDto)obj);
        }

        public bool Equals(SWorkerRoleDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.SWorkerRoleID == SWorkerRoleID;
        }

        public override int GetHashCode()
        {
            unchecked { return SWorkerRoleID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as SWorkerRoleDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.SWorkerRoleID == SWorkerRoleID, "Id");
            result &= CheckChanges(other.SWorkerRoleCode == SWorkerRoleCode, "Код");
            result &= CheckChanges(StringEquals(other.SWorkerRoleName, SWorkerRoleName), "Наименование");
            result &= CheckChanges(other.SWorkerPosition == SWorkerPosition, "Должность работника");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
