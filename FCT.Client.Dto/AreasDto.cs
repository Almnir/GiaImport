using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable][Description("АТЕ")]
    [BulkTable("rbd_Areas", "Areas", RootTagName = "ArrayOfAreasDto")]
    public class AreasDto : RegionDtoBase, IEquatable<AreasDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("AreaID")]
        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Признак удаленной строки", FieldIndex = 2)]
        [Description("Удалено")]
		public override bool IsDeleted { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Код административно-территориальной единицы", FieldIndex = 3)]
        [Description("Код АТЕ")]
        public int AreaCode { get; set; }

        [BulkColumn]
        [CsvColumn(Name = "Наименование административно-территориальной единицы", FieldIndex = 4)]
        [Description("Наименование АТЕ")]
		public string AreaName { get; set; }

        [BulkColumn]
        public override int Region { get; set; }

        /// OBSOLETE: НЕТ В ИНТЕРФЕЙСЕ
        public string LawAddress { get; set; }
        /// OBSOLETE: НЕТ В ИНТЕРФЕЙСЕ
        public string Address { get; set; }

        [CsvColumn(Name = "ФИО сотрудника, ответственного за проведение ЕГЭ", FieldIndex = 5)]
        [Description("ФИО ответственного за ЕГЭ")]
		public string ChargeFIO { get; set; }

        [CsvColumn(Name = "Телефон(ы) сотрудника, ответственного за проведение ЕГЭ", FieldIndex = 6)]
        [Description("Телефоны ответственного за ЕГЭ")]
		public string Phones { get; set; }

        [CsvColumn(Name = "Адрес(а) электронной почты сотрудника, ответственного за проведение ЕГЭ", FieldIndex = 7)]
        [Description("Email ответственного за ЕГЭ")]
		public string Mails { get; set; }

        /// OBSOLETE: НЕТ В ИНТЕРФЕЙСЕ
        public string WWW { get; set; }

        [BulkColumn]
        [XmlElement("IsDeleted")] 
        public string IsDeletedSerialize 
        {
            get { return IsDeleted ? "1" : "0"; } 
            set { IsDeleted = XmlConvert.ToBoolean(value); } 
        }

        //[XmlIgnore]
        public int IsDeletedProperty { set { IsDeleted = Convert.ToBoolean(value); } }

        #region NonSerializable
        

        #endregion

        #region IEquatable<AreasDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AreasDto)) return false;
            return Equals((AreasDto) obj);
        }

        public bool Equals(AreasDto other)
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
            var other = obj as AreasDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.IsDeleted.Equals(IsDeleted), TypeExtensions.Description<AreasDto>(c => c.IsDeleted));
            result &= CheckChanges(other.AreaCode == AreaCode, TypeExtensions.Description<AreasDto>(c => c.AreaCode));
            result &= CheckChanges(StringEquals(other.AreaName, AreaName), TypeExtensions.Description<AreasDto>(c => c.AreaName));
            result &= CheckChanges(StringEquals(other.ChargeFIO, ChargeFIO), TypeExtensions.Description<AreasDto>(c => c.ChargeFIO));
            result &= CheckChanges(StringEquals(other.Phones, Phones), TypeExtensions.Description<AreasDto>(c => c.Phones));
            result &= CheckChanges(StringEquals(other.Mails, Mails), TypeExtensions.Description<AreasDto>(c => c.Mails));
            result &= CheckChanges(StringEquals(other.LawAddress, LawAddress), "LawAddress");
            result &= CheckChanges(StringEquals(other.Address, Address), "Address");
            result &= CheckChanges(StringEquals(other.WWW, WWW), "WWW");

            return result ? 0 : 1;
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get; set; }

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
