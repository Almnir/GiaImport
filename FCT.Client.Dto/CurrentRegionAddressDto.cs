using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Адреса РЦОИ")]
    public class CurrentRegionAddressDto : DtoCreateDateBase, IEquatable<CurrentRegionAddressDto>, IDtoWithAddress, IDtoWithAddressType, IDtoWithCurrentRegion
    {
        [XmlElement]
        public override int Region { get; set; }

        public override Guid DtoID { get; set; }

        public virtual Guid CurrentRegion { get; set; }

        public virtual Guid Address { get; set; }

        public virtual int AddressType { get; set; }

        #region NonSerializable

        [XmlIgnore][Description("РЦОИ")]
        public CurrentRegionDto CurrentRegionDto { get; set; }

        [XmlIgnore]
        public int IsDeletedProperty { set { IsDeleted = Convert.ToBoolean(value); } }

        [XmlIgnore]
        public AddressDto AddressDto { get; set; }

        [XmlIgnore]
        [Description("Адрес")]
        public string AddressDescription { get; set; }

        [XmlIgnore]
        [Description("Тип адреса")]
        public AddressTypeDto AddressTypeDto { get; set; }

        #endregion

        #region IEquatable<CurrentRegionAddress> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CurrentRegionAddressDto)) return false;
            return Equals((CurrentRegionAddressDto)obj);
        }

        public bool Equals(CurrentRegionAddressDto other)
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
            var other = obj as CurrentRegionAddressDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(CurrentRegion == other.CurrentRegion, TypeExtensions.Description<CurrentRegionAddressDto>(c => c.CurrentRegion));
            result &= CheckChanges(Address == other.Address, TypeExtensions.Description<CurrentRegionAddressDto>(c => c.Address));
            result &= CheckChanges(AddressType == other.AddressType, TypeExtensions.Description<CurrentRegionAddressDto>(c => c.AddressType));

            return result ? 0 : 1;
        }

        #endregion

        public override string ToString()
        {
            return AddressDto.ToString();
        }

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
