using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Client.Dto;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
    [Serializable][Description("Экзамен")]
    public class ExamsDto : DtoBase, IEquatable<ExamsDto>, IDtoWithSubject, IDtoWithWave
    {
        #region NonSerializable

        [XmlIgnore] public SchemeVersionsDto SchemeVersionDto { get; set; }
        [XmlIgnore] public SubjectsDto SubjectDto { get; set; }
        [XmlIgnore] public WavesDto WaveDto { get; set; }

        #endregion

        [CsvColumn(Name = "Код дня экзамена", FieldIndex = 1)]
		public int ExamGlobalID { get; set; }

        public decimal SchemeVersionID { get; set; }

        public int TestTypeCode { get; set; }

		[CsvColumn(Name = "Код общеобразовательного предмета", FieldIndex = 3)]
		public int SubjectCode { get; set; }

		[CsvColumn(Name = "Дата экзамена", FieldIndex = 2, RBDExt = RBDExtensions.StringToDate)]
		public string ExamDate { get; set; }

        [XmlIgnore] public string ExamDateParsed { get { return ExamDate.ToDateString(); } }

		[CsvColumn(Name = "Код типа экзамена", FieldIndex = 4)]
		public int WaveCode { get; set; }

        [CsvColumn(Name = "Тип даты экзамена", FieldIndex = 5)]
        public int ExamType { get; set; }

        #region IEquatable<ExamsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ExamsDto)) return false;
            return Equals((ExamsDto) obj);
        }

	    public bool Equals(ExamsDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return other.ExamGlobalID == ExamGlobalID;
	    }

	    public override int GetHashCode()
	    {
	        unchecked { return ExamGlobalID*397; }
	    }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as ExamsDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.ExamGlobalID == ExamGlobalID, "Id"); 
            result &= CheckChanges(other.TestTypeCode == TestTypeCode, "TestTypeCode");
            result &= CheckChanges(other.SchemeVersionID == SchemeVersionID, "Схема");
            result &= CheckChanges(other.SubjectCode == SubjectCode, "Код общеобразовательного предмета"); 
            result &= CheckChanges(StringEquals(other.ExamDate, ExamDate), "Дата экзамена"); 
            result &= CheckChanges(other.WaveCode == WaveCode, "Этап"); 
            result &= CheckChanges(other.ExamType == ExamType, "Код типа экзамена"); 

            return result ? 0 : 1;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} - {1} ({2}, {3})", 
                ExamDateParsed, 
                SubjectDto.Return(x => x.SubjectName, "---"),
                WaveDto.Return(x => x.WaveName, "---"),
                ((TestTypeCode)TestTypeCode).GetDescription());            
        }
	}
}
	