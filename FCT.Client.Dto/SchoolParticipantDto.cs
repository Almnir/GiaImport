using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Доп. место регистрации участника")]
    public class SchoolParticipantDto : RegionDtoBase, IEquatable<SchoolParticipantDto>, IDtoWithSchool
    {
        [XmlElement]
        public override int Region { get; set; }

        [Description("Участник")]
        [XmlIgnore] public string ParticipantName { get { return ParticipantDto.Return(x => x.FIO, "---"); } }

        [Description("Школа")]
        [XmlIgnore] public string SchoolName { get { return SchoolDto.Return(x => x.ToString(), "---"); } }

        #region NonSerializable
        
        [XmlIgnore] public SchoolsDto SchoolDto { get; set; }
        [XmlIgnore] public ParticipantsDto ParticipantDto { get; set; }
        
        #endregion

        [CsvColumn(Name = "Guid", FieldIndex = 1)]
		public override Guid DtoID { get; set; }

		[CsvColumn(Name = "Guid ОО, в котором участник зарегистрирован", FieldIndex = 3)]
		public Guid School { get; set; }
		
        [CsvColumn(Name = "Guid участника", FieldIndex = 2)]
		public Guid Participant { get; set; }

        private DateTime _registrationDate;
        
        [CsvColumn(Name = "Дата-время регистрации", FieldIndex = 4, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        [Description("Дата регистрации")]
		public DateTime RegistrationDate
        {
            get { return _registrationDate; }
            set { _registrationDate = DateTime.SpecifyKind(value.Date, DateTimeKind.Unspecified); }
        }

        #region IEquatable<SchoolParticipantDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SchoolParticipantDto)) return false;
            return Equals((SchoolParticipantDto) obj);
        }

        public bool Equals(SchoolParticipantDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region && 
                other.School.Equals(School) && 
                Participant.Equals(other.Participant);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + Participant.ToString().GetHashCode();
                result = result*37 + School.ToString().GetHashCode();
                result = result*37 + Region.GetHashCode();
                return result;
            }
        }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}