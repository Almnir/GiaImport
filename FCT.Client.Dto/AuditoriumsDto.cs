using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Attributes;
using RBD.Common.Enums;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable][Description("Аудитория")]
    [BulkTable("rbd_Auditoriums", "Auditoriums", RootTagName = "ArrayOfAuditoriumsDto")]
    public class AuditoriumsDto : DtoCreateDateBase, IEquatable<AuditoriumsDto>, IDtoWithStation,
        IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("REGION")]
        [XmlElement]
        public override int Region { get; set; }

        [XmlIgnore] private AuditoriumSurrogateKey _auditoriumSurrogateKey;
        [XmlIgnore]public AuditoriumSurrogateKey AuditoriumSurrogateKey
        {
            get { return _auditoriumSurrogateKey ?? (_auditoriumSurrogateKey = new AuditoriumSurrogateKey(Station, AuditoriumCode)); }
        }

        [Description("Удалена")]
        [XmlIgnore] public override bool IsDeleted  { get { return DeleteType != DeleteType.OK; } set { } } 

        [Description("МСУ")]
        [XmlIgnore] public string GovernmentName { get { return StationDto.With(c => c.GovernmentDto.Return(x => x.ToString(), "---")); } }

        [Description("Код ППЭ")]
        [XmlIgnore]public string StationCode { get { return StationDto.Return(x => x.StationCodeString, "---"); } }

        [Description("Наименование ППЭ")]
        [XmlIgnore]
        public string StationName { get { return StationDto.Return(x => x.StationName, StationUID); } }

        [XmlIgnore] private string _auditoriumCode;

        [BulkColumn("AuditoriumCode")]
        [CsvColumn(Name = "Номер аудитории", FieldIndex = 4)]
        [Description("Номер аудитории")]
        public string AuditoriumCode
        {
            get { return _auditoriumCode; }
            set { _auditoriumCode = value.ToAuditoriumCodeFormat(); }
        }
        
        [XmlIgnore]
        public int AuditoriumCodeInt
        {
            get
            {
                int code;
                if (string.IsNullOrEmpty(_auditoriumCode) || !Int32.TryParse(_auditoriumCode, out code))
                    return -1;
                return code;
            }
        }

        [XmlIgnore] private string _auditoriumName;

        [BulkColumn("AuditoriumName")]
        [CsvColumn(Name = "Наименование аудитории", FieldIndex = 3)]
        [Description("Наименование аудитории")]
        public string AuditoriumName
        {
            get { return _auditoriumName; } 
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 22)
                {
                    _auditoriumName = value.Substring(0, 22);
                }
                else _auditoriumName = value;
            }
        }

        [BulkColumn("RowsCount")]
        [CsvColumn(Name = "Количество рядов в аудитории", FieldIndex = 5)]
        [Description("Количество рядов в аудитории")]
		public int RowsCount { get; set; }

        [BulkColumn("ColsCount")]
        [CsvColumn(Name = "Количество посадочных мест в ряду", FieldIndex = 6)]
        [Description("Количество посадочных мест в ряду")]
		public int ColsCount { get; set; }

        [Description("Расположение рядов в аудитории")]
        [XmlIgnore] public string OrganizerOrderName { get { return OrganizerOrder.GetDescription(); } }

        [Description("Принцип рассадки")]
        [XmlIgnore] public string LimitPotencialName { get { return LimitPotencial.GetDescription(); } }

        [BulkColumn("AuditoriumID")]
		[CsvColumn(Name = "Guid", FieldIndex = 1)]
		public override Guid DtoID { get; set; }

        [BulkColumn("StationID")]
        [CsvColumn(Name = "Guid ППЭ", FieldIndex = 7)]
		public Guid Station { get; set; }

        [BulkColumn("OrganizerOrder")]
        [CsvColumn(Name = "Расположение рядов в аудитории", FieldIndex = 8)]
		public AuditoriumDirection OrganizerOrder { get; set; }
		
        [CsvColumn(Name = "Признак удаленной строки", FieldIndex = 2)]
		public DeleteType DeleteType { get; set; }

        [BulkColumn("LimitPotencial")]
        [CsvColumn(Name = "Признак специализированной рассадки", FieldIndex = 9)]
		public LimitPotencial LimitPotencial { get; set; }

        [XmlElement("Imported")]
        public string ImportedSerialize { get { return Imported ? "1" : "0"; } set { Imported = XmlConvert.ToBoolean(value); } }
        
        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 10, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate { get { return base.CreateDate; } set { base.CreateDate = value; } }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 11, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate { get { return base.UpdateDate; } set { base.UpdateDate = value; } }

        [Description("Фактическая вместимость")]
        [XmlIgnore] public int PlacesCountFact { get; set; }

        [Description("Фактическое количество рабочих мест для устных экзаменов")]
        [XmlIgnore] public int VerbalPlacesCountFact { get; set; }
        
        [XmlIgnore] public int Dirty_PlacesCount 
        {
            get { return RowsCount * ColsCount; }
        }

        [XmlIgnore] public int VideoControlProperty { set { VideoControl = Convert.ToBoolean(value); } }

        [BulkColumn("VideoControl")]
        [Description("Наличие online видеонаблюдения")]
        [XmlIgnore] public bool VideoControl { get; set; }
        
#if GIA
#else
        [CsvColumn(Name = "Наличие online видеонаблюдения", FieldIndex = 12)]
#endif
        [XmlElement("VideoControl")]
        public string VideoControlSerialize
        {
            get { return VideoControl ? "1" : "0"; }
            set { VideoControl = XmlConvert.ToBoolean(value); }
        }

        [Description("Форма экзамена")]
        [XmlIgnore] public string ExamFormNamе { get { return ExamForm.GetDescription(); } }
        [BulkColumn("ExamForm")]
        [XmlIgnore] public ExamForm ExamForm { get; set; }

        [CsvColumn(Name = "Форма экзамена", FieldIndex = 13)]
        public int ExamFormInt { get { return (int)ExamForm; } set { ExamForm = (ExamForm)value; } }

        [Description("Может использоваться в качестве лаборатории")]
#if !GIA
        [GridColumnIgnore] /* Скрываем поле на форме импорта "просмотр экземпляров объектов" */
#endif
        [BulkColumn("IsLab")]
        [XmlIgnore] public bool IsLab { get; set; }

#if GIA
        [CsvColumn(Name = "Является лабораторией", FieldIndex = 12)]
#endif
        public int IsLabInt { get { return Convert.ToInt32(IsLab); } set { IsLab = Convert.ToBoolean(value); } }

        #region NonSerializable

        [XmlIgnore] public StationsDto StationDto { get; set; }
        [XmlIgnore] public bool Imported { get; set; }
        [XmlIgnore] public int AuditoriumDirectionProperty { set { OrganizerOrder = (AuditoriumDirection)value; } }
        [XmlIgnore] public int DeleteTypeProperty { set
        {
            DeleteType = (DeleteType)value;
        } }

        [XmlIgnore] public int LimitPotencialProperty
        {
            get { return (int) LimitPotencial; }
            set { LimitPotencial = (LimitPotencial)value; }
        }
        
        [XmlIgnore] public int OrganizerOrderProperty
        {
            get { return (int) OrganizerOrder; }
            set { OrganizerOrder = (AuditoriumDirection)value; }
        }

        #endregion

        #region IEquatable<AuditoriumsDto> Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AuditoriumsDto)) return false;
            return Equals((AuditoriumsDto) obj);
        }

        public bool Equals(AuditoriumsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.AuditoriumSurrogateKey.Equals(AuditoriumSurrogateKey);
        }

        public override int GetHashCode()
        {
            return AuditoriumSurrogateKey.GetHashCode();
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }

        public override int CompareTo(object obj)
        {
            var other = obj as AuditoriumsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Station.Equals(Station), TypeExtensions.Description<AuditoriumsDto>(c => c.StationName));
            result &= CheckChanges(other.AuditoriumCode == AuditoriumCode, TypeExtensions.Description<AuditoriumsDto>(c => c.AuditoriumCode));
            result &= CheckChanges(StringEquals(other.AuditoriumName, AuditoriumName), TypeExtensions.Description<AuditoriumsDto>(c => c.AuditoriumName));
            result &= CheckChanges(other.RowsCount == RowsCount, TypeExtensions.Description<AuditoriumsDto>(c => c.RowsCount));
            result &= CheckChanges(other.ColsCount == ColsCount, TypeExtensions.Description<AuditoriumsDto>(c => c.ColsCount));
            result &= CheckChanges(Equals(other.OrganizerOrder, OrganizerOrder), TypeExtensions.Description<AuditoriumsDto>(c => c.OrganizerOrderName));
            result &= CheckChanges(Equals(other.DeleteType, DeleteType), TypeExtensions.Description<AuditoriumsDto>(c => c.IsDeleted));
            result &= CheckChanges(Equals(other.LimitPotencial, LimitPotencial), TypeExtensions.Description<AuditoriumsDto>(c => c.LimitPotencialName));
            result &= CheckChanges(other.VideoControl.Equals(VideoControl), TypeExtensions.Description<AuditoriumsDto>(c => c.VideoControl));
            result &= CheckChanges(other.IsLab == IsLab, TypeExtensions.Description<AuditoriumsDto>(c => c.IsLab));
            result &= CheckChanges(other.ExamForm == ExamForm, TypeExtensions.Description<AuditoriumsDto>(c => c.ExamFormNamе));

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

        #endregion

        public override string ToString()
        {
            return string.Format("({0}) {1}", AuditoriumCode, AuditoriumName);
        }

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class AuditoriumSurrogateKey : IEquatable<AuditoriumSurrogateKey>
    {
        public Guid Station { get; set; }
        public string AuditoriumCode { get; set; }

        public AuditoriumSurrogateKey(Guid station, string auditoriumCode)
        {
            Station = station;
            AuditoriumCode = auditoriumCode ?? string.Empty;
        }

        public static bool operator ==(AuditoriumSurrogateKey left, AuditoriumSurrogateKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AuditoriumSurrogateKey left, AuditoriumSurrogateKey right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AuditoriumSurrogateKey)) return false;
            return Equals((AuditoriumSurrogateKey)obj);
        }

        public bool Equals(AuditoriumSurrogateKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.AuditoriumCode == AuditoriumCode &&
                other.Station.Equals(Station);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 37 + AuditoriumCode.GetHashCode();
                result = result * 37 + Station.ToString().GetHashCode();
                return result;
            }
        }
    }
}
