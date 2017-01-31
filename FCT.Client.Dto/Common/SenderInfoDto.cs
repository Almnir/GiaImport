using System;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using RBD.Client.Services.Export;
using RBD.Common.Enums;

namespace RBD.Client.Services.Import.DataSource
{
    [Serializable]
    public class SenderInfo : IEquatable<SenderInfo>
    {
        public SenderInfo()
        {
            ExportSettings = new ExportSettings();
        }

        public string Version { get; set; }
        public DateTime ExportDate { get; set; }

        public Guid MOYOId
	    {
	        get { return SenderMOYO != null ? SenderMOYO.DtoID : Guid.Empty; }
	    }

        public Guid OYId
	    {
	        get { return SenderOY != null ? SenderOY.DtoID : Guid.Empty; }
	    }

        /// <summary>
        /// Тип отправителя
        /// </summary>
        public ImportSenderType SenderType { get; set; }
		public GovernmentKeyDto SenderMOYO { get; set; }
		public SchoolKeyDto SenderOY { get; set; }
        [XmlIgnore] public Guid SenderId { get; set; }

        public string CodeName { get { return string.Format("{0} - {1}", SenderCode, SenderName); } }
        public string ExCodeName { get; set; }

        public string SenderCode
        { 
            get
            {
                if (SenderOY != null) return SenderOY.SchoolCode.ToString();
                if (SenderMOYO != null) return SenderMOYO.GovernmentCode.ToString();
                return string.Empty;
            }
        }
        public string SenderName 
        { 
            get
            {
                if (SenderOY != null) return SenderOY.SchoolName;
                if (SenderMOYO != null) return SenderMOYO.GovernmentName;
                return string.Empty;
            }
        }

        public string SenderLastImportDate { get; set; }
        public ExportSettings ExportSettings { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SenderInfo)) return false;
            return Equals((SenderInfo) obj);
        }

        public bool Equals(SenderInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.SenderMOYO, SenderMOYO) && Equals(other.SenderOY, SenderOY);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((SenderMOYO != null ? SenderMOYO.GetHashCode() : 0)*397) ^ (SenderOY != null ? SenderOY.GetHashCode() : 0);
            }
        }

        //public static WizardSettingsManager WizardSettings
        //{
        //    get { return WizardManagers.GetManager.Resolve<WizardSettingsManager>(); }
        //}
    }
}
