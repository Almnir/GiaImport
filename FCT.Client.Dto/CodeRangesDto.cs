using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD.Client.Common.Enums;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Диапазоны номеров бланков")]
    public class CodeRangesDto : DtoCreateDateBase, IEquatable<CodeRangesDto>
    {
        [XmlElement]
        public override int Region { get; set; }

        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        public Int64 StartCode { get; set; }

        public Int64 EndCode { get; set; }

        public Int64 CurrentCode { get; set; }

        public CodeRangeType CodeRangeType { get; set; }

        public Guid? CurrentRegion { get; set; }

        public Guid? Government { get; set; }

        public Guid? School { get; set; }

        public CodeRangeOwner CodeRangeOwner { get; set; }

        public Guid? ParentCodeRange { get; set; }

        #region IEquatable<CodeRangesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CodeRangesDto)) return false;
            return Equals((CodeRangesDto)obj);
        }

        public bool Equals(CodeRangesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.DtoID.Equals(DtoID);
        }

        public override int GetHashCode()
        {
            unchecked { return DtoID.ToString().GetHashCode(); }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as CodeRangesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.CodeRangeOwner == CodeRangeOwner, "CodeRangeOwner");
            result &= CheckChanges(other.CodeRangeType == CodeRangeType, "CodeRangeType");
            result &= CheckChanges(other.CurrentCode == CurrentCode, "CurrentCode");
            result &= CheckChanges(other.CurrentRegion == CurrentRegion, "CurrentRegion");
            result &= CheckChanges(other.EndCode == EndCode, "EndCode");
            result &= CheckChanges(other.Government == Government, "Government");
            result &= CheckChanges(other.ParentCodeRange == ParentCodeRange, "ParentCodeRange");
            result &= CheckChanges(other.School == School, "School");
            result &= CheckChanges(other.StartCode == StartCode, "StartCode");

            return result ? 0 : 1;
        }

        #endregion
    }
}
