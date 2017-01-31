using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("ППОИ")]
    public class PCentersDto : DtoCreateDateBase, IEquatable<PCentersDto>
    {
        [CsvColumn(Name = "Признак удаленной строки", FieldIndex = 2), XmlIgnore]
        [Description("Удалено")]
        public override bool IsDeleted { get; set; }

        [CsvColumn(Name = "Код ППОИ", FieldIndex = 3)]
        [Description("Код ППОИ")]
        public int PCenterCode { get; set; }

        [CsvColumn(Name = "Наименование ППОИ", FieldIndex = 4)]
        [Description("Наименование ППОИ")]
        public string PCenterName { get; set; }

        [CsvColumn(Name = "Адрес ППОИ", FieldIndex = 5)]
        [Description("Адрес ППОИ")]
        public string PCenterAddress { get; set; }

        [CsvColumn(Name = "ФИО ответственного за обработку бланков в ППОИ", FieldIndex = 6)]
        [Description("ФИО ответственного лица")]
        public string ChargeFIO { get; set; }

        [CsvColumn(Name = "Телефоны ответственного за обработку бланков в ППОИ", FieldIndex = 7)]
        [Description("Телефоны ответственного лица")]
        public string Phones { get; set; }

        [CsvColumn(Name = "Адрес электронной почты ответственного за обработку бланков в ППОИ", FieldIndex = 8)]
        [Description("E-mail ответственного лица")]
        public string Mails { get; set; }

        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        [XmlElement("REGION")]
        public override int Region { get; set; }

        [XmlElement("IsDeleted")]
        public string IsDeletedSerialize
        {
            get { return IsDeleted ? "1" : "0"; }
            set { IsDeleted = XmlConvert.ToBoolean(value); }
        }

        #region NonSerializable

        [XmlIgnore] public int IsDeletedProperty { set { IsDeleted = Convert.ToBoolean(value); } }

        #endregion

        #region IEquatable<PCentersDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PCentersDto)) return false;
            return Equals((PCentersDto) obj);
        }

        public bool Equals(PCentersDto other)
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
            var other = obj as PCentersDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.IsDeleted.Equals(IsDeleted), TypeExtensions.Description<PCentersDto>(c => c.IsDeleted));
            result &= CheckChanges(other.PCenterCode == PCenterCode, TypeExtensions.Description<PCentersDto>(c => c.PCenterCode));
            result &= CheckChanges(StringEquals(other.PCenterName, PCenterName), TypeExtensions.Description<PCentersDto>(c => c.PCenterName));
            result &= CheckChanges(StringEquals(other.PCenterAddress, PCenterAddress), TypeExtensions.Description<PCentersDto>(c => c.PCenterAddress));
            result &= CheckChanges(StringEquals(other.ChargeFIO, ChargeFIO), TypeExtensions.Description<PCentersDto>(c => c.ChargeFIO));
            result &= CheckChanges(StringEquals(other.Phones, Phones), TypeExtensions.Description<PCentersDto>(c => c.Phones));
            result &= CheckChanges(StringEquals(other.Mails, Mails), TypeExtensions.Description<PCentersDto>(c => c.Mails));

            return result ? 0 : 1;	        
	    }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
	}
}
