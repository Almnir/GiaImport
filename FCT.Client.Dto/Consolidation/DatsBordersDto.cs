using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable]
    [Description("Данные о шкалах функционала Шкалирование ГИА")]
    [BulkTable("dats_Borders", "DatsBorders", RootTagName = "ArrayOfDatsBordersDto", IsResTable = true)]
    public class DatsBordersDto : DtoBase, IEquatable<DatsBordersDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("BorderID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [BulkColumn("SubjectCode")]
        [Description("Код предмета")]
        public int SubjectCode { get; set; }

        [BulkColumn("ScalingGroupFK")]
        public Guid ScalingGroupID { get; set; }

        [BulkColumn("PrimaryMark")]
        [Description("Максимальное значение пер-вичного балла для получения оценки шкалы")]
        public int PrimaryMark { get; set; }

        [BulkColumn("ScaleMark")]
        [Description("Оценка шкалы")]
        public int ScaleMark { get; set; }
        
        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public int IsDeletedProperty { set { IsDeleted = Convert.ToBoolean(value); } }
        #endregion

        #region IEquatable<DatsBordersDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DatsBordersDto)) return false;
            return Equals((DatsBordersDto)obj);
        }

        public bool Equals(DatsBordersDto other)
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
            var other = obj as DatsBordersDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.SubjectCode == SubjectCode, TypeExtensions.Description<DatsBordersDto>(c => c.SubjectCode));
            result &= CheckChanges(StringEquals(other.ScalingGroupUID, ScalingGroupUID), TypeExtensions.Description<DatsBordersDto>(c => c.ScalingGroupUID));
            result &= CheckChanges(other.PrimaryMark == PrimaryMark, TypeExtensions.Description<DatsBordersDto>(c => c.PrimaryMark));
            result &= CheckChanges(other.ScaleMark == ScaleMark, TypeExtensions.Description<DatsBordersDto>(c => c.ScaleMark));

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
        public string ScalingGroupUID { get; set; }

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
