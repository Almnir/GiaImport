using System;
using System.ComponentModel;
using System.Data;
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
    [Serializable][Description("Работник ППЭ")]
    [BulkTable("rbd_StationWorkers", "StationWorkers", RootTagName = "ArrayOfStationWorkersDto")]
    public class StationWorkersDto : DtoCreateDateBase, IEquatable<StationWorkersDto>, IPeopleDto,
        IUidableDto, IDtoCollectorAccepter
    {
        [BulkColumn("REGION")]
        [XmlElement]
        public override int Region { get; set; }

        public string FIO { get { return string.Format("{0} {1} {2}", Surname, Name, SecondName); } }

        [Description("Удалён")]
        [XmlIgnore] public override bool IsDeleted { get { return DeleteType != DeleteType.OK; } set { } }

        [Description("Код работника ППЭ")]
        [XmlIgnore] public string StationWorkerCodeString 
        { get { return StationWorkerCode.HasValue ? StationWorkerCode.Value.ToString("000000") : null; } }

        [BulkColumn("StationWorkerCode", typeof(int))]
        [CsvColumn(Name = "Код работника ППЭ", FieldIndex = 3)]
        [Description("Код работника ППЭ")]
		public int? StationWorkerCode { get; set; }

        [BulkColumn("Surname")]
		[CsvColumn(Name = "Фамилия", FieldIndex = 4)]
        [Description("Фамилия")]
		public string Surname { get; set; }

        [BulkColumn("Name")]
		[CsvColumn(Name = "Имя", FieldIndex = 5)]
        [Description("Имя")]
		public string Name { get; set; }

        [BulkColumn("SecondName")]
        [CsvColumn(Name = "Отчество", FieldIndex = 6)]
        [Description("Отчество")]
		public string SecondName { get; set; }

        [Description("Тип документа")]
        [XmlIgnore] public string DocumentTypeName { get { return DocumentTypeDto.Return(x => x.DocumentTypeName, "---"); } }

        [BulkColumn("DocumentSeries")]
		[CsvColumn(Name = "Серия документа", FieldIndex = 11)]
        [Description("Серия документа")]
		public string DocumentSeries { get; set; }

        [BulkColumn("DocumentNumber")]
		[CsvColumn(Name = "Номер документа", FieldIndex = 12)]
        [Description("Номер документа")]
		public string DocumentNumber { get; set; }

        [BulkColumn("BirthYear")]
        [CsvColumn(Name = "Год рождения", FieldIndex = 15)]
        [Description("Год рождения")]
		public int BirthYear { get; set; }

        [Description("Пол")]
        [XmlIgnore] public string SexName { get { return Sex != null ? Sex.Value.GetDescription() : "---"; } }

        [BulkColumn("SchoolPosition")]
        [CsvColumn(Name = "Должность по основному месту работы", FieldIndex = 8)]
        [Description("Должность по основному месту работы")]
		public string SchoolPosition { get; set; }

        [BulkColumn("NotSchoolJob")]
        [CsvColumn(Name = "Основное место работы (если не ОО)", FieldIndex = 9)]
        [Description("Основное место работы (если не ОО)")]
		public string NotSchoolJob { get; set; }

        [Description("МСУ")]
        [XmlIgnore] public string GovernmentName { get { return GovernmentDto.Return(x => x.ToString(), GovernmentUID); } }

        [Description("ОО - основное место работы")]
        [XmlIgnore] public string SchoolName { get { return SchoolDto.Return(x => x.ToString(), SchoolUID); } }

        [BulkColumn("WorkerPositionID")]
        public int? WorkerPosition { get; set; }
        
        [CsvColumn(Name = "Должность в ППЭ", FieldIndex = 20)]
        public int? WorkerPositionCode { get; set; }
        
        [Description("Должность в ППЭ")]
        [XmlIgnore] public string SWorkerPositionName { get { return SWorkerPositionsDto.Return(x => x.SWorkerPositionName, "---"); } }

        [BulkColumn("StationWorkerID")]
        [CsvColumn(Name = "Guid", FieldIndex = 1)]
		public override Guid DtoID { get; set; }

        [BulkColumn("DocumentTypeCode")]
#if GiaDataCollect
        [XmlElement("DocumentTypeCode")]
#endif
        [CsvColumn(Name = "Код типа документа, удостоверяющего личность", FieldIndex = 13)]
		public int? DocumentType { get; set; }

        [BulkColumn("Sex")]
        [CsvColumn(Name = "Пол", FieldIndex = 14, RBDExt =  RBDExtensions.EnumGenderFix)]
		public Gender? Sex { get; set; }

        [BulkColumn("DeleteType")]
        [CsvColumn(Name = "Признак удаленной строки", FieldIndex = 2)]
		public DeleteType DeleteType { get; set; }

        [BulkColumn("GovernmentID")]
        [CsvColumn(Name = "Guid МСУ, к которому прикреплён работник", FieldIndex = 10)]
		public Guid? Government { get; set; }

        [BulkColumn("SchoolID")]
        [CsvColumn(Name = "Guid ОО основного места работы", FieldIndex = 7)]
		public Guid? School { get; set; }
		
        [XmlElement("Imported")]
        public string ImportedSerialize
        {
            get { return Imported ? "1" : "0"; }
            set { Imported = XmlConvert.ToBoolean(value); }
        }

        [BulkColumn("PrecedingYear")]
        [CsvColumn(Name = "Участвовал в ЕГЭ ранее", FieldIndex = 18)]
        public int? PrecedingYear { get; set; }
        
        [Description("Участвовал в ЕГЭ ранее")]
        public string PrecedingYearName
        {
            get
            {
                if (!PrecedingYear.HasValue) return "---";
                return PrecedingYear.Value == 1 ? "Да" : "Нет";
            }
        }

        [BulkColumn("Seniority")]
        [CsvColumn(Name = "Общий преподавательский стаж работы", FieldIndex = 19)]
        [Description("Общий преподавательский стаж работы")]
        public int? Seniority { get; set; }

        [BulkColumn("EducationTypeID")]
        public int? EducationType { get; set; }
        //[CsvColumn(Name = "Уровень проф. образования", FieldIndex = 21)]
        public int? EducationTypeCode { get; set; }
        [Description("Уровень проф. образования")]
        [XmlIgnore] public string EducationTypeName { get { return EducationTypesDto.Return(x => x.EduTypeName, "---"); } }

        [BulkColumn("SWorkerCategory")]
        [Description("Категория")]
        public string SWorkerCategory { get; set; }

        [BulkColumn("CreateDate")]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 16, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn("UpdateDate")]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 17, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        public virtual Guid? Certificate { get; set; }

        #region NonSerialized

        [XmlIgnore] public GovernmentsDto GovernmentDto { get; set; }
        [XmlIgnore] public DocumentTypesDto DocumentTypeDto { get; set; }
        [XmlIgnore] public SchoolsDto SchoolDto { get; set; }
        [XmlIgnore] public SWorkerPositionsDto SWorkerPositionsDto { get; set; }
        [XmlIgnore] public EducationTypesDto EducationTypesDto { get; set; }
        [XmlIgnore] public string Dirty_DocumentTypeName { get; set; }

        [XmlIgnore] public int? SexProperty
        {
            get { return (int?) Sex; }
            set { if (value != null) Sex = (Gender)value; }
        }
        [XmlIgnore] public int DeleteTypeProperty { set { DeleteType = (DeleteType)value; } }
        [XmlIgnore] public int ImportedProperty { set { Imported = Convert.ToBoolean(value); } }
        [XmlIgnore] public bool Imported { get; set; }

        #endregion

        #region IEquatable<StationWorkersDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StationWorkersDto)) return false;
            return Equals((StationWorkersDto) obj);
        }

        public bool Equals(StationWorkersDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.DtoID.Equals(DtoID);
        }

        public override int GetHashCode()
        {
            unchecked { return DtoID.ToString().GetHashCode(); } 
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }

        public override int CompareTo(object obj)
        {
            var other = obj as StationWorkersDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(Equals(other.DeleteType, DeleteType), TypeExtensions.Description<StationWorkersDto>(c => c.IsDeleted));
            result &= CheckChanges(Equals(other.StationWorkerCode, StationWorkerCode), TypeExtensions.Description<StationWorkersDto>(c => c.StationWorkerCode));
            result &= CheckChanges(StringEquals(other.Surname, Surname), TypeExtensions.Description<StationWorkersDto>(c => c.Surname));
            result &= CheckChanges(StringEquals(other.Name, Name), TypeExtensions.Description<StationWorkersDto>(c => c.Name));
            result &= CheckChanges(StringEquals(other.SecondName, SecondName), TypeExtensions.Description<StationWorkersDto>(c => c.SecondName));
            result &= CheckChanges(StringEquals(other.DocumentSeries, DocumentSeries), TypeExtensions.Description<StationWorkersDto>(c => c.DocumentSeries));
            result &= CheckChanges(StringEquals(other.DocumentNumber, DocumentNumber), TypeExtensions.Description<StationWorkersDto>(c => c.DocumentNumber));
            result &= CheckChanges(other.BirthYear == BirthYear, TypeExtensions.Description<StationWorkersDto>(c => c.BirthYear));
            result &= CheckChanges(StringEquals(other.SchoolPosition, SchoolPosition), TypeExtensions.Description<StationWorkersDto>(c => c.SchoolPosition));
            result &= CheckChanges(StringEquals(other.NotSchoolJob, NotSchoolJob), TypeExtensions.Description<StationWorkersDto>(c => c.NotSchoolJob));
            result &= CheckChanges(Equals(other.DocumentType, DocumentType), TypeExtensions.Description<StationWorkersDto>(c => c.DocumentTypeName));
            result &= CheckChanges(Equals(other.Sex, Sex), TypeExtensions.Description<StationWorkersDto>(c => c.SexName));
            result &= CheckChanges(Equals(other.Government, Government), TypeExtensions.Description<StationWorkersDto>(c => c.GovernmentName));
            result &= CheckChanges(Equals(other.School, School), TypeExtensions.Description<StationWorkersDto>(c => c.SchoolName));
            result &= CheckChanges(Equals(other.WorkerPositionCode, WorkerPositionCode), TypeExtensions.Description<StationWorkersDto>(c => c.SWorkerPositionName));
            result &= CheckChanges(Equals(other.PrecedingYear, PrecedingYear), TypeExtensions.Description<StationWorkersDto>(c => c.PrecedingYearName));
            result &= CheckChanges(Equals(other.Seniority, Seniority), TypeExtensions.Description<StationWorkersDto>(c => c.Seniority));
            result &= CheckChanges(Equals(other.EducationTypeCode, EducationTypeCode), TypeExtensions.Description<StationWorkersDto>(c => c.EducationTypeName));
            result &= CheckChanges(StringEquals(other.SWorkerCategory, SWorkerCategory), TypeExtensions.Description<StationWorkersDto>(c => c.SWorkerCategory));
            result &= CheckChanges(Equals(other.Certificate, Certificate), "Сертификат");

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
        public string GovernmentUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string SchoolUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string CertificateKeyUID { get; set; }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
