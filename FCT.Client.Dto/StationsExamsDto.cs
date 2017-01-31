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
    [Serializable][Description("Назначение ППЭ на экзамен")]
    [BulkTable("rbd_StationsExams", "StationsExams", RootTagName = "ArrayOfStationsExamsDto")]
    public class StationsExamsDto : DtoCreateDateBase, IEquatable<StationsExamsDto>, IDtoCollectorAccepter, IUidableDto, IDtoWithExam, IDtoWithStation
    {
        [BulkColumn("StationsExamsID")]
        public override Guid DtoID { get; set; }
        
        [XmlElement]
        [BulkColumn("REGION")]
		public override int Region { get; set; }

        [BulkColumn("StationID")]
		[CsvColumn(Name = "Guid ППЭ", FieldIndex = 1)]
		public Guid Station { get; set; }

        [BulkColumn("ExamGlobalID")]
		[CsvColumn(Name = "Код дня экзамена", FieldIndex = 2)]
		public int Exam { get; set; }

        #region NonSerializable
        
        [Description("Наименование ППЭ")]
        [XmlIgnore] public string StationName { get { return StationDto.Return(x => x.ToString(), StationUID); } }

        [Description("Экзамен")]
        [XmlIgnore] public string ExamName { get { return ExamDto.Return(x => x.ToString(), Convert.ToString(Exam)); } }

        [XmlIgnore] public ExamsDto ExamDto { get; set; }
        [XmlIgnore] public StationsDto StationDto { get; set; }
        [XmlIgnore] public int LockOnStationProperty { set { LockOnStation = Convert.ToBoolean(value); } }

        #endregion

        [Description("Кол-во мест на экзамен")]
        [CsvColumn(Name = "Количество мест на экзамен", FieldIndex = 3)]
        public int? PlacesCount { get; set; }

        [BulkColumn]
        [Description("Экспортирован в ППЭ")]
        public bool LockOnStation { get; set; }

        [BulkColumn]
        public bool IsAutoAppoint { get; set; }

        [XmlElement("LockOnStation")]
        public string LockOnStationSerialize
        {
            get { return LockOnStation ? "1" : "0"; }
            set { LockOnStation = XmlConvert.ToBoolean(value); }
        }

        [XmlElement("IsAutoAppoint")]
        public string IsAutoAppointSerialize
        {
            get { return IsAutoAppoint ? "1" : "0"; }
            set { IsAutoAppoint = XmlConvert.ToBoolean(value); }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 4, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 5, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        #region IEquatable<StationsExamsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as StationsExamsDto);
        }

        public bool Equals(StationsExamsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region && 
                other.Station.Equals(Station) && 
                other.Exam == Exam;
        }

        public override int GetHashCode()
        {
	        unchecked
	        {
                int result = 17;
                result = result*37 + Region.GetHashCode();
                result = result*37 + Station.ToString().GetHashCode();
                result = result*37 + Exam.GetHashCode();
                return result;
	        }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as StationsExamsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Station.Equals(Station), "ППЭ");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.PlacesCount == PlacesCount, "Кол-во мест на экзамен");
            result &= CheckChanges(other.LockOnStation == LockOnStation, "Блокировка экспорта в ППЭ");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;            
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID
        {
            get { return string.Format("({0}, {1})", StationUID, Exam); }
            set { }
        }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationUID { get; set; }

        #endregion

        public override ImportGroup ImportGroup { get { return ImportGroup.Planning; } }

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
