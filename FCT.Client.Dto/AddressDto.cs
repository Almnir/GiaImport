using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Адрес")]
    public class AddressDto : DtoCreateDateBase, IEquatable<AddressDto>
    {
        [XmlElement]
        public override int Region { get; set; }

        public override Guid DtoID { get; set; }

        [Description("Индекс")]
        public virtual string ZipCode { get; set; }

        public virtual int LocalityType { get; set; }

        [Description("Наименование населенного пункта")]
        public virtual string LocalityName { get; set; }

        public virtual int StreetType { get; set; }

        [Description("Наименование улицы")]
        public virtual string StreetName { get; set; }

        public virtual int BuildingType { get; set; }

        [Description("Номер строения")]
        public virtual string BuildingNumber { get; set; }

        public virtual int Township { get; set; }

        public int OCATO { get; set; }

        #region NonSerializable

        [XmlIgnore]
        [Description("Район")]
        public TownshipsDto TownshipDto { get; set; }

        [XmlIgnore]
        [Description("Тип нас. пункта")]
        public LocalityTypeDto LocalityTypeDto { get; set; }

        [XmlIgnore]
        [Description("Тип улицы")]
        public StreetTypeDto StreetTypeDto { get; set; }

        [XmlIgnore]
        [Description("Тип здания")]
        public BuildingTypeDto BuildingTypeDto { get; set; }

        [XmlIgnore]
        public bool IsTownshipBroken { get; set; }

        #endregion

        #region IEquatable<AddressDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AddressDto)) return false;
            return Equals((AddressDto)obj);
        }

        public bool Equals(AddressDto other)
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
            var other = obj as AddressDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(StringEquals(other.ZipCode, ZipCode), TypeExtensions.Description<AddressDto>(c => c.ZipCode));
            result &= CheckChanges(StringEquals(other.LocalityName, LocalityName), TypeExtensions.Description<AddressDto>(c => c.LocalityName));
            result &= CheckChanges(StringEquals(other.StreetName, StreetName), TypeExtensions.Description<AddressDto>(c => c.StreetName));
            result &= CheckChanges(StringEquals(other.BuildingNumber, BuildingNumber), TypeExtensions.Description<AddressDto>(c => c.BuildingNumber));
            result &= CheckChanges(other.LocalityType == LocalityType, TypeExtensions.Description<AddressDto>(c => c.LocalityTypeDto));
            result &= CheckChanges(other.StreetType == StreetType, TypeExtensions.Description<AddressDto>(c => c.StreetTypeDto));
            result &= CheckChanges(other.BuildingType == BuildingType, TypeExtensions.Description<AddressDto>(c => c.BuildingTypeDto));

            return result ? 0 : 1;
        }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
