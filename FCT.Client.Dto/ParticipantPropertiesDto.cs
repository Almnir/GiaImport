using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
    [Serializable][Description("Параметры участника")]
    [BulkTable("rbd_ParticipantProperties", "ParticipantProperties", RootTagName = "ArrayOfParticipantPropertiesDto")]
    public class ParticipantPropertiesDto : RegionDtoBase, IEquatable<ParticipantPropertiesDto>, IDtoCollectorAccepter, IUidableDto
    {
        [BulkColumn("PropertyId")]
        [CsvColumn(Name = "GUID записи", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, ParticipantUID); } }

        [BulkColumn("ParticipantId")]
        [CsvColumn(Name = "GUID участника", FieldIndex = 2)]
        public Guid Participant { get; set; }

        [CsvColumn(Name = "Свойство", FieldIndex = 3)]
        public ParticipantPropopertyType Property { get; set; }
        
#warning добавлено чтобы не менять бд
        public Guid ParticipantID { get { return ParticipantDto.Return(x => x.DtoID, Participant); } set { } }

        [Description("Параметр")]
        [XmlIgnore] public string PropertyName { get { return Property.GetDescription(); } }

        [BulkColumn("PValue")]
        [Description("Значение параметра")]
        [CsvColumn(Name = "Значение свойства", FieldIndex = 4)]
        public string PValue { get; set; }

#if GiaDataCollect
        [XmlElement("Region")]
#else
        [XmlElement("REGION")]
#endif
        [BulkColumn("Region")]
        [CsvColumn(Name = "Регион", FieldIndex = 5)]
        public override int Region { get; set; }

        #region NonSerializable

        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }

        [BulkColumn("Property")]
        [XmlIgnore] public int PropertyProperty
        {
            get { return (int)Property; }
            set { Property = (ParticipantPropopertyType) value; }
        }

        #endregion

        #region IEquatable<ParticipantPropertiesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ParticipantPropertiesDto)) return false;
            return Equals((ParticipantPropertiesDto) obj);
        }

        public bool Equals(ParticipantPropertiesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Participant.Equals(Participant) && 
                other.Property == Property &&
                other.Region == Region;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Participant.ToString().GetHashCode();
                result = result*37 + Property.GetHashCode();
                result = result * 37 + Region.GetHashCode();
                return result;
            }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as ParticipantPropertiesDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Participant.Equals(Participant), "Участник");
            result &= CheckChanges(Equals(other.Property, Property), "Параметр");
            result &= CheckChanges(StringEquals(other.PValue, PValue), "Значение параметра");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;            
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID
        {
            get { return string.Format("({0}, {1})", ParticipantUID, PropertyProperty); }
            set { }
        }

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
