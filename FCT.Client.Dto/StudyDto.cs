using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Форма обучения")]
    public class StudyDto : DtoBase, IEquatable<StudyDto>
    {
        [CsvColumn(Name = "Код", FieldIndex = 1)]
        public int Code { get; set; }

        [CsvColumn(Name = "Наименование", FieldIndex = 2)]
        public string Name { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<StudyDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StudyDto)) return false;
            return Equals((StudyDto) obj);
        }

        public bool Equals(StudyDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Code == Code;
        }

        public override int GetHashCode()
        {
            unchecked { return Code * 397; }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as StudyDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Code == Code, "Код");
            result &= CheckChanges(StringEquals(other.Name, Name), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
