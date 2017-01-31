using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Extensions;

namespace FCT.Client.Dto.Consolidation
{
    [Serializable][Description(@"Напечатанные свидетельства \ справки")]
    [BulkTable("prnf_CertificatePrintMain", "PrnfCertificatePrintMain", RootTagName = "ArrayOfPrnfCertificatePrintMainDto", IsResTable = true)]
    public class PrnfCertificatePrintMainDto : DtoBase, IEquatable<PrnfCertificatePrintMainDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("CertificatePrintMainID")]
        public override Guid DtoID { get; set; }

        [BulkColumn("REGION")]
        [Description("Регион")]
        public int Region { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, ParticipantUID); } }

        [BulkColumn("ParticipantFK")]
        public Guid ParticipantId { get; set; }

        [BulkColumn]
        [Description("Номер свидетельства")]
        public string LicenseNumber { get; set; }

        [BulkColumn]
        [Description("Время создания записи или печати")]
        public string PrintTime { get; set; }

        [BulkColumn] [XmlIgnore] public int Wave { get; set; }
        [BulkColumn] [XmlIgnore] public int Sex { get; set; }
        [BulkColumn] [XmlIgnore] public int Graduate { get; set; }
        [BulkColumn] [XmlIgnore] public int LicenseDouble { get; set; }
        //[BulkColumn] [XmlIgnore] public int Reserve1 { get; set; }
        //[BulkColumn] [XmlIgnore] public int Reserve5 { get; set; }
        
        #region NonSerializable
        [XmlIgnore] public RegionsDto RegionDto { get; set; }
        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }
        #endregion

        #region IEquatable<PrnfCertificatePrintMainDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PrnfCertificatePrintMainDto)) return false;
            return Equals((PrnfCertificatePrintMainDto)obj);
        }

        public bool Equals(PrnfCertificatePrintMainDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.Region == Region &&
                StringEquals(other.UID, UID);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 37 + Region.GetHashCode();
                result = result * 37 + (string.IsNullOrEmpty(UID) ? 0 : UID.Trim().ToUpper().GetHashCode());
                return result;
            }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as PrnfCertificatePrintMainDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(Equals(other.ParticipantUID, ParticipantUID), "Участник");
            result &= CheckChanges(StringEquals(other.LicenseNumber, LicenseNumber), TypeExtensions.Description<PrnfCertificatePrintMainDto>(c => c.LicenseNumber));
            result &= CheckChanges(StringEquals(other.PrintTime, PrintTime), TypeExtensions.Description<PrnfCertificatePrintMainDto>(c => c.PrintTime));
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
        public string ParticipantUID { get; set; }

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
