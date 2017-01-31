using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
    [Serializable][Description("Назначение аудитории на экзамен")]
    [BulkTable("rbd_StationExamAuditory", "StationExamAuditory", RootTagName = "ArrayOfStationExamAuditoryDto")]
    public class StationExamAuditoryDto : DtoCreateDateBase, IEquatable<StationExamAuditoryDto>, IDtoWithStation, IDtoWithStationExam, 
        IDtoWithAuditorium, IUidableDto, IDtoCollectorAccepter
    {
        [BulkColumn("Region")]
        [XmlElement]
        public override int Region { get; set; }

        [BulkColumn("StationExamAuditoryID")]
        public override Guid DtoID { get; set; }

        [XmlIgnore]private AuditoriumSurrogateKey _auditoriumSurrogateKey;
        [XmlIgnore]public AuditoriumSurrogateKey AuditoriumSurrogateKey
        {
            get { return _auditoriumSurrogateKey ?? (_auditoriumSurrogateKey = new AuditoriumSurrogateKey(Station, AuditoriumCode)); }
        }
        /* поиск аудитории по коду + ппэ */
        [XmlIgnore]
        private string _auditoriumCode;
        [CsvColumn(Name = "Код аудитории", FieldIndex = 2)]
        public string AuditoriumCode
        {
            get { return _auditoriumCode; }
            set { _auditoriumCode = value.ToAuditoriumCodeFormat(); }
        }

        [CsvColumn(Name = "Guid ППЭ", FieldIndex = 1)]
        public Guid Station { get; set; }

        [CsvColumn(Name = "Код дня экзамена", FieldIndex = 3)]
        public int Exam { get; set; }

        #region NonSerializable

        [Description("Наименование ППЭ")]
        [XmlIgnore] public string StationName { get { return StationDto.Return(x => x.ToString(), StationUID); } }
        
        [Description("Аудитория")]
        [XmlIgnore] public string AuditoriumName { get { return AuditoriumDto.Return(x => x.ToString(), AuditoriumUID); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return StationExamDto.With(x => x.ExamDto).Return(x => x.ToString(), Convert.ToString(Exam)); } }

        [XmlIgnore] public StationsExamsDto StationExamDto { get; set; }
        [XmlIgnore] public StationsDto StationDto { get; set; }
        [XmlIgnore] public AuditoriumsDto AuditoriumDto { get; set; }

        [BulkColumn("StationID")]
        public Guid StationId { get { return StationDto != null ? StationDto.DtoID : Guid.Empty; } set { } }

        [BulkColumn("AuditoriumID")]
        [XmlElement("Auditorium")]
        public Guid AuditoriumId { get { return AuditoriumDto != null ? AuditoriumDto.DtoID : Guid.Empty; } set { } }

        [BulkColumn("StationsExamsID")]
        [XmlElement("StationsExamsID")]
        public Guid StationExamId { get { return StationExamDto != null ? StationExamDto.DtoID : Guid.Empty; } set { } }

        #endregion

        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 5, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 6, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        [BulkColumn("PlacesCount")]
        [Description("Кол-во мест на экзамен")]
        [CsvColumn(Name = "Количество мест на экзамен", FieldIndex = 4)]
        public int PlacesCount { get; set; }

	    [XmlIgnore] public StationsExamsDto Dirty_StationExam
	    {
            get { return new StationsExamsDto { Station = Station, Exam = Exam, Region = Region }; }
	    }

        [BulkColumn("IsPreparation")]
        [XmlIgnore]
        [Description("Назначена как аудитория подготовки")]
        public bool IsPreparation { get; set; }

        [XmlElement("IsPreparation")]
        [CsvColumn(Name = "Назначена как аудитория подготовки", FieldIndex = 7)]
        public string IsPreparationSerialize
        {
            get { return IsPreparation ? "1" : "0"; }
            set { IsPreparation = XmlConvert.ToBoolean(value); }
        }

        [XmlIgnore]
        public AuditoriumsDto Dirty_Auditorium
        {
            get { return new AuditoriumsDto { Station = Station, AuditoriumCode = AuditoriumCode }; }
        }

        #region IEquatable<StationExamAuditoryDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StationExamAuditoryDto)) return false;
            return Equals((StationExamAuditoryDto) obj);
        }

        public bool Equals(StationExamAuditoryDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region &&
                other.AuditoriumSurrogateKey.Equals(AuditoriumSurrogateKey) && 
                other.IsPreparation == IsPreparation &&
                other.Exam == Exam;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Region.GetHashCode();
                result = result * 37 + AuditoriumSurrogateKey.GetHashCode();
                result = result * 37 + IsPreparation.GetHashCode();
                result = result*37 + Exam.GetHashCode();
                return result;
            }
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }

        public override int CompareTo(object obj)
        {
            var other = obj as StationExamAuditoryDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.AuditoriumCode.Equals(AuditoriumCode), "Аудитория");
            result &= CheckChanges(other.PlacesCount == PlacesCount, "Кол-во мест на экзамен");
            result &= CheckChanges(other.Exam == Exam, "Экзмен");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");
            result &= CheckChanges(other.IsPreparation == IsPreparation, "Признак назначения аудитории на экзамен как аудитории подготовки");

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
        public string StationUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string AuditoriumUID { get; set; }

        #endregion

        public override ImportGroup ImportGroup { get { return ImportGroup.Planning; } }

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
