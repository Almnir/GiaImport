using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable]
    [Description("Данные о группах критериев функционала Шкалирование ГИА")]
    [BulkTable("dats_Groups", "DatsGroups", RootTagName = "ArrayOfDatsGroupsDto", IsResTable = true)]
    public class DatsGroupsDto : DtoBase, IEquatable<DatsGroupsDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("ScalingGroupID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [BulkColumn("GroupTypeCode")]
        [Description("Код группы")]
        public int GroupTypeCode { get; set; }

        [BulkColumn("GroupName")]
        [Description("Название группы")]
        public string GroupName { get; set; }

        [BulkColumn("TestTypeCode")]
        [Description("Тип тестерования")]
        public int TestTypeCode { get; set; }

        [BulkColumn("SubjectCode")]
        [Description("Код предмета")]
        public int SubjectCode { get; set; }

        [BulkColumn("ScaleMarkMinimum")]
        [Description("Минимальный балл/оценка удовлетворяющая данному критерию")]
        public int ScaleMarkMinimum { get; set; }

        [BulkColumn("GroupMarkMinimum")]
        [Description("Минимальный первичный балл, который соответствует оценке группы")]
        public int GroupMarkMinimum { get; set; }
        
        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public int IsDeletedProperty { set { IsDeleted = Convert.ToBoolean(value); } }
        #endregion

        #region IEquatable<DatsBordersDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DatsGroupsDto)) return false;
            return Equals((DatsGroupsDto)obj);
        }

        public bool Equals(DatsGroupsDto other)
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
            var other = obj as DatsGroupsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.GroupTypeCode == GroupTypeCode, TypeExtensions.Description<DatsGroupsDto>(c => c.SubjectCode));
            result &= CheckChanges(StringEquals(other.GroupName, GroupName), TypeExtensions.Description<DatsGroupsDto>(c => c.GroupName));
            result &= CheckChanges(other.TestTypeCode == TestTypeCode, TypeExtensions.Description<DatsGroupsDto>(c => c.TestTypeCode));
            result &= CheckChanges(other.SubjectCode == SubjectCode, TypeExtensions.Description<DatsGroupsDto>(c => c.SubjectCode));
            result &= CheckChanges(other.ScaleMarkMinimum == ScaleMarkMinimum, TypeExtensions.Description<DatsGroupsDto>(c => c.ScaleMarkMinimum));
            result &= CheckChanges(other.GroupMarkMinimum == GroupMarkMinimum, TypeExtensions.Description<DatsGroupsDto>(c => c.GroupMarkMinimum));

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
