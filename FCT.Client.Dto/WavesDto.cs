using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Этап")]
	public class WavesDto : DtoBase, IEquatable<WavesDto>, IDtoWithSchemeVersion
	{
        #region NonSerialized

        [XmlIgnore] public SchemeVersionsDto SchemeVersionDto { get; set; }

        #endregion

        public int WaveGlobalID { get; set; }
        public decimal SchemeVersionID { get; set; }

		[CsvColumn(Name = "Код", FieldIndex = 1)]
		public int WaveCode { get; set; }

		[CsvColumn(Name = "Наименование", FieldIndex = 2)]
		public string WaveName { get; set; }

        #region IEquatable<WavesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (WavesDto)) return false;
            return Equals((WavesDto) obj);
        }

	    public bool Equals(WavesDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return other.WaveCode == WaveCode;
	    }

	    public override int GetHashCode()
	    {
	        unchecked { return WaveCode * 397; }
	    }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as WavesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.WaveGlobalID == WaveGlobalID, "Id");
            result &= CheckChanges(other.SchemeVersionID == SchemeVersionID, "Схема");
            result &= CheckChanges(other.WaveCode == WaveCode, "Код");
            result &= CheckChanges(StringEquals(other.WaveName, WaveName), "Наименование");

            return result ? 0 : 1;
        }

        #endregion
	}	
}
