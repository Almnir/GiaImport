using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Человеко-тесты")]
    [BulkTable("res_HumanTests", "HumanTests", RootTagName = "ArrayOfHumanTestsDto", IsResTable = true)]
    public class HumanTestsDto : DtoBase, IEquatable<HumanTestsDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("HumanTestID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, ParticipantUID); } }

        [BulkColumn("ParticipantFK")]
        public Guid ParticipantId { get; set; }

        [BulkColumn] 
        public Guid PackageFK { get; set; }

        [BulkColumn] [XmlIgnore] public int TestTypeCode { get; set; }
        [BulkColumn] [XmlIgnore] public int LicenseCondition { get; set; }
        [BulkColumn] [XmlIgnore] public int ReplicationCondition { get; set; }
        [BulkColumn] [XmlIgnore] public int RegionCode { get; set; }

        [BulkColumn]
        [Description("Дата экзамена")]
        public string ExamDate { get; set; }

        [BulkColumn]
        [Description("Код предмета")]
        public int SubjectCode { get; set; }

        [BulkColumn]
        [Description("Код ППЭ")]
        public int StationCode { get; set; }

        [BulkColumn]
        [Description("Код аудитории")]
        public string AuditoriumCode { get; set; }

        [BulkColumn]
        [Description("Код представительства")]
        public string DepartmentCode { get; set; }

        [BulkColumn]
        [Description("Вариант")]
        public int VariantCode { get; set; }

        [BulkColumn]
        [Description("Статус процесса обработки")]
        public int ProcessCondition { get; set; }

        [BulkColumn]
        public int FileName { get; set; }

        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }
        #endregion

        #region IEquatable<HumanTestsDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(HumanTestsDto)) return false;
            return Equals((HumanTestsDto)obj);
        }

        public bool Equals(HumanTestsDto other)
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
            var other = obj as HumanTestsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(Equals(other.ParticipantUID, ParticipantUID), "Участник");
            result &= CheckChanges(StringEquals(other.ExamDate, ExamDate), TypeExtensions.Description<HumanTestsDto>(c => c.ExamDate));
            result &= CheckChanges(other.SubjectCode == SubjectCode, TypeExtensions.Description<HumanTestsDto>(c => c.SubjectCode));
            result &= CheckChanges(other.StationCode == StationCode, TypeExtensions.Description<HumanTestsDto>(c => c.StationCode));
            result &= CheckChanges(other.DepartmentCode == DepartmentCode, TypeExtensions.Description<HumanTestsDto>(c => c.DepartmentCode));
            result &= CheckChanges(StringEquals(other.AuditoriumCode, AuditoriumCode), TypeExtensions.Description<HumanTestsDto>(c => c.AuditoriumCode));
            result &= CheckChanges(other.VariantCode == VariantCode, TypeExtensions.Description<HumanTestsDto>(c => c.VariantCode));
            result &= CheckChanges(other.ProcessCondition == ProcessCondition, TypeExtensions.Description<HumanTestsDto>(c => c.ProcessCondition));
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
        public string ParticipantUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string PackageUID { get; set; }

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
