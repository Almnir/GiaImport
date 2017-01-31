using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Enums;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable][Description("Участник")]
    [BulkTable("rbd_Participants", "Participants", RootTagName = "ArrayOfParticipantsDto")]
    public class ParticipantsDto : DtoCreateDateBase, IEquatable<ParticipantsDto>, IDtoCollectorAccepter, IUidableDto, IDtoWithDocument, IPeopleDto
    {
        [BulkColumn("ParticipantID")]
        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        public ParticipantsDto()
        {
            DuplicatesIds = new HashSet<Guid>();
        }

        [Description("Удалён")]
        [XmlIgnore] public override bool IsDeleted
        {
            get { return DeleteType != DeleteType.OK && DeleteType != DeleteType.Dublicate; } set {}
        }

        //[Description("Код участника")]
        [BulkColumn]
		public string ParticipantCode { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Фамилия", FieldIndex = 3)]
        [Description("Фамилия")]
        public string Surname { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Имя", FieldIndex = 4)]
        [Description("Имя")]
		public string Name { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Отчество", FieldIndex = 5)]
        [Description("Отчество")]
		public string SecondName { get; set; }
		
        [Description("Тип документа")]
        [XmlIgnore] public string DocumentTypeName 
        { get { return DocumentTypeDto.Return(x => x.DocumentTypeName, "---"); } }

        [BulkColumn]
        [CsvColumn(Name = "Серия документа", FieldIndex = 6)]
        [Description("Серия документа")]
		public string DocumentSeries { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Номер документа", FieldIndex = 7)]
        [Description("Номер документа")]
		public string DocumentNumber { get; set; }

        [Description("Регион в котором окончил ОО")]
        [XmlIgnore] public string FinishRegionName 
        { get { return FinishRegionDto.Return(x => x.RegionName, "---"); } }

        [Description("Категория участника")]
        [XmlIgnore] public string ParticipantCategoryName 
        { get { return ParticipantCategoryDto.Return(x => x.CategoryName, "---"); } }

        [Description("Выпускная ОО")]
        [XmlIgnore] public string SchoolOutcomingName 
        { get { return SchoolOutcomingDto != null ? string.Format("({0}) {1}", 
            SchoolOutcomingDto.SchoolCode, SchoolOutcomingDto.SchoolName) : "---"; } }

        [Description("Приоритетная ОО")]
        [XmlIgnore] public string SchoolRegistrationName 
        { get { return SchoolRegistrationDto != null ? string.Format("({0}) {1}", 
            SchoolRegistrationDto.SchoolCode, SchoolRegistrationDto.SchoolName) : "---"; } }

        [Description("Форма обучения")]
        [XmlIgnore] public string StudyName { get { return StudyDto != null ? StudyDto.Name : "---"; } }

        [Description("Пол")]
        [XmlIgnore] public string SexName { get { return Sex.GetDescription(); } }

        [Description("Форма ГИА")]
        [XmlIgnore] public string GiaName { get { return Gia.GetDescription(); } }

        [Description("Результат ГИА")]
        [XmlIgnore] public string GiaAcceptName { get { return GiaAccept.GetDescription(); } }

        [Description("Принцип рассадки")]
        [XmlIgnore] public string LimitPotencialName { get { return LimitPotencial.GetDescription(); } }

        [Description("Гражданство")]
        [XmlIgnore] public string CitizenshipName { get { return CitizenshipDto != null ? CitizenshipDto.CitizenshipName : "---"; } }

        [BulkColumn("pClass")]
        [CsvColumn(Name = "Класс", FieldIndex = 10)]
        [Description("Класс")]
		public string PClass { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Дата рождения", FieldIndex = 11, OutputFormat = "dd.MM.yyyy")]
        [Description("Дата рождения")]
		public DateTime BirthDay { get; set; }

        #region NonSerialized

        [XmlIgnore] public string FIO { get { return string.Format("{0} {1} {2}", Surname, Name, SecondName); } }
        [XmlIgnore] public DocumentTypesDto DocumentTypeDto { get; set; }
        [XmlIgnore] public RegionsDto FinishRegionDto { get; set; }
        [XmlIgnore] public ParticipantCategoriesDto ParticipantCategoryDto { get; set; }
        [XmlIgnore] public SchoolsDto SchoolOutcomingDto { get; set; }
        [XmlIgnore] public SchoolsDto SchoolRegistrationDto { get; set; }
        [XmlIgnore] public StudyDto StudyDto { get; set; }
        [XmlIgnore] public CitizenshipDto CitizenshipDto { get; set; }
        [XmlIgnore] public ParticipantsDto ParticipantDoubleDto { get; set; }
        [XmlIgnore] public ParticipantPropertiesDto ParticipantPropertyDto { get; set; }
        
        [XmlIgnore] public int SexProperty { set { Sex = (Gender)value; } }
        [XmlIgnore] public int GiaProperty { set { Gia = (Gia)value; } }
        [XmlIgnore] public int GiaAcceptProperty { set { GiaAccept = (GiaAccept)value; } }
        [XmlIgnore] public int LimitPotencialProperty { set { LimitPotencial = (LimitPotencial)value; } }
        [XmlIgnore] public int DeleteTypeProperty { set { DeleteType = (DeleteType)value; } }

        [XmlIgnore]
        [CsvColumn(Name = "Снилс", FieldIndex = 23)]
        public string SnilsFromCsv { get; set; }

        #endregion

        [BulkColumn]
        [XmlElement]
		public override int Region { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Код типа документа, удостоверяющего личность", FieldIndex = 8)]
		public int DocumentTypeCode { get; set; }

        [BulkColumn("Sex", typeof(int))]
        [CsvColumn(Name = "Пол", FieldIndex = 9, RBDExt =  RBDExtensions.EnumGenderFix)]
		public Gender Sex { get; set; }

        [BulkColumn("Gia", typeof(int))]
        [CsvColumn(Name = "Форма ГИА", FieldIndex = 19)]
        [XmlIgnore] public Gia Gia { get; set; }

        [XmlElement("Gia")]
        public int GiaInt
        {
            get { return (int)Gia; }
            set { Gia = (Gia)value; }
        }

        [BulkColumn("GiaAccept", typeof(int))]
        [CsvColumn(Name = "Действующие результаты ГИА", FieldIndex = 20)]
        [XmlIgnore] public GiaAccept GiaAccept { get; set; }

        [XmlElement("GiaAccept")]
        public int GiaAcceptInt
        {
            get { return (int) GiaAccept; }
            set { GiaAccept = (GiaAccept) value; }
        }

        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }

        [CsvColumn(Name = "Признак удаленной строки", FieldIndex = 2)]
		public DeleteType DeleteType { get; set; }

        [BulkColumn("LimitPotencial", typeof(int))]
        [CsvColumn(Name = "Признак специализированной рассадки", FieldIndex = 12)]
		public LimitPotencial LimitPotencial { get; set; }

    	public Guid? ParticipantDouble { get; set; }

        [CsvColumn(Name = "Код субъекта Российской Федерации, в котором участник закончил ОО", FieldIndex = 14)]
		public int? FinishRegion { get; set; }

        [BulkColumn("ParticipantCategoryFK")]
        public int ParticipantCategory { get; set; }

        [CsvColumn(Name = "Код категории участника", FieldIndex = 13)]
        public int ParticipantCategoryCode { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Guid ОО, основная ОО участника", FieldIndex = 15)]
        public Guid SchoolRegistration { get; set; }

        [CsvColumn(Name = "Guid ОО, выпускная ОО", FieldIndex = 16)]
        public Guid? SchoolOutcoming { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Код формы обучения", FieldIndex = 21)]
        public int Study { get; set; }

        [BulkColumn("CitizenshipID", typeof(int))]
        public int Citizenship { get; set; }

        [CsvColumn(Name = "Код гражданства", FieldIndex = 22)]
        public int CitizenshipCode { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 17, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate { get { return base.CreateDate; } set { base.CreateDate = value; } }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 18, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate { get { return base.UpdateDate; } set { base.UpdateDate = value; } }

        [BulkColumn]
        [CsvColumn(Name = "", FieldIndex = 19, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime ImportCreateDate { get { return base.ImportCreateDate; } set { base.ImportCreateDate = value; } }

        [BulkColumn]
        [CsvColumn(Name = "", FieldIndex = 20, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime ImportUpdateDate { get { return base.ImportUpdateDate; } set { base.ImportUpdateDate = value; } }

        [Description("Заблокирован в БД")]
        [XmlIgnore] public bool IsBlockedDbProperty { get; set; }

        [Description("Заблокирован в импорте")]
        [XmlIgnore] public bool IsBlockedImportProperty { get; set; }

        [Description("Дата блокировки")]
        public string BlockedDateProperty { get; set; }

        [Description("Причина блокировки")]
        [XmlIgnore] public string BlockedReasonProperty { get; set; }
        
        [Description("Является дублем")]
        [XmlIgnore] public bool IsDuplicate { get { return ParticipantDouble.HasValue || DeleteType == DeleteType.Dublicate; } set {} }

        [Description("Кол-во дублей")]
        [XmlIgnore] public int DuplicatesCount { get { return DuplicatesIds.Count; } }
        [XmlIgnore] public HashSet<Guid> DuplicatesIds { get; set; }
        [XmlIgnore] public bool HasDuplicatesInDb { get; set; }

        #region IEquatable<ParticipantsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ParticipantsDto)) return false;
            return Equals((ParticipantsDto) obj);
        }

        public static bool operator ==(ParticipantsDto left, ParticipantsDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ParticipantsDto left, ParticipantsDto right)
        {
            return !Equals(left, right);
        }

        public bool Equals(ParticipantsDto other)
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
            var other = obj as ParticipantsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.ParticipantCode, ParticipantCode), TypeExtensions.Description<ParticipantsDto>(c => c.ParticipantCode));
            result &= CheckChanges(StringEquals(other.Surname, Surname), TypeExtensions.Description<ParticipantsDto>(c => c.Surname));
            result &= CheckChanges(StringEquals(other.Name, Name), TypeExtensions.Description<ParticipantsDto>(c => c.Name));
            result &= CheckChanges(StringEquals(other.SecondName, SecondName), TypeExtensions.Description<ParticipantsDto>(c => c.SecondName));
            result &= CheckChanges(StringEquals(other.DocumentSeries, DocumentSeries), TypeExtensions.Description<ParticipantsDto>(c => c.DocumentSeries));
            result &= CheckChanges(StringEquals(other.DocumentNumber, DocumentNumber), TypeExtensions.Description<ParticipantsDto>(c => c.DocumentNumber));
            result &= CheckChanges(StringEquals(other.PClass, PClass), TypeExtensions.Description<ParticipantsDto>(c => c.PClass));
            result &= CheckChanges(other.BirthDay.Equals(BirthDay), TypeExtensions.Description<ParticipantsDto>(c => c.BirthDay));
            result &= CheckChanges(other.DocumentTypeCode == DocumentTypeCode, TypeExtensions.Description<ParticipantsDto>(c => c.DocumentTypeName));
            result &= CheckChanges(Equals(other.Sex, Sex), TypeExtensions.Description<ParticipantsDto>(c => c.SexName));
            result &= CheckChanges(Equals(other.Gia, Gia), TypeExtensions.Description<ParticipantsDto>(c => c.GiaName));
            result &= CheckChanges(Equals(other.DeleteType, DeleteType), DeleteType != DeleteType.Dublicate ? "Удален" : "Признак - Дубль");
            result &= CheckChanges(Equals(other.LimitPotencial, LimitPotencial), TypeExtensions.Description<ParticipantsDto>(c => c.LimitPotencialName));
            result &= CheckChanges(Equals(other.ParticipantDouble, ParticipantDouble), TypeExtensions.Description<ParticipantsDto>(c => c.IsDuplicate));
            result &= CheckChanges(Equals(other.FinishRegion, FinishRegion), TypeExtensions.Description<ParticipantsDto>(c => c.FinishRegionName));
            result &= CheckChanges(other.ParticipantCategory == ParticipantCategory, TypeExtensions.Description<ParticipantsDto>(c => c.ParticipantCategoryName));
            result &= CheckChanges(other.SchoolRegistration.Equals(SchoolRegistration), TypeExtensions.Description<ParticipantsDto>(c => c.SchoolRegistrationName));
            result &= CheckChanges(Equals(other.SchoolOutcoming, SchoolOutcoming), TypeExtensions.Description<ParticipantsDto>(c => c.SchoolOutcomingName));
            result &= CheckChanges(other.Study == Study, TypeExtensions.Description<ParticipantsDto>(c => c.StudyName));
            result &= CheckChanges(Equals(other.CitizenshipCode, CitizenshipCode), TypeExtensions.Description<ParticipantsDto>(c => c.CitizenshipName));
            result &= CheckChanges(Equals(other.GiaAccept, GiaAccept), TypeExtensions.Description<ParticipantsDto>(c => c.GiaAcceptName));

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
        public string SchoolRegistrationUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public int CitizenshipUID { get; set; }

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
