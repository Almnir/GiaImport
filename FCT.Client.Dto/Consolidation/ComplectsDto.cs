using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Комплекты")]
    [BulkTable("res_Complects", "Complects", RootTagName = "ArrayOfComplectsDto", IsResTable = true)]
    public class ComplectsDto : DtoBase, IEquatable<ComplectsDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("ComplectID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [BulkColumn]
        public int RegionCode { get; set; }

        [BulkColumn("Barcode_AB")]
        [Description("Штрих код бланка AB")]
        public string BarcodeAB { get; set; }

        [BulkColumn("Barcode_C")]
        [Description("Штрих код бланка С")]
        public string BarcodeC { get; set; }

        [BulkColumn("Barcode_D")]
        [Description("Штрих код бланка D")]
        public string BarcodeD { get; set; }

        [BulkColumn("Barcode_R")]
        [Description("Штрих код бланка R")]
        public string BarcodeR { get; set; }

        [BulkColumn("SheetFK_AB")]
        public Guid SheetABId { get; set; }

        [BulkColumn("SheetFK_C")]
        public Guid SheetCId { get; set; }

        [BulkColumn("SheetFK_R")]
        public Guid SheetRId { get; set; }

        [BulkColumn("SheetFK_D")]
        public Guid SheetDId { get; set; }

        [BulkColumn]
        public int TestTypeCode {get; set;}

        [BulkColumn]
        public int SubjectCode { get; set; }

        [BulkColumn]
        public int ZoneCode { get; set; }

        [BulkColumn]
        public int VariantCode { get; set; }

        [BulkColumn]
        public bool IsTom { get; set; }

        [BulkColumn]
        public Guid ExchangedID { get; set; }

        [BulkColumn]
        public string ExamDate { get; set; }

        [BulkColumn]
        public string DepartmentCode { get; set; }

        [BulkColumn]
        public string StuffCode { get; set; }

        [BulkColumn]
        public int ComplectType { get; set; }

        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public int IsEmptyProperty { set { IsDeleted = Convert.ToBoolean(value); } }
        #endregion

        #region IEquatable<AltsDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ComplectsDto)) return false;
            return Equals((ComplectsDto)obj);
        }

        public bool Equals(ComplectsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.Region == Region;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 37 + Region.GetHashCode();
                return result;
            }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as ComplectsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.BarcodeD, BarcodeD), TypeExtensions.Description<ComplectsDto>(c => c.BarcodeD));
            result &= CheckChanges(StringEquals(other.BarcodeC, BarcodeC), TypeExtensions.Description<ComplectsDto>(c => c.BarcodeC));
            result &= CheckChanges(StringEquals(other.BarcodeAB, BarcodeAB), TypeExtensions.Description<ComplectsDto>(c => c.BarcodeAB));
            result &= CheckChanges(StringEquals(other.BarcodeR, BarcodeR), TypeExtensions.Description<ComplectsDto>(c => c.BarcodeR));
            
            return result ? 0 : 1;
        }
        #endregion

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string SheetCUID { get; set; }
#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string SheetABUID { get; set; }
#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string SheetRUID { get; set; }
#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string SheetDUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string ExchangedUID { get; set; }

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
