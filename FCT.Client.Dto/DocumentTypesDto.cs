using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Тип документа")]
	public class DocumentTypesDto : DtoBase, IEquatable<DocumentTypesDto>
	{
		[CsvColumn(Name = "Код", FieldIndex = 1)]
		public int DocumentTypeCode { get; set; }

		[CsvColumn(Name = "Наименование", FieldIndex = 2)]
		public string DocumentTypeName { get; set; }

        public int SortBy { get; set; }

        #region IEquatable<DocumentTypesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DocumentTypesDto)) return false;
            return Equals((DocumentTypesDto) obj);
        }

	    public bool Equals(DocumentTypesDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return other.DocumentTypeCode == DocumentTypeCode;
	    }

	    public override int GetHashCode()
	    {
	        unchecked { return (DocumentTypeCode * 397); }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as DocumentTypesDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.DocumentTypeCode == DocumentTypeCode, "Код");
            result &= CheckChanges(StringEquals(other.DocumentTypeName, DocumentTypeName), "Наименование");
            result &= CheckChanges(other.SortBy == SortBy, "Сортировка");

            return result ? 0 : 1;
        }

        #endregion
    }
}
