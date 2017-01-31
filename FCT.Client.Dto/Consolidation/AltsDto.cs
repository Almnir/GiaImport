using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Протоколы экспертов")]
    [BulkTable("sht_Alts", "Alts", RootTagName = "ArrayOfAltsDto", IsResTable = true)]
    public class AltsDto : DtoBase, IEquatable<AltsDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("AltID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [BulkColumn("SheetFK")]
        public Guid SheetId { get; set; }

        [BulkColumn]
        [Description("Штрих код")]
        public string Barcode { get; set; }

        [BulkColumn]
        [Description("Номер протокола")]
        public string ProtocolCode { get; set; }

        [Description("Эксперт")]
        [XmlIgnore] public string ExpertName { get { return ExpertDto.Return(x => x.FIO, ExpertUID); } }

        [BulkColumn("ExpertFK")]
        public Guid ExpertId { get; set; }

        [BulkColumn]
        [Description("Код эксперта")]
        public int ExpertCode { get; set; }

        [BulkColumn]
        [Description("Флаг третьей проверки")]
        [XmlIgnore] public bool IsThird { get; set; }

        [BulkColumn]
        [Description("Удалено")]
        public bool DeleteType { get; set; }

        [XmlElement("IsThird")]
        public int IsThirdInt
        {
            get { return Convert.ToInt32(IsThird); }
            set { IsThird = (value == 1); }
        }

        [XmlIgnore]
        public int Alt { get; set; }
        
        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public ExpertsDto ExpertDto { get; set; }
        [XmlIgnore] public SheetsCDto SheetsCDto { get; set; }
        [XmlIgnore] public int IsDeletedProperty { set { IsDeleted = Convert.ToBoolean(value); } }
        #endregion

        #region IEquatable<AltsDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AltsDto)) return false;
            return Equals((AltsDto)obj);
        }

        public bool Equals(AltsDto other)
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
            var other = obj as AltsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.SheetCUID, SheetCUID), "Бланк С");
            result &= CheckChanges(StringEquals(other.ExpertUID, ExpertUID), TypeExtensions.Description<AltsDto>(c => c.ExpertUID));
            result &= CheckChanges(StringEquals(other.Barcode, Barcode), TypeExtensions.Description<AltsDto>(c => c.Barcode));
            result &= CheckChanges(StringEquals(other.ProtocolCode, ProtocolCode), TypeExtensions.Description<AltsDto>(c => c.ProtocolCode));
            result &= CheckChanges(other.ExpertCode == ExpertCode, TypeExtensions.Description<AltsDto>(c => c.ExpertCode));
            result &= CheckChanges(other.IsThird == IsThird, TypeExtensions.Description<AltsDto>(c => c.IsThird));

            return result ? 0 : 1;
        }
        #endregion

        #region GiaDataCollect Fields

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
        public string ExpertUID { get; set; }

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
