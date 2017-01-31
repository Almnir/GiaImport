using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Enums;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Измененные задания в апелляции")]
    [BulkTable("ac_AppealTasks", "AppealTasks", RootTagName = "ArrayOfAppealTasksDto", IsResTable = true)]
    public class AppealTasksDto : DtoBase, IEquatable<AppealTasksDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("AppealTaskID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [Description("Апелляционное заявление")]
        [XmlIgnore] public string AppealName { get { return AppealDto.Return(x => x.AppealType.ToString(), AppealUID); } }

        [BulkColumn("AppealFK")]
        public Guid AppealId { get; set; }

        [Description("Тип задания")]
        [XmlIgnore] public string TaskTypeName { get { return TaskType.GetDescription(); } }

        [BulkColumn("TaskType", typeof(int))]
        public TaskType TaskType { get; set; }

        [BulkColumn]
        [Description("Номер задания")]
        public int TaskNumber { get; set; }

        #region OldValue CData
        [BulkColumn]
        [Description("Старый ответ на задание")]
        [XmlIgnore] public string OldValue
        {
            get { return CDataOldValue.OldValue; }
            set { CDataOldValue.OldValue = value; }
        }

        [XmlElement("OldValue")]
        public CDataOldValueNode CDataOldValue { get; set; }

        public class CDataOldValueNode
        {
            [XmlIgnore]
            public string OldValue { get; set; }

            [XmlText]
            public XmlNode[] CDataOldValue
            {
                get
                {
                    var dummy = new XmlDocument();
                    return new XmlNode[] { dummy.CreateCDataSection(OldValue) };
                }
                set
                {
                    if (value == null)
                    {
                        OldValue = "";
                        return;
                    }

                    if (value.Length != 1)
                    {
                        throw new InvalidOperationException(
                            String.Format("Invalid array length {0}", value.Length));
                    }

                    OldValue = value[0].Value;
                }
            }
        }
        #endregion

        #region NewValue CData
        [BulkColumn]
        [Description("Новый ответ на задание")]
        [XmlIgnore] public string NewValue
        {
            get { return CDataNewValue.NewValue; }
            set { CDataNewValue.NewValue = value; }
        }

        [XmlElement("NewValue")]
        public CDataNewValueNode CDataNewValue { get; set; }

        public class CDataNewValueNode
        {
            [XmlIgnore]
            public string NewValue { get; set; }

            [XmlText]
            public XmlNode[] CDataNewValue
            {
                get
                {
                    var dummy = new XmlDocument();
                    return new XmlNode[] { dummy.CreateCDataSection(NewValue) };
                }
                set
                {
                    if (value == null)
                    {
                        NewValue = "";
                        return;
                    }

                    if (value.Length != 1)
                    {
                        throw new InvalidOperationException(
                            String.Format("Invalid array length {0}", value.Length));
                    }

                    NewValue = value[0].Value;
                }
            }
        }
        #endregion

        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public AppealsDto AppealDto { get; set; }
        [XmlIgnore] public int TaskTypeProperty { set { TaskType = (TaskType)value; } }
        #endregion

        #region IEquatable<AppealTasksDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AppealTasksDto)) return false;
            return Equals((AppealTasksDto)obj);
        }

        public bool Equals(AppealTasksDto other)
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
            var other = obj as AppealTasksDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.AppealUID, AppealUID), TypeExtensions.Description<AppealTasksDto>(c => c.AppealUID));
            result &= CheckChanges(other.TaskType == TaskType, TypeExtensions.Description<AppealTasksDto>(c => c.TaskTypeName));
            result &= CheckChanges(other.TaskNumber == TaskNumber, TypeExtensions.Description<AppealTasksDto>(c => c.TaskNumber));
            result &= CheckChanges(StringEquals(other.OldValue, OldValue), TypeExtensions.Description<AppealTasksDto>(c => c.OldValue));
            result &= CheckChanges(StringEquals(other.NewValue, NewValue), TypeExtensions.Description<AppealTasksDto>(c => c.NewValue));

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
        public string AppealUID { get; set; }

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
