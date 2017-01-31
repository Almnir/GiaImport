using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Должность работника ППЭ")]
	public class SWorkerPositionsDto : DtoBase, IEquatable<SWorkerPositionsDto>
	{
        public int SWorkerPositionID { get; set; }

		[CsvColumn(Name = "Код", FieldIndex = 1)]
		public  int SWorkerPositionCode { get; set; }

		[CsvColumn(Name = "Наименование", FieldIndex = 2)]
		public  string SWorkerPositionName  { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<SWorkerPositionsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SWorkerPositionsDto)) return false;
            return Equals((SWorkerPositionsDto) obj);
        }

	    public bool Equals(SWorkerPositionsDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return other.SWorkerPositionID == SWorkerPositionID;
	    }

	    public override int GetHashCode()
	    {
	        unchecked { return SWorkerPositionID * 397; }
	    }

        public override int CompareTo(object obj)
        {
            var other = obj as SWorkerPositionsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.SWorkerPositionID == SWorkerPositionID, "Id");
            result &= CheckChanges(other.SWorkerPositionCode == SWorkerPositionCode, "Код");
            result &= CheckChanges(StringEquals(other.SWorkerPositionName, SWorkerPositionName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
	}
}
