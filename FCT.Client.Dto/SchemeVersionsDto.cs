using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto
{
    [Serializable][Description("Схема")]
    public class SchemeVersionsDto : DtoBase, IEquatable<SchemeVersionsDto>
    {
        public decimal SchemeVersionID { get; set; }
        public string SchemeVersionDate { get; set; }
        public int SchemeTypeCode { get; set; }

        #region IEquatable<SchemeVersionsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SchemeVersionsDto)) return false;
            return Equals((SchemeVersionsDto) obj);
        }

        public bool Equals(SchemeVersionsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.SchemeVersionID == SchemeVersionID;
        }

        public override int GetHashCode()
        {
            unchecked { return SchemeVersionID.GetHashCode(); }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as SchemeVersionsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.SchemeVersionID == SchemeVersionID, "Id");
            result &= CheckChanges(StringEquals(other.SchemeVersionDate, SchemeVersionDate), "Дата");
            result &= CheckChanges(other.SchemeTypeCode == SchemeTypeCode, "Код");

            return result ? 0 : 1;
        }

        #endregion
    }
}
