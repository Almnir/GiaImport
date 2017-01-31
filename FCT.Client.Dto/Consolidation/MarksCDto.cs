using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Информация об оценках")]
    [BulkTable("sht_Marks_C", "MarksC", RootTagName = "ArrayOfMarksCDto", IsResTable = true)]
    public class MarksCDto : DtoBase, IEquatable<MarksCDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("MarkID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [BulkColumn("SheetFK")]
        public Guid SheetId { get; set; }

        [BulkColumn]
        [Description("Имя файла протокола")]
        public string ProtocolFile { get; set; }

        [BulkColumn]
        [Description("Номер протокола")]
        public string ProtocolCode { get; set; }

        [BulkColumn]
        [Description("CRC изображения")]
        public string ProtocolCRC { get; set; }
        
        [BulkColumn]
        [Description("Флаг третьей проверки")]
        [XmlIgnore]
        public bool ThirdCheck { get; set; }

        [XmlElement("IsThird")]
        public int ThirdCheckInt
        {
            get { return Convert.ToInt32(ThirdCheck); }
            set { ThirdCheck = (value == 1); }
        }

        [BulkColumn]
        [Description("Номер строки в протоколе")]
        public int RowNumber { get; set; }

        [BulkColumn]
        [Description("Номер задания")]
        public int TaskNumber { get; set; }
        
        [BulkColumn]
        [Description("Оценка за задание")]
        public string MarkValue { get; set; }        

        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public SheetsCDto SheetsCDto { get; set; }
        #endregion

        #region IEquatable<MarksCDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AltsDto)) return false;
            return Equals((MarksCDto)obj);
        }

        public bool Equals(MarksCDto other)
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
            var other = obj as MarksCDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.SheetCUID, SheetCUID), "Бланк С");
            result &= CheckChanges(StringEquals(other.ProtocolFile, ProtocolFile), TypeExtensions.Description<MarksCDto>(c => c.ProtocolFile));
            result &= CheckChanges(StringEquals(other.ProtocolCode, ProtocolCode), TypeExtensions.Description<MarksCDto>(c => c.ProtocolCode));
            result &= CheckChanges(StringEquals(other.ProtocolCRC, ProtocolCRC), TypeExtensions.Description<MarksCDto>(c => c.ProtocolCRC));
            result &= CheckChanges(StringEquals(other.MarkValue, MarkValue), TypeExtensions.Description<MarksCDto>(c => c.MarkValue));
            result &= CheckChanges(other.ThirdCheck == ThirdCheck, TypeExtensions.Description<MarksCDto>(c => c.ThirdCheck));
            result &= CheckChanges(other.RowNumber == RowNumber, TypeExtensions.Description<MarksCDto>(c => c.RowNumber));
            result &= CheckChanges(other.TaskNumber == TaskNumber, TypeExtensions.Description<MarksCDto>(c => c.TaskNumber));

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
