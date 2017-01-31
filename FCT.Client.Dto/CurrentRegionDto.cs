using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable][Description("Информация о субъекте РФ")]
    [BulkTable("rbd_CurrentRegion", "CurrentRegion", RootTagName = "ArrayOfCurrentRegionDto")]
    public class CurrentRegionDto : RegionDtoBase, IEquatable<CurrentRegionDto>, IDtoCollectorAccepter, IUidableDto
    {
        public CurrentRegionDto()
        {
            CurrentRegionAddress = new List<CurrentRegionAddressDto>();
        }

        [BulkColumn("ID")]
        public override Guid DtoID { get; set; }

        #region NonSerializable

        [Description("Регион")]
        [XmlIgnore] public override string RegionName { get { return RegionDto.Return(x => x.RegionName, "---"); } }

        [XmlIgnore]
        public List<CurrentRegionAddressDto> CurrentRegionAddress { get; set; }
        #endregion

#if GiaDataCollect
        [XmlElement("Region")]
#else
        [XmlElement("REGION")]
#endif
        [BulkColumn("REGION")]
        public override int Region { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Полное наименование организации, выполняющей функции РЦОИ субъекта Российской Федерации", FieldIndex = 1)]
        [Description("Полное наименование")]
		public string Name { get; set; }

        [BulkColumn]
		public string RCOIName { get; set; }

        [BulkColumn]
        [Description("Юридический адрес")]
        [CsvColumn(Name = "Юридический адрес", FieldIndex = 2)]
		public string RCOILawAddress { get; set; }

        [BulkColumn]
        [Description("Фактический адрес")]
        [CsvColumn(Name = "Фактический адрес", FieldIndex = 3)]
		public string RCOIAddress { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "Организационно-правовая форма РЦОИ", FieldIndex = 4)]
        [Description("Организационно-правовая форма")]
		public string RCOIProperty { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "Должность руководителя РЦОИ субъекта Российской Федерации", FieldIndex = 5)]
        [Description("Должность руководителя")]
		public string RCOIDPosition { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "ФИО руководителя РЦОИ субъекта Российской Федерации", FieldIndex = 6)]
        [Description("ФИО руководителя")]
		public string RCOIDFio { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "Телефон(ы) РЦОИ субъекта Российской Федерации", FieldIndex = 7)]
        [Description("Телефоны")]
		public string RCOIPhones { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "Факс(ы) РЦОИ субъекта Российской Федерации", FieldIndex = 8)]
        [Description("Факсы")]
		public string RCOIFaxs { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "E-mail РЦОИ субъекта Российской Федерации", FieldIndex = 9)]
        [Description("E-mail")]
		public string RCOIEMails { get; set; }
        
        public string GEKAddress { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "ФИО председателя ГЭК субъекта Российской Федерации", FieldIndex = 10)]
        [Description("ФИО председателя")]
		public string GEKDFio { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "Телефон(ы) ГЭК субъекта Российской Федерации", FieldIndex = 11)]
        [Description("Телефоны ГЭК")]
		public string GEKPhones { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "Факс(ы) ГЭК субъекта Российской Федерации", FieldIndex = 12)]
        [Description("Факсы ГЭК")]
		public string GEKFaxs { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "E-mail ГЭК субъекта Российской Федерации", FieldIndex = 13)]
        [Description("E-mail ГЭК")]
		public string GEKEMails { get; set; }

        [BulkColumn]
		[CsvColumn(Name = "Адрес(а) WWW – сайта(ов), посвященные проведению ЕГЭ в субъекте Российской Федерации", FieldIndex = 14)]
        [Description("Адрес WWW-сайта")]
		public string EGEWWW { get; set; }

        #region IEquatable<CurrentRegionDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (CurrentRegionDto)) return false;
            return Equals((CurrentRegionDto) obj);
        }

        public bool Equals(CurrentRegionDto other)
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
            var other = obj as CurrentRegionDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.Name, Name), TypeExtensions.Description<CurrentRegionDto>(c => c.Name));
            result &= CheckChanges(StringEquals(other.RCOIName, RCOIName), TypeExtensions.Description<CurrentRegionDto>(c => c.RCOIName));
            result &= CheckChanges(StringEquals(other.RCOIProperty, RCOIProperty), TypeExtensions.Description<CurrentRegionDto>(c => c.RCOIProperty));
            result &= CheckChanges(StringEquals(other.RCOIDPosition, RCOIDPosition), TypeExtensions.Description<CurrentRegionDto>(c => c.RCOIDPosition));
            result &= CheckChanges(StringEquals(other.RCOIDFio, RCOIDFio), TypeExtensions.Description<CurrentRegionDto>(c => c.RCOIDFio));
            result &= CheckChanges(StringEquals(other.RCOIPhones, RCOIPhones), TypeExtensions.Description<CurrentRegionDto>(c => c.RCOIPhones));
            result &= CheckChanges(StringEquals(other.RCOIFaxs, RCOIFaxs), TypeExtensions.Description<CurrentRegionDto>(c => c.RCOIFaxs));
            result &= CheckChanges(StringEquals(other.RCOIEMails, RCOIEMails), TypeExtensions.Description<CurrentRegionDto>(c => c.RCOIEMails));
            result &= CheckChanges(StringEquals(other.GEKAddress, GEKAddress), TypeExtensions.Description<CurrentRegionDto>(c => c.GEKAddress));
            result &= CheckChanges(StringEquals(other.GEKDFio, GEKDFio), TypeExtensions.Description<CurrentRegionDto>(c => c.GEKDFio));
            result &= CheckChanges(StringEquals(other.GEKPhones, GEKPhones), TypeExtensions.Description<CurrentRegionDto>(c => c.GEKPhones));
            result &= CheckChanges(StringEquals(other.GEKFaxs, GEKFaxs), TypeExtensions.Description<CurrentRegionDto>(c => c.GEKFaxs));
            result &= CheckChanges(StringEquals(other.GEKEMails, GEKEMails), TypeExtensions.Description<CurrentRegionDto>(c => c.GEKEMails));
            result &= CheckChanges(StringEquals(other.EGEWWW, EGEWWW), TypeExtensions.Description<CurrentRegionDto>(c => c.EGEWWW));

            return result ? 0 : 1;             
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get { return Region.ToString(); } set { } }

        #endregion

        public override string ToString()
        {
            return Name;
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
