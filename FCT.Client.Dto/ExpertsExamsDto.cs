using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Enums;
using System.Xml;

namespace FCT.Client.Dto
{
    [Serializable][Description("Экзамен эксперта")]
    [BulkTable("rbd_ExpertsExams", "ExpertsExams", RootTagName = "ArrayOfExpertsExamsDto")]
    public class ExpertsExamsDto : DtoCreateDateBase, IEquatable<ExpertsExamsDto>, IDtoWithExam, IDtoWithExpert,
        IUidableDto, IDtoCollectorAccepter
    {
        [BulkColumn("REGION")]
        [XmlElement]
        public override int Region { get; set; }

        [BulkColumn("ID")]
        public override Guid DtoID { get; set; }

        [Description("Эксперт")]
        [XmlIgnore] public string ExpertName { get { return ExpertDto.Return(x => x.FIO, ExpertUID); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return ExamDto.Return(x => x.ToString(), Convert.ToString(Exam)); } }

        [BulkColumn("ExamGlobalID")]
        [XmlElement]
        public int Exam { get; set; }

        [BulkColumn("ExpertID")]
        public Guid Expert { get; set; }

        [XmlElement("IsDeleted")]
        public string IsDeletedSerialize
        {
            get { return IsDeleted ? "1" : "0"; }
            set { IsDeleted = XmlConvert.ToBoolean(value); }
        }

        [BulkColumn]
        [XmlElement]
        public Guid StationsExamsID { get; set; }

        [BulkColumn]
        [XmlElement]
        public int CheckFormOnExam { get; set; }

        #region NonSerializable

        [XmlIgnore] public ExamsDto ExamDto { get; set; }
        [XmlIgnore] public ExpertsDto ExpertDto { get; set; }

        #endregion
       
        #region IEquatable<ExpertsExamsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ExpertsExamsDto);
        }

        public bool Equals(ExpertsExamsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Expert.Equals(Expert) && 
                other.Exam == Exam && 
                other.Region == Region;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Expert.ToString().GetHashCode();
                result = result*37 + Exam.GetHashCode();
                result = result*37 + Region.GetHashCode();
                return result;
            }
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID
        {
            get { return string.Format("({0}, {1})", ExpertUID, Exam); }
            set { }
        }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string ExpertUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationsExamsUID { get; set; }

        #endregion

        public override ImportGroup ImportGroup { get { return ImportGroup.Planning; } }

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
