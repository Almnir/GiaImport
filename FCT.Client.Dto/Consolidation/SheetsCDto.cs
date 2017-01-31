using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;
using RBD.Common.Enums;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Бланки С")]
    [BulkTable("sht_Sheets_C", "SheetsC", RootTagName = "ArrayOfSheetsCDto", IsResTable = true)]
    public class SheetsCDto : DtoBase, IEquatable<SheetsCDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("SheetID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [BulkColumn]
        public int RegionCode { get; set; }

        [BulkColumn]
        [Description("Штрих код")]
        [XmlIgnore]
        public string Barcode { get; set; }

        [BulkColumn]
        [Description("Флаг пустоты бланка")]
        [XmlIgnore] public bool IsEmpty { get; set; }

        [XmlElement("IsEmpty")]
        public int IsEmptyInt
        {
            get { return Convert.ToInt32(IsEmpty); }
            set { IsEmpty = (value == 1); }
        }

        [BulkColumn]
        [XmlIgnore]
        public string FileName { get; set; }

        [BulkColumn]
        [XmlIgnore]
        public string DepartmentCode { get; set; }

        [BulkColumn]
        [XmlIgnore]
        public string ExamDate { get; set; }

        [BulkColumn]
        [XmlIgnore]
        public string AuditoriumCode { get; set; }

        [BulkColumn("CRC")]
        [XmlIgnore]
        public string Crc { get; set; }

        [BulkColumn]
        public Guid PackageFK {get; set;}

        [BulkColumn]
        public int TestTypeCode {get; set;}

        [BulkColumn]
        public int SubjectCode { get; set; }

        [BulkColumn]
        public int StationCode { get; set; }

        [BulkColumn]
        public int SheetCode { get; set; }

        [BulkColumn]
        [XmlIgnore]
        public string ResponsibleCode { get; set; }

        [BulkColumn]
        public int ImageNumber { get; set; }

        [BulkColumn]
        public int VariantCode { get; set; }

        [BulkColumn]
        [XmlIgnore]
        public string ImagePaths { get; set; }

        [BulkColumn]
        [XmlIgnore]
        public string ProtocolCodes { get; set; }

        [BulkColumn]
        public int Condition { get; set; }

        [BulkColumn]
        public int ProjectBatchID { get; set; }

        [BulkColumn("DeleteType")]
        [XmlIgnore]
        public DeleteType DeleteType { get; set; }
        
        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public int IsEmptyProperty { set { IsEmpty = Convert.ToBoolean(value); } }
        #endregion

        #region IEquatable<AltsDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SheetsCDto)) return false;
            return Equals((SheetsCDto)obj);
        }

        public bool Equals(SheetsCDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.Region == Region &&
                StringEquals(other.UID, UID);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 37 + Region.GetHashCode();
                result = result * 37 + (string.IsNullOrEmpty(UID) ? 0 : UID.Trim().ToUpper().GetHashCode());
                return result;
            }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as SheetsCDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.RegionCode == RegionCode, TypeExtensions.Description<SheetsCDto>(c => c.RegionCode));
            result &= CheckChanges(StringEquals(other.Barcode, Barcode), TypeExtensions.Description<SheetsCDto>(c => c.Barcode));
            result &= CheckChanges(other.IsEmpty == IsEmpty, TypeExtensions.Description<SheetsCDto>(c => c.IsEmpty));

            return result ? 0 : 1;
        }
        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get; set; }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }
    }
}
