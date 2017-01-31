using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description("Оценки и баллы")]
    [BulkTable("res_Marks", "Marks", RootTagName = "ArrayOfMarksDto", IsResTable = true)]
    public class MarksDto : DtoBase, IEquatable<MarksDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("HumanTestID")]
        public override Guid DtoID { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string HumanTestName
        {
            get { return HumanTestDto.With(x => x.ParticipantDto).Return(x => x.FIO, HumanTestUID); }
        }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [BulkColumn]
        [Description("Первичный балл за весь экзамен")]
        public int PrimaryMark { get; set; }

        [BulkColumn]
        [Description("Процент выполнения теста")]
        public int PercentMark { get; set; }

        [BulkColumn]
        [Description("Преодоление минимальной границы")]
        public int Mark5 { get; set; }

        [BulkColumn]
        [Description("Первичный балл за часть А")]
        public int PrimaryMarkA { get; set; }

        [BulkColumn]
        [Description("Выполнение заданий части А")]
        public string TestResultA { get; set; }

        [BulkColumn]
        [Description("Первичный балл за часть В")]
        public int PrimaryMarkB { get; set; }

        [BulkColumn]
        [Description("Выполнение заданий части В")]
        public string TestResultB { get; set; }

        [BulkColumn]
        [Description("Сумма оценок за часть С")]
        public int PrimaryMarkC { get; set; }

        [BulkColumn]
        [Description("Выполнение заданий части С")]
        public string TestResultC { get; set; }

        [BulkColumn]
        [Description("Сумма оценок за устную часть")]
        public int PrimaryMarkD { get; set; }

        [BulkColumn]
        [Description("Выполнение заданий устной части")]
        public string TestResultD { get; set; }
           
        [BulkColumn] [XmlIgnore] public int Mark100 { get; set; }
        [BulkColumn] [XmlIgnore] public int MarkX { get; set; }
        [BulkColumn] [XmlIgnore] public int Rating { get; set; }

        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public HumanTestsDto HumanTestDto { get; set; }
        #endregion

        #region IEquatable<MarksDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MarksDto)) return false;
            return Equals((MarksDto)obj);
        }

        public bool Equals(MarksDto other)
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
            var other = obj as MarksDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            
            result &= CheckChanges(other.PrimaryMark == PrimaryMark, TypeExtensions.Description<MarksDto>(c => c.PrimaryMark));
            result &= CheckChanges(other.PercentMark == PercentMark, TypeExtensions.Description<MarksDto>(c => c.PercentMark));
            result &= CheckChanges(other.Mark5 == Mark5, TypeExtensions.Description<MarksDto>(c => c.Mark5));
            result &= CheckChanges(other.PrimaryMarkA == PrimaryMarkA, TypeExtensions.Description<MarksDto>(c => c.PrimaryMarkA));
            result &= CheckChanges(StringEquals(other.TestResultA, TestResultA), TypeExtensions.Description<MarksDto>(c => c.TestResultA));
            result &= CheckChanges(other.PrimaryMarkB == PrimaryMarkB, TypeExtensions.Description<MarksDto>(c => c.PrimaryMarkB));
            result &= CheckChanges(StringEquals(other.TestResultB, TestResultB), TypeExtensions.Description<MarksDto>(c => c.TestResultB));
            result &= CheckChanges(other.PrimaryMarkC == PrimaryMarkC, TypeExtensions.Description<MarksDto>(c => c.PrimaryMarkC));
            result &= CheckChanges(StringEquals(other.TestResultC, TestResultC), TypeExtensions.Description<MarksDto>(c => c.TestResultC));
            result &= CheckChanges(other.PrimaryMarkD == PrimaryMarkD, TypeExtensions.Description<MarksDto>(c => c.PrimaryMarkD));
            result &= CheckChanges(StringEquals(other.TestResultD, TestResultD), TypeExtensions.Description<MarksDto>(c => c.TestResultD));

            return result ? 0 : 1;
        }
        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get { return HumanTestUID; } set { } }

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
