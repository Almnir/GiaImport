using System;
using System.Collections.Generic;
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
    [Serializable][Description("ОО")]
    [BulkTable("rbd_Schools", "Schools", RootTagName = "ArrayOfSchoolsDto")]
    public class SchoolsDto : DtoCreateDateBase, IEquatable<SchoolsDto>, IDtoCollectorAccepter, IUidableDto
	{
        public SchoolsDto()
        {
            SchoolAddress = new List<SchoolAddressDto>();
        }

        [BulkColumn("SchoolID")]
        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        [Description("Удалена")]
        [XmlIgnore] public override bool IsDeleted  { get { return DeleteType != DeleteType.OK; } set { } }

        [Description("Код ОО")]
        [XmlIgnore] public string SchoolCodeString { get { return SchoolCode.ToString("000000"); } }

        [BulkColumn]
        [CsvColumn(Name = "Код ОО", FieldIndex = 3)]
		public int SchoolCode { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Полное наименование ОО по уставу", FieldIndex = 4)]
        [Description("Полное наименование")]
		public string SchoolName { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Краткое наименование ОО", FieldIndex = 27)]
        [Description("Краткое наименование")]
		public string ShortName { get; set; }

        [Description("Район")]
        [XmlIgnore] public string TownshipName { get { return TownshipDto.Return(x => x.TownshipName, "---"); } }

        [Description("МСУ")]
        [XmlIgnore] public string GovernmentName { get { return GovernmentDto.Return(x => x.ToString(), "---"); } }

        [Description("Вид ОПФ")]
        [XmlIgnore] public string SchoolPropertyName { get { return SchoolPropertyDto.Return(x => x.SchoolPropertyName, "---"); } }

        [Description("Код АТЕ")]
        [XmlIgnore] public string AreaCode { get { return AreaDto.Return(x => x.AreaCode.ToString(), "---"); } }

        [Description("Вид школы")]
        [XmlIgnore] public string SchoolKindName { get { return SchoolKindDto.Return(x => x.SchoolKindName, "---"); } }

        [Description("Тип населенного пункта")]
        [XmlIgnore] public string TownTypeName { get { return TownTypeDto.Return(x => x.TownTypeName, "---"); } }

        [BulkColumn]
        [Description("Юридический адрес ОО")]
        [CsvColumn(Name = "Юридический адрес ОО", FieldIndex = 10)]
		public string LawAddress { get; set; }

        [BulkColumn]
        [Description("Фактический адрес ОО")]
        [CsvColumn(Name = "Фактический адрес ОО", FieldIndex = 11)]
		public string Address { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Должность руководителя ОО", FieldIndex = 12)]
        [Description("Должность руководителя ОО")]
		public string DPosition { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "ФИО работника, отвечающего за подготовку и проведение ЕГЭ", FieldIndex = 19)]
        [Description("ФИО ответственного за ЕГЭ")]
		public string FIO { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Телефоны ОО", FieldIndex = 14)]
        [Description("Телефоны")]
		public string Phones { get; set; }
		
        [CsvColumn(Name = "Факсы ОО", FieldIndex = 15)]
        [Description("Факсы")]
		public string Faxs { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "E-mail ОО", FieldIndex = 16)]
        [Description("E-mail")]
		public string Mails { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Количество обучающихся в выпускных классах", FieldIndex = 17)]
        [Description("Обучающиеся в выпускных классах")]
		public int People11 { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Количество обучающихся в предвыпускных классах", FieldIndex = 18)]
        [Description("Обучающиеся в предвыпускных классах")]
		public int People9 { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "ФИО руководителя ОО", FieldIndex = 13)]
        [Description("ФИО руководителя ОО")]
		public string ChargeFIO { get; set; }

        [CsvColumn(Name = "Адрес WWW – сайта ОО", FieldIndex = 20)]
        [Description("Адрес WWW-сайта")]
		public string WWW { get; set; }

        [Description("Номер лицензии")]
        public string LicNum { get; set; }

        [CsvColumn(Name = "Регистрационный номер лицензии", FieldIndex = 21)]
        [Description("Рег. номер лицензии")]
		public string LicRegNo { get; set; }

        [CsvColumn(Name = "Дата выдачи лицензии на ведение образовательной деятельности", 
            FieldIndex = 22, RBDExt = RBDExtensions.StringToDateFromLongString)]
        [Description("Дата выдачи лицензии")]
		public string LicIssueDate { get; set; }
		
        [CsvColumn(Name = "Дата окончания лицензии на ведение образовательной деятельности", 
            FieldIndex = 23, RBDExt = RBDExtensions.StringToDateFromLongString)]
        [Description("Дата окончания лицензии")]
		public string LicFinishingDate { get; set; }
        
        [Description("Номер свидетельства о гос. аккредитации")]
        public string AccCertNum { get; set; }
		
        [CsvColumn(Name = "Регистрационный номер свидетельства о государственной аккредитации", FieldIndex = 29)]
        [Description("Рег. номер свидетельства о гос. аккредитации")]
		public string AccCertRegNo { get; set; }
		
        [CsvColumn(Name = "Дата выдачи свидетельства о государственной аккредитации", 
            FieldIndex = 24, RBDExt = RBDExtensions.StringToDateFromLongString)]
        [Description("Дата выдачи свидетельства о гос. аккредитации")]
		public string AccCertIssueDate { get; set; }
		
        [CsvColumn(Name = "Дата окончания действия свидетельства о государственной аккредитации", 
            FieldIndex = 25, RBDExt = RBDExtensions.StringToDateFromLongString)]
        [Description("Дата окончания действия свидетельства о гос. аккредитации")]
		public string AccCertFinishingDate { get; set; }

        [CsvColumn(Name = "ОО является специальным пунктом регистрации выпускников прошлых лет", FieldIndex = 26), XmlIgnore]
        [Description("Специальный пункт регистрации")]
		public bool IsVirtualSchool { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Признак расположения ОО в ТОМ", FieldIndex = 30), XmlIgnore]
        [Description("Расположено в ТОМ")]
    	public bool IsTOM { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 31, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 32, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        #region NonSerialized

        [XmlIgnore] public GovernmentsDto GovernmentDto { get; set; }
        [XmlIgnore] public SchoolPropertiesDto SchoolPropertyDto { get; set; }
        [XmlIgnore] public AreasDto AreaDto { get; set; }
        [XmlIgnore] public SchoolKindsDto SchoolKindDto { get; set; }
        [XmlIgnore] public bool IsSchoolKindBroken { get; set; }

        [XmlIgnore] public TownTypesDto TownTypeDto { get; set; }
        [XmlIgnore] public TownshipsDto TownshipDto { get; set; }
        [XmlIgnore] public bool IsTownshipBroken { get; set; }

        [XmlIgnore] public int IsTOMProperty { set { IsTOM = Convert.ToBoolean(value); } }
        [XmlIgnore] public int isVirtualSchoolProperty { set { IsVirtualSchool = Convert.ToBoolean(value); } }
        [XmlIgnore] public int DeleteTypeProperty { set { DeleteType = (DeleteType)value; } }
        [XmlIgnore] public List<SchoolAddressDto> SchoolAddress { get; set; }

        #endregion

        [BulkColumn("GovernmentID")]
		[CsvColumn(Name = "Guid МСУ", FieldIndex = 7)]
		public Guid Government { get; set; }

        [BulkColumn("SchoolKindFK")]
		public short SchoolKind { get; set; }

        [BulkColumn("SchoolPropertyFk")]
		public short SchoolProperty { get; set; }

        [BulkColumn("AreaFK")]
        [CsvColumn(Name = "Guid АТЕ", FieldIndex = 8)]
		public Guid Area { get; set; }

        [BulkColumn("TownTypeFK")]
		public int TownType { get; set; }

        [XmlElement("IsVirtualSchool")]
        public string IsVirtualSchoolSerialize
        {
            get { return IsVirtualSchool ? "1" : "0"; }
            set { IsVirtualSchool = XmlConvert.ToBoolean(value); }
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

        [Description("Доп. признаки ОО")]
        [XmlIgnore]
        public string SchoolFlagsNamе { get { return SchoolFlags.GetDescription(); } }
        [XmlIgnore]
        public SchoolFlags SchoolFlags { get; set; }
        [XmlElement("SchoolFlags")]
        public int SchoolFlagsSerialize { get { return (int)SchoolFlags; } set { SchoolFlags = (SchoolFlags)value; } }


#if GiaDataCollect
        [XmlElement("Region")]
#else
        [XmlElement("REGION")]
#endif
        [BulkColumn("REGION")]
        public override int Region { get; set; }

        [BulkColumn("TownshipFK")]
		public int Township { get; set; }

        [CsvColumn(Name = "Код вида организационно-правовой формы образовательной организации", FieldIndex = 6)]
        public short SchoolPropertyCode { get; set; }
        
        [CsvColumn(Name = "Код вида ОО", FieldIndex = 5)]
        public int SchoolKindCode { get; set; }

        [CsvColumn(Name = "Код ОКАТО", FieldIndex = 28)]
        public int OCATO { get; set; }
        
        [CsvColumn(Name = "Код типа населенного пункта, где расположена ОО", FieldIndex = 9)]
        public int TownTypeCode { get; set; }

        #region IEquatable<SchoolsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SchoolsDto)) return false;
            return Equals((SchoolsDto) obj);
        }

        public bool Equals(SchoolsDto other)
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
            var other = obj as SchoolsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Area.Equals(Area), "АТЕ");
            result &= CheckChanges(Equals(other.DeleteType, DeleteType), TypeExtensions.Description<SchoolsDto>(c => c.IsDeleted));
            result &= CheckChanges(other.SchoolCode == SchoolCode, TypeExtensions.Description<SchoolsDto>(c => c.SchoolCodeString));
            result &= CheckChanges(StringEquals(other.SchoolName, SchoolName), TypeExtensions.Description<SchoolsDto>(c => c.SchoolName));
            result &= CheckChanges(StringEquals(other.ShortName, ShortName), TypeExtensions.Description<SchoolsDto>(c => c.ShortName));
            result &= CheckChanges(StringEquals(other.Address, Address), TypeExtensions.Description<SchoolsDto>(c => c.Address));
            result &= CheckChanges(StringEquals(other.LawAddress, LawAddress), TypeExtensions.Description<SchoolsDto>(c => c.LawAddress));
            result &= CheckChanges(StringEquals(other.DPosition, DPosition), TypeExtensions.Description<SchoolsDto>(c => c.DPosition));
            result &= CheckChanges(StringEquals(other.FIO, FIO), TypeExtensions.Description<SchoolsDto>(c => c.FIO));
            result &= CheckChanges(StringEquals(other.Phones, Phones), TypeExtensions.Description<SchoolsDto>(c => c.Phones));
            result &= CheckChanges(StringEquals(other.Faxs, Faxs), TypeExtensions.Description<SchoolsDto>(c => c.Faxs));
            result &= CheckChanges(StringEquals(other.Mails, Mails), TypeExtensions.Description<SchoolsDto>(c => c.Mails));
            result &= CheckChanges(other.People11 == People11, TypeExtensions.Description<SchoolsDto>(c => c.People11));
            result &= CheckChanges(other.People9 == People9, TypeExtensions.Description<SchoolsDto>(c => c.People9));
            result &= CheckChanges(StringEquals(other.ChargeFIO, ChargeFIO), TypeExtensions.Description<SchoolsDto>(c => c.ChargeFIO));
            result &= CheckChanges(StringEquals(other.WWW, WWW), TypeExtensions.Description<SchoolsDto>(c => c.WWW));
            result &= CheckChanges(StringEquals(other.LicNum, LicNum), TypeExtensions.Description<SchoolsDto>(c => c.LicNum));
            result &= CheckChanges(StringEquals(other.LicRegNo, LicRegNo), TypeExtensions.Description<SchoolsDto>(c => c.LicRegNo));
            result &= CheckChanges(StringEquals(other.LicIssueDate, LicIssueDate), TypeExtensions.Description<SchoolsDto>(c => c.LicIssueDate));
            result &= CheckChanges(StringEquals(other.LicFinishingDate, LicFinishingDate), TypeExtensions.Description<SchoolsDto>(c => c.LicFinishingDate));
            result &= CheckChanges(StringEquals(other.AccCertNum, AccCertNum), TypeExtensions.Description<SchoolsDto>(c => c.AccCertNum));
            result &= CheckChanges(StringEquals(other.AccCertRegNo, AccCertRegNo), TypeExtensions.Description<SchoolsDto>(c => c.AccCertRegNo));
            result &= CheckChanges(StringEquals(other.AccCertIssueDate, AccCertIssueDate), TypeExtensions.Description<SchoolsDto>(c => c.AccCertIssueDate));
            result &= CheckChanges(StringEquals(other.AccCertFinishingDate, AccCertFinishingDate), TypeExtensions.Description<SchoolsDto>(c => c.AccCertFinishingDate));
            result &= CheckChanges(other.IsVirtualSchool.Equals(IsVirtualSchool), TypeExtensions.Description<SchoolsDto>(c => c.IsVirtualSchool));
            result &= CheckChanges(other.IsTOM.Equals(IsTOM), TypeExtensions.Description<SchoolsDto>(c => c.IsTOM));
            result &= CheckChanges(other.Government.Equals(Government), TypeExtensions.Description<SchoolsDto>(c => c.GovernmentName));
            result &= CheckChanges(other.SchoolKind == SchoolKind, TypeExtensions.Description<SchoolsDto>(c => c.SchoolKindName));
            result &= CheckChanges(other.SchoolProperty == SchoolProperty, TypeExtensions.Description<SchoolsDto>(c => c.SchoolPropertyName));
            result &= CheckChanges(other.TownType == TownType, TypeExtensions.Description<SchoolsDto>(c => c.TownTypeName));
            
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
        public string AreaUID { get; set; }
        
        #endregion

        public override string ToString()
        {
            return string.Format("({0}) {1}", SchoolCode, SchoolName);
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
