using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Апелляции участников")]
    [BulkTable("ac_Appeals", "Appeals", RootTagName = "ArrayOfAppealsDto", IsResTable = true)]
    public class AppealsDto : DtoBase, IEquatable<AppealsDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("AppealID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string HumanTestName { get { return HumanTestDto.With(x => x.ParticipantDto).Return(x => x.FIO, HumanTestUID); } }

        [BulkColumn("HumanTestFK")]
        public Guid HumanTestId { get; set; }

        [Description("Тип апелляции")]
        [BulkColumn("AppealType", typeof(int))]
        public int AppealType { get; set; }

        [BulkColumn]
        [Description("Флаг отклонения")]
        public bool DeclinedByCommittee { get; set; }

        [XmlElement("DeclinedByCommittee")]
        public int DeclinedByCommitteeInt
        {
            get { return Convert.ToInt32(DeclinedByCommittee); }
            set { DeclinedByCommittee = (value == 1); }
        }

        private DateTime _createTime;
        [BulkColumn]
        [Description("Время создания апелляции")]
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = DateTime.SpecifyKind(value.NotMillisecondsDateTime(), DateTimeKind.Unspecified); }
        }

        private DateTime _updateTime;
        [BulkColumn]
        [Description("Время последнего обновления")]
        public DateTime UpdateTime 
        {
            get { return _updateTime; }
            set { _updateTime = DateTime.SpecifyKind(value.NotMillisecondsDateTime(), DateTimeKind.Unspecified); } 
        }

        [BulkColumn]
        [Description("Состояние апелляции")]
        public int AppealCondition { get; set; }

        #region AppealComment CData
        [BulkColumn]
        [Description("Комментарий")]
        [XmlIgnore] public string AppealComment 
        {
            get
            {
                return CDataAppealComment != null ? CDataAppealComment.AppealComment : string.Empty;
            }
            set { CDataAppealComment.AppealComment = value; }
        }

        [XmlElement("AppealComment")]
        public CDataNodeType CDataAppealComment { get; set; }

        public class CDataNodeType
        {
            [XmlIgnore]
            public string AppealComment { get; set; }

            [XmlText]
            public XmlNode[] CDataAppealComment
            {
                get
                {
                    var dummy = new XmlDocument();
                    return new XmlNode[] { dummy.CreateCDataSection(AppealComment) };
                }
                set
                {
                    if (value == null)
                    {
                        AppealComment = "";
                        return;
                    }

                    if (value.Length != 1)
                    {
                        throw new InvalidOperationException(
                            String.Format(
                                "Invalid array length {0}", value.Length));
                    }

                    AppealComment = value[0].Value;
                }
            }
        }
        #endregion

        [BulkColumn]
        public int WorkStation {get; set;}

        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public HumanTestsDto HumanTestDto { get; set; }
        #endregion

        #region IEquatable<AppealsDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AppealsDto)) return false;
            return Equals((AppealsDto)obj);
        }

        public bool Equals(AppealsDto other)
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
            var other = obj as AppealsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.HumanTestUID, HumanTestUID), TypeExtensions.Description<AppealsDto>(c => c.HumanTestUID));
            result &= CheckChanges(other.AppealType == AppealType, TypeExtensions.Description<AppealsDto>(c => c.AppealType));
            result &= CheckChanges(other.DeclinedByCommittee == DeclinedByCommittee, TypeExtensions.Description<AppealsDto>(c => c.DeclinedByCommittee));
            result &= CheckChanges(other.CreateTime == CreateTime, TypeExtensions.Description<AppealsDto>(c => c.CreateTime));
            result &= CheckChanges(other.UpdateTime == UpdateTime, TypeExtensions.Description<AppealsDto>(c => c.UpdateTime));
            result &= CheckChanges(other.AppealCondition == AppealCondition, TypeExtensions.Description<AppealsDto>(c => c.AppealCondition));
            result &= CheckChanges(StringEquals(other.AppealComment, AppealComment), TypeExtensions.Description<AppealsDto>(c => c.AppealComment));

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
        public string HumanTestUID { get; set; }

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

