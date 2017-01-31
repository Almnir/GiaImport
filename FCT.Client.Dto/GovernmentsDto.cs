using System;
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
    [Serializable][Description("МСУ")]
    [BulkTable("rbd_Governments", "Governments", RootTagName = "ArrayOfGovernmentsDto")]
    public class GovernmentsDto : DtoCreateDateBase, IEquatable<GovernmentsDto>, IDtoCollectorAccepter, IUidableDto, IDtoWithCurrentRegion
    {
        [BulkColumn("GovernmentID")]
        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        [Description("Удалено")]
        [XmlIgnore] public override bool IsDeleted { get { return DeleteType != DeleteType.OK; } set { } }

        [BulkColumn]
        [CsvColumn(Name = "Код ОИВ", FieldIndex = 3)]
        [Description("Код МСУ")]
        public int GovernmentCode { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Полное наименование ОИВ", FieldIndex = 4)]
        [Description("Полное наименование МСУ")]
        public string GovernmentName { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Юридический адрес ОИВ, включая почтовый индекс", FieldIndex = 5)]
        [Description("Юридический адрес")]
        public string LawAddress { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Фактический адрес ОИВ, включая почтовый индекс", FieldIndex = 6)]
        [Description("Фактический адрес")]
        public string Address { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "ФИО руководителя ОИВ", FieldIndex = 8)]
        [Description("ФИО руководителя МСУ")]
        public string ChargeFIO { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Телефон(ы) ОИВ", FieldIndex = 9)]
        [Description("Телефоны МСУ")]
        public string Phones { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "E-mail ОИВ", FieldIndex = 11)]
        [Description("E-mail МСУ")]
        public string Mails { get; set; }
        
        [CsvColumn(Name = "Адрес WWW – сайта ОИВ", FieldIndex = 15)]
        [Description("Адрес WWW-сайта")]
        public string WWW { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Должность руководителя  ОИВ", FieldIndex = 7)]
        [Description("Должность руководителя МСУ")]
        public string ChargePosition { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Факс(ы) ОИВ", FieldIndex = 10)]
        [Description("Факсы МСУ")]
        public string Faxes { get; set; }

        [Description("Уровень МСУ")]
        [XmlIgnore] public string GTypeName { get { return GType.GetDescription(); } }

        [BulkColumn]
        [CsvColumn(Name = "ФИО специалиста ОИВ, ответственного за подготовку и проведение ЕГЭ", FieldIndex = 12)]
        [Description("ФИО ответственного за ЕГЭ")]
        public string SpecialistFIO { get; set; }
        
        [CsvColumn(Name = "E-mail специалиста ОИВ, ответственного за подготовку и проведение ЕГЭ", FieldIndex = 13)]
        [Description("E-mail ответственного за ЕГЭ")]
        public string SpecialistMails { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Телефон(ы) специалиста ОИВ, ответственного за подготовку и проведение ЕГЭ", FieldIndex = 14)]
        [Description("Телефоны ответственного за ЕГЭ")]
        public string SpecialistPhones { get; set; }

        [BulkColumn("RegionID")]
        public Guid CurrentRegion { get; set; }

#if GiaDataCollect
        [XmlElement("Region")]
#else
        [XmlElement("REGION")]
#endif
        [BulkColumn("REGION")]
        public override int Region { get; set; }

        [BulkColumn("GType", typeof(int))]
        [CsvColumn(Name = "Уровень ОИВ", FieldIndex = 16, RBDExt = RBDExtensions.EnumGovTypeFix)]
        public MOYOType GType { get; set; }
        
        [CsvColumn(Name = "Признак удаленной строки", FieldIndex = 2)]
        public DeleteType DeleteType { get; set; }

        public int TimeZoneId { get; set; }

        #region NonSerializable

        [XmlIgnore] public CurrentRegionDto CurrentRegionDto { get; set; }

        #endregion

        #region IEquatable<GovernmentsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (GovernmentsDto)) return false;
            return Equals((GovernmentsDto) obj);
        }

        public bool Equals(GovernmentsDto other)
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
            var other = obj as GovernmentsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(Equals(other.DeleteType, DeleteType), TypeExtensions.Description<GovernmentsDto>(c => c.IsDeleted));
            result &= CheckChanges(other.GovernmentCode == GovernmentCode, TypeExtensions.Description<GovernmentsDto>(c => c.GovernmentCode));
            result &= CheckChanges(StringEquals(other.GovernmentName, GovernmentName), TypeExtensions.Description<GovernmentsDto>(c => c.GovernmentName));
            result &= CheckChanges(StringEquals(other.LawAddress, LawAddress), TypeExtensions.Description<GovernmentsDto>(c => c.LawAddress));
            result &= CheckChanges(StringEquals(other.Address, Address), TypeExtensions.Description<GovernmentsDto>(c => c.Address));
            result &= CheckChanges(StringEquals(other.ChargeFIO, ChargeFIO), TypeExtensions.Description<GovernmentsDto>(c => c.ChargeFIO));
            result &= CheckChanges(StringEquals(other.Phones, Phones), TypeExtensions.Description<GovernmentsDto>(c => c.Phones));
            result &= CheckChanges(StringEquals(other.Mails, Mails), TypeExtensions.Description<GovernmentsDto>(c => c.Mails));
            result &= CheckChanges(StringEquals(other.WWW, WWW), TypeExtensions.Description<GovernmentsDto>(c => c.WWW));
            result &= CheckChanges(StringEquals(other.ChargePosition, ChargePosition), TypeExtensions.Description<GovernmentsDto>(c => c.ChargePosition));
            result &= CheckChanges(StringEquals(other.Faxes, Faxes), TypeExtensions.Description<GovernmentsDto>(c => c.Faxes));
            result &= CheckChanges(StringEquals(other.SpecialistFIO, SpecialistFIO), TypeExtensions.Description<GovernmentsDto>(c => c.SpecialistFIO));
            result &= CheckChanges(StringEquals(other.SpecialistMails, SpecialistMails), TypeExtensions.Description<GovernmentsDto>(c => c.SpecialistMails));
            result &= CheckChanges(StringEquals(other.SpecialistPhones, SpecialistPhones), TypeExtensions.Description<GovernmentsDto>(c => c.SpecialistPhones));
            result &= CheckChanges(Equals(other.GType, GType), TypeExtensions.Description<GovernmentsDto>(c => c.GTypeName));

            return result ? 0 : 1;
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("({0}) {1}", GovernmentCode, GovernmentName);           
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
