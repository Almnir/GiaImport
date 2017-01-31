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
    [Serializable][Description("Ответы участников")]
    [BulkTable("res_Answers", "Answers", RootTagName = "ArrayOfAnswersDto", IsResTable = true)]
    public class AnswersDto : DtoBase, IEquatable<AnswersDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("AnswerID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [Description("Результат")]
        [XmlIgnore] public string MarkName { get { return MarkDto.Return(x => x.PrimaryMark.ToString(), HumanTestUID); } }

        [BulkColumn("HumanTestFK")]
        public Guid HumanTestId { get; set; }

        [Description("Тип задания")]
        [XmlIgnore] public string TaskTypeName { get { return TaskTypeCode.GetDescription(); } }

        [BulkColumn]
        public TaskType TaskTypeCode { get; set; }

        [BulkColumn]
        [Description("Номер задания")]
        public int TaskNumber { get; set; }
        
        #region AnswerValue CData
        [BulkColumn][Description("Ответ")]
        [XmlIgnore] public string AnswerValue
        {
            get { return CDataAnswerValue.AnswerValue; }
            set { CDataAnswerValue.AnswerValue = value; }
        }

        [XmlElement("AnswerValue")]
        public CDataAnswerValueNode CDataAnswerValue { get; set; }

        public class CDataAnswerValueNode
        {
            [XmlIgnore]
            public string AnswerValue { get; set; }

            [XmlText]
            public XmlNode[] CDataAnswerValue
            {
                get
                {
                    var dummy = new XmlDocument();
                    return new XmlNode[] { dummy.CreateCDataSection(AnswerValue) };
                }
                set
                {
                    if (value == null)
                    {
                        AnswerValue = "";
                        return;
                    }

                    if (value.Length != 1)
                    {
                        throw new InvalidOperationException(
                            String.Format("Invalid array length {0}", value.Length));
                    }

                    AnswerValue = value[0].Value;
                }
            }
        }
        #endregion

        #region ReplaceValueValue CData
        [BulkColumn]
        [Description("Старый ответ на задание")]
        [XmlIgnore]
        public string ReplaceValue
        {
            get { return CDataReplaceValue.Return(x => x.ReplaceValue, string.Empty); }
            set { CDataReplaceValue.ReplaceValue = value; }
        }

        [XmlElement("ReplaceValue")]
        public CDataReplaceValueNode CDataReplaceValue { get; set; }

        public class CDataReplaceValueNode
        {
            [XmlIgnore]
            public string ReplaceValue { get; set; }

            [XmlText]
            public XmlNode[] CDataReplaceValue
            {
                get
                {
                    var dummy = new XmlDocument();
                    return new XmlNode[] { dummy.CreateCDataSection(ReplaceValue) };
                }
                set
                {
                    if (value == null)
                    {
                        ReplaceValue = "";
                        return;
                    }

                    if (value.Length != 1)
                    {
                        throw new InvalidOperationException(
                            String.Format("Invalid array length {0}", value.Length));
                    }

                    ReplaceValue = value[0].Value;
                }
            }
        }
        #endregion

        [BulkColumn]
        [Description("Категория ответа")]
        public int CategoryValue { get; set; }
               
        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public MarksDto MarkDto { get; set; }
        [XmlIgnore] public int TaskTypeCodeProperty { set { TaskTypeCode = (TaskType)value; } }
        #endregion

        #region IEquatable<AnswersDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AnswersDto)) return false;
            return Equals((AnswersDto)obj);
        }

        public bool Equals(AnswersDto other)
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
            var other = obj as AnswersDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.HumanTestUID, HumanTestUID), TypeExtensions.Description<AnswersDto>(c => c.HumanTestUID));
            result &= CheckChanges(other.TaskTypeCode == TaskTypeCode, TypeExtensions.Description<AnswersDto>(c => c.TaskTypeName));
            result &= CheckChanges(other.TaskNumber == TaskNumber, TypeExtensions.Description<AnswersDto>(c => c.TaskNumber));
            result &= CheckChanges(StringEquals(other.AnswerValue, AnswerValue), TypeExtensions.Description<AnswersDto>(c => c.AnswerValue));
            result &= CheckChanges(StringEquals(other.ReplaceValue, ReplaceValue), TypeExtensions.Description<AnswersDto>(c => c.ReplaceValue));
            result &= CheckChanges(other.CategoryValue == CategoryValue, TypeExtensions.Description<AnswersDto>(c => c.CategoryValue));

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
