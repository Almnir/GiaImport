using System;
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
    [Serializable][Description("Место")]
    [BulkTable("rbd_Places", "Places", RootTagName = "ArrayOfPlacesDto")]
    public class PlacesDto : RegionDtoBase, IEquatable<PlacesDto>, IDtoWithAuditorium, IUidableDto, 
        IDtoCollectorAccepter
    {
        [BulkColumn("REGION")]
        [XmlElement]
        public override int Region { get; set; }

        [XmlIgnore]private AuditoriumSurrogateKey _auditoriumSurrogateKey;
        [XmlIgnore]public AuditoriumSurrogateKey AuditoriumSurrogateKey
        {
            get { return _auditoriumSurrogateKey ?? (_auditoriumSurrogateKey = new AuditoriumSurrogateKey(Station, AuditoriumCode)); }
        }
        /* поиск аудитории по коду + ппэ */
        [XmlIgnore]
        private string _auditoriumCode;
        
        [CsvColumn(Name = "Код аудитории", FieldIndex = 2)]
        public string AuditoriumCode
        {
            get { return _auditoriumCode; }
            set { _auditoriumCode = value.ToAuditoriumCodeFormat(); }
        }

        [CsvColumn(Name = "ППЭ", FieldIndex = 1)]
        public Guid Station { get; set; }

        [Description("Аудитория")]
        [XmlIgnore]public string AuditoriumName { get { return AuditoriumDto.Return(x => x.ToString(), AuditoriumUID); } }

        [BulkColumn("Row")]
		[CsvColumn(Name = "Номер ряда", FieldIndex = 3)]
        [Description("Ряд")]
		public int Row { get; set; }

        [BulkColumn("Col")]
		[CsvColumn(Name = "Порядковый номер посадочного места в ряду", FieldIndex = 4)]
        [Description("Место")]
		public int Col { get; set; }

        [BulkColumn("IsBad")]
		[XmlIgnore]
        [Description("Исключено из структуры")]
        public bool IsBad { get; set; }

        [BulkColumn("PlacesID")]
		public override Guid DtoID { get; set; }

        [XmlElement("IsBad")]
        [CsvColumn(Name = "Признак исключения из рассадки", FieldIndex = 5)]
        public string IsBadSerialize
        {
            get { return IsBad ? "1" : "0"; }
            set { IsBad = XmlConvert.ToBoolean(value); }
        }

        [BulkColumn("PlaceType")]
        [XmlIgnore]
        public PlaceType PlaceType { get; set; }
        [XmlElement("PlaceType")]
        [CsvColumn(Name = "Тип места", FieldIndex = 6)]
        public int PlaceTypeSerialize
        {
            get { return (int)PlaceType; } 
            set { PlaceType = (PlaceType)value; }
        }

        #region NonSerializable

        [XmlIgnore] public AuditoriumsDto AuditoriumDto { get; set; }
        [XmlIgnore] public bool IsAuditoriumBroken { get; set; }

        [XmlIgnore] public int IsBadProperty { set { IsBad = Convert.ToBoolean(value); } }

        [BulkColumn("AuditoriumID")]
        [XmlElement("Auditorium")]
        public Guid AuditoriumId { get { return AuditoriumDto != null ? AuditoriumDto.DtoID : Guid.Empty; } set { } }

        [XmlIgnore]
        public AuditoriumsDto Dirty_Auditorium
        {
            get { return new AuditoriumsDto { Station = Station, AuditoriumCode = AuditoriumCode }; }
        }

        [XmlIgnore][Description("Тип места")]
        public string PlaceTypeDescription { get { return PlaceType.GetDescription(); } }
        
        #endregion

        #region IEquatable<PlacesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PlacesDto)) return false;
            return Equals((PlacesDto) obj);
        }

        public bool Equals(PlacesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region &&
                other.AuditoriumSurrogateKey.Equals(AuditoriumSurrogateKey) && 
                other.Row == Row && 
                other.Col == Col;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Region.GetHashCode();
                result = result * 37 + AuditoriumSurrogateKey.GetHashCode();
                result = result*37 + Row.GetHashCode();
                result = result*37 + Col.GetHashCode();
                return result;
            }
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as PlacesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.IsDeleted == IsDeleted, TypeExtensions.Description<PlacesDto>(c => c.IsDeleted));
            result &= CheckChanges(other.AuditoriumCode.Equals(AuditoriumCode), TypeExtensions.Description<PlacesDto>(c => c.AuditoriumName));
            result &= CheckChanges(other.Row == Row, TypeExtensions.Description<PlacesDto>(c => c.Row));
            result &= CheckChanges(other.Col == Col, TypeExtensions.Description<PlacesDto>(c => c.Col));
            result &= CheckChanges(other.IsBad == IsBad, TypeExtensions.Description<PlacesDto>(c => c.IsBad));
            result &= CheckChanges(other.PlaceType == PlaceType, TypeExtensions.Description<PlacesDto>(c => c.PlaceTypeDescription));

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
        public string AuditoriumUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationUID { get; set; }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
