using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Сертификат")]
    public class CertificateKeysDto : DtoCreateDateBase, IEquatable<CertificateKeysDto>
    {
        [XmlElement]
        public override int Region { get; set; }

        [Description("Тип сертификата")]
        public virtual int CertificateKeyType { get; set; }

        [Description("Данные сертификата")]
        public virtual byte[] CertificateKey { get; set; }

        #region NonSerializable
        
        #endregion

        #region IEquatable<AddressDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CertificateKeysDto)) return false;
            return Equals((CertificateKeysDto)obj);
        }

        public bool Equals(CertificateKeysDto other)
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
            var other = obj as CertificateKeysDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.CertificateKeyType == CertificateKeyType, TypeExtensions.Description<CertificateKeysDto>(c => c.CertificateKeyType));
            result &= CheckChanges(other.CertificateKey.SequenceEqual(CertificateKey), TypeExtensions.Description<CertificateKeysDto>(c => c.CertificateKey));

            return result ? 0 : 1;
        }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
