using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Enums;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable][Description("ППЭ")]
    [BulkTable("rbd_Stations", "Stations", RootTagName = "ArrayOfStationsDto")]
    public class StationsDto : DtoCreateDateBase, IEquatable<StationsDto>, IDtoCollectorAccepter, IUidableDto, IDtoWithAddressNullable
    {
        [BulkColumn("StationID")]
        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        [Description("Удалено")]
        [XmlIgnore] public override bool IsDeleted { get { return DeleteType != DeleteType.OK; } set { } }

        [Description("Код ППЭ")]
        [XmlIgnore] public string StationCodeString { get { return StationCode.ToString("0000"); } }

        [BulkColumn]
        [CsvColumn(Name = "Код ППЭ", FieldIndex = 3)]
        public int StationCode { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Наименование ППЭ", FieldIndex = 4)]
        [Description("Наименование ППЭ")]
		public string StationName { get; set; }

        [BulkColumn]
        [Description("Адрес ППЭ")]
        [CsvColumn(Name = "Адрес ППЭ", FieldIndex = 5)]
        public string StationAddress { get; set; }

        [Description("МСУ")]
        [XmlIgnore] public string GovernmentName { get { return GovernmentDto.Return(x => x.ToString(), GovernmentUID); } }

        [Description("Код АТЕ")]
        [XmlIgnore] public string AreaCode { get { return AreaDto != null ? AreaDto.AreaCode.ToString() : AreaUID; } }

        [Description("Базовая ОО")]
        [XmlIgnore] public string SchoolName { get { return SchoolDto.Return(x => x.ToString(), SchoolUID); } }

        [Description("ППОИ")]
        [XmlIgnore] public string PCenterName { get { return PCenterDto.Return(x => x.PCenterName, "---"); } }

        [BulkColumn]
        [Description("Вместимость")]
		public int sVolume { get; set; }

        [BulkColumn("AuditoriumsCount")]
        [Description("Кол-во аудиторий")]
		public int AuditoriumsCountNeeded { get; set; }

        [CsvColumn(Name = "Телефон(ы) ППЭ", FieldIndex = 8)]
        [Description("Телефоны ППЭ")]
		public string Phones { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Адрес(а) электронной почты ППЭ", FieldIndex = 9)]
        [Description("Адреса электронной почты")]
		public string Mails { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Признак расположения ППЭ в ТОМ", FieldIndex = 10), XmlIgnore]
        [Description("Расположено в ТОМ")]
		public bool IsTOM { get; set; }

        #region NonSerialized

        [XmlIgnore] public GovernmentsDto GovernmentDto { get; set; }
        [XmlIgnore] public AreasDto AreaDto { get; set; }
        [XmlIgnore] public SchoolsDto SchoolDto { get; set; }
        [XmlIgnore] public PCentersDto PCenterDto { get; set; }

        [XmlIgnore] public int IsActiveProperty { set { IsActive = Convert.ToBoolean(value); } }
        [XmlIgnore] public int IsTOMProperty { set { IsTOM = Convert.ToBoolean(value); } }
        [XmlIgnore] public int DeleteTypeProperty { set { DeleteType = (DeleteType)value; } }
        [XmlIgnore] public bool IsActive { get; set; }

        [XmlIgnore]
        public AddressDto AddressDto { get; set; }

        #endregion

        [XmlElement]
        [BulkColumn("REGION")]
		public override int Region { get; set; }

        [BulkColumn("AreaID")]
        [CsvColumn(Name = "Guid АТЕ, на территории которой расположен ППЭ", FieldIndex = 12)]
		public Guid Area { get; set; }

        [BulkColumn("SchoolFK", typeof(Guid))]
        [CsvColumn(Name = "Guid ОО, на территории которого находится ППЭ", FieldIndex = 6)]
		public Guid? School { get; set; }

        [BulkColumn("GovernmentID")]
		[CsvColumn(Name = "Guid МСУ, к которому относится ППЭ", FieldIndex = 11)]
		public Guid Government { get; set; }
		
        [CsvColumn(Name = "Guid ППОИ, на территории которого обрабатываются бланки ППЭ", FieldIndex = 7)]
		public Guid? PCenter { get; set; }

        public virtual Guid? Address { get; set; }
       
        [XmlElement("IsActive")]
        public string IsActiveSerialize
        {
            get { return IsActive ? "1" : "0"; }
            set { IsActive = XmlConvert.ToBoolean(value); }
        }

        [BulkColumn]
        [CsvColumn(Name = "Признак удаленной строки", FieldIndex = 2)]
		public DeleteType DeleteType { get; set; }

        [XmlElement("IsTOM")]
        public string IsTOMSerialize
        {
            get { return IsTOM ? "1" : "0"; }
            set { IsTOM = XmlConvert.ToBoolean(value); }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 13, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 14, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        [Description("Фактическое кол-во аудиторий")]
        [XmlIgnore] public int AuditoriumsCountFact { get; set; }
        
        [Description("Фактическая вместимость")]
        [XmlIgnore] public int PlacesCountFact { get; set; }

        [Description("Фактическое количество рабочих мест для устных экзаменов")]
        [XmlIgnore] public int VerbalPlacesCountFact { get; set; }

        #region VideoControl
        [CsvColumn(Name = "Наличие видеонаблюдения", FieldIndex = 15)]
        [XmlIgnore] public int VideoControlProperty
        {
            get { return VideoControl ? 1 : 0; }
            set { VideoControl = Convert.ToBoolean(value); }
        }

        [Description("Наличие online видеонаблюдения")]

        [BulkColumn]
        public bool VideoControl { get; set; }
        
        [XmlElement("VideoControl")] 
        public string VideoControlSerialize
        {
            get { return VideoControl ? "1" : "0"; }
            set { VideoControl = XmlConvert.ToBoolean(value); }
        }
        #endregion

        [Description("Форма экзамена")][XmlIgnore] 
        public string ExamFormNamе { get { return ExamForm.GetDescription(); } }

        [BulkColumn]
        public ExamForm ExamForm { get; set; }
        [CsvColumn(Name = "Форма экзамена", FieldIndex = 16)]
        public int ExamFormInt { get { return (int)ExamForm; } set { ExamForm = (ExamForm)value; } }

        [Description("Доп. признаки ППЭ")][XmlIgnore]
        public string StationFlagsNamе { get { return StationFlags.GetDescription(); } }

        [BulkColumn]
        public StationFlags StationFlags { get; set; }
        [CsvColumn(Name = "Доп. признаки ППЭ", FieldIndex = 17)]
        [XmlElement("StationFlags")]
        public int StationFlagsSerialize { get { return (int)StationFlags; } set { StationFlags = (StationFlags)value; } }

        [XmlIgnore]
        public virtual bool IsHomePPE
        {
            get { return StationFlags.ContainsFlag(StationFlags.HomePPE); }
        }

        public virtual int? TimeZoneId { get; set; }

        #region IEquatable<StationsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StationsDto)) return false;
            return Equals((StationsDto) obj);
        }

        public bool Equals(StationsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.DtoID.Equals(DtoID);
        }

        public override int GetHashCode()
        {
            unchecked { return DtoID.ToString().GetHashCode(); } 
        }

        public override int CompareTo(object obj)
        {
            var other = obj as StationsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Area.Equals(Area), "АТЕ");
            result &= CheckChanges(Equals(other.DeleteType, DeleteType), TypeExtensions.Description<StationsDto>(c => c.IsDeleted));
            result &= CheckChanges(other.StationCode == StationCode, TypeExtensions.Description<StationsDto>(c => c.StationCodeString));
            result &= CheckChanges(StringEquals(other.StationName, StationName), TypeExtensions.Description<StationsDto>(c => c.StationName));
            result &= CheckChanges(StringEquals(other.StationAddress, StationAddress), TypeExtensions.Description<StationsDto>(c => c.StationAddress));
            result &= CheckChanges(other.sVolume == sVolume, TypeExtensions.Description<StationsDto>(c => c.sVolume));
            result &= CheckChanges(other.AuditoriumsCountNeeded == AuditoriumsCountNeeded, TypeExtensions.Description<StationsDto>(c => c.AuditoriumsCountNeeded));
            result &= CheckChanges(StringEquals(other.Phones, Phones), TypeExtensions.Description<StationsDto>(c => c.Phones));
            result &= CheckChanges(StringEquals(other.Mails, Mails), TypeExtensions.Description<StationsDto>(c => c.Mails));
            result &= CheckChanges(other.IsTOM.Equals(IsTOM), TypeExtensions.Description<StationsDto>(c => c.IsTOM));
            result &= CheckChanges(Equals(other.School, School), TypeExtensions.Description<StationsDto>(c => c.SchoolName));
            result &= CheckChanges(other.Government.Equals(Government), TypeExtensions.Description<StationsDto>(c => c.GovernmentName));
            result &= CheckChanges(Equals(other.PCenter, PCenter), TypeExtensions.Description<StationsDto>(c => c.PCenterName));
            result &= CheckChanges(other.VideoControl.Equals(VideoControl), TypeExtensions.Description<StationsDto>(c => c.VideoControl));
            result &= CheckChanges(other.ExamForm == ExamForm, TypeExtensions.Description<StationsDto>(c => c.ExamFormNamе));
            result &= CheckChanges(other.StationFlags == StationFlags, TypeExtensions.Description<StationsDto>(c => c.StationFlagsNamе));
            result &= CheckChanges(other.Address == Address, TypeExtensions.Description<StationsDto>(c => c.StationAddress));
            
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
        public string AreaUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        [BulkColumn("GovernmentID")]
        public string GovernmentUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        [BulkColumn("SchoolFK")]
        public string SchoolUID { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("({0}) {1}", StationCode, StationName);           
        }

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
