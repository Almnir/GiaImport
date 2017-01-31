using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto
{
    [Serializable][Description("Ученое звание")]
    public class EducationKindsDto : DtoBase, IEquatable<EducationKindsDto>
    {
        public int EduKindID { get; set; }
        public int EduKindCode { get; set; }
        public string EduKindName { get; set; }
        public int SortBy { get; set; }

        #region IEquatable<EducationKindsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (EducationKindsDto)) return false;
            return Equals((EducationKindsDto) obj);
        }

        public bool Equals(EducationKindsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.EduKindID == EduKindID;
        }

        public override int GetHashCode()
        {
            unchecked { return EduKindID * 397; }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as EducationKindsDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.EduKindID == EduKindID, "Id");
            result &= CheckChanges(other.EduKindCode == EduKindCode, "Код");
            result &= CheckChanges(StringEquals(other.EduKindName, EduKindName), "Наименование");;
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
