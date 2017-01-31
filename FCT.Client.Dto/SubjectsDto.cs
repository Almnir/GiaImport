using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Предмет")]
    public class SubjectsDto : DtoBase, IEquatable<SubjectsDto>, IDtoWithSchemeVersion
	{
        #region NonSerialized

        [XmlIgnore] public SchemeVersionsDto SchemeVersionDto { get; set; }

        #endregion

        public int SubjectGlobalID { get; set; }
        public decimal SchemeVersionID { get; set; }
        public int TestTypeCode { get; set; }

		[CsvColumn(Name = "Код", FieldIndex = 1)]
		public int SubjectCode { get; set; }

		[CsvColumn(Name = "Наименование", FieldIndex = 2)]
		public string SubjectName { get; set; }

        public bool NeedThirdCheck { get; set; }
        public int TaskAmountA { get; set; }
        public int TaskAmountB { get; set; }
        public int TaskAmountC { get; set; }
        public int TaskAmountD { get; set; }

        #region IEquatable<SubjectsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SubjectsDto)) return false;
            return Equals((SubjectsDto) obj);
        }

	    public bool Equals(SubjectsDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return other.SubjectCode == SubjectCode;
	    }

	    public override int GetHashCode()
	    {
	        unchecked { return SubjectCode * 397; }
	    }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as SubjectsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.SubjectGlobalID == SubjectGlobalID, "Id");
            result &= CheckChanges(other.SchemeVersionID == SchemeVersionID, "Схема");
            result &= CheckChanges(other.TestTypeCode == TestTypeCode, "TestTypeCode");
            result &= CheckChanges(other.SubjectCode == SubjectCode, "Код");
            result &= CheckChanges(StringEquals(other.SubjectName, SubjectName), "Наименование");
            result &= CheckChanges(other.NeedThirdCheck == NeedThirdCheck, "NeedThirdCheck");
            result &= CheckChanges(other.TaskAmountA == TaskAmountA, "TaskAmountA");
            result &= CheckChanges(other.TaskAmountB == TaskAmountB, "TaskAmountB");
            result &= CheckChanges(other.TaskAmountC == TaskAmountC, "TaskAmountC");
            result &= CheckChanges(other.TaskAmountD == TaskAmountD, "TaskAmountD");

            return result ? 0 : 1;
        }

        #endregion
	}
}
