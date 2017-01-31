using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto
{
    [Serializable][Description("Уровень образования")]
    public class EducationTypesDto : DtoBase, IEquatable<EducationTypesDto>
    {
        public int EduTypeID { get; set; }
        public int EduTypeCode { get; set; }
        public string EduTypeName { get; set; }
        public int SortBy { get; set; }

        #region IEquatable<EducationTypesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (EducationTypesDto)) return false;
            return Equals((EducationTypesDto) obj);
        }

        public bool Equals(EducationTypesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.EduTypeID == EduTypeID;
        }

        public override int GetHashCode()
        {
            unchecked { return EduTypeID * 397; }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as EducationTypesDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.EduTypeID == EduTypeID, "Id");
            result &= CheckChanges(other.EduTypeCode == EduTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.EduTypeName, EduTypeName), "Наименование");;
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
