using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;

namespace FCT.Client.Dto
{
    [Serializable][Description("Сведения об аккредитации общественных наблюдателей")]
    public class StationWorkersAccreditationDto : DtoCreateDateBase, IEquatable<StationWorkersAccreditationDto>, IDtoWithStationWorker
    {
        [XmlElement]
        public override int Region { get; set; }

        [Description("Работник")]
        [XmlIgnore] public string StationWorkerName { get { return StationWorkerDto.Return(x => x.FIO, "---"); } }

        [Description("МСУ места аккредитации")]
        [XmlIgnore] public string GovernmentName { get { return GovernmentDto.Return(x => x.ToString(), "---"); } }

        [CsvColumn(Name = "GUID работника ППЭ", FieldIndex = 1)]
        public Guid StationWorker { get; set; }

        [CsvColumn(Name = "GUID МСУ места аккредитации", FieldIndex = 2)]
        public Guid? Government { get; set; }

        [Description("Номер удостоверения общественного наблюдателя")]
        string _documentNumber;

        [CsvColumn(Name = "Номер удостоверения", FieldIndex = 4)]
        public string DocumentNumber
        {
            get { return _documentNumber;  }
            set 
            {
                _documentNumber = value ?? string.Empty;
                DocumentNumberAsKey = _documentNumber.Trim().Replace(" ","").ToUpper();
            }
        }

        public string DocumentNumberAsKey { get; set; }

        [Description("Место регистрации")]
        [CsvColumn(Name = "Место регистрации (если не МСУ)", FieldIndex = 3)]
        public string NotGovernmentAccreditation { get; set; }

        [CsvColumn(Name = "Признак наличия близких родственников", FieldIndex = 5)]
        public int IsFamilyInt { set; get; }

        [Description("Наличие близких родственников, сдающих ЕГЭ")]
        [XmlIgnore] public bool IsFamily { get { return IsFamilyInt == 1; } }

        [Description("Дата начала действия удостоверения")]
        [CsvColumn(Name = "Дата начала действия удостоверения (аккредитации)", FieldIndex = 6, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public DateTime DateFrom { get; set; }

        [Description("Дата окончания действия удостоверения")]
        [CsvColumn(Name = "Дата окончания действия удостоверения (аккредитации)", FieldIndex = 7, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public DateTime? DateTo { get; set; }

        [CsvColumn(Name = "Дата-время создания", FieldIndex = 8, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate { get { return base.CreateDate; } set { base.CreateDate = value; } }

        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 9, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate { get { return base.UpdateDate; } set { base.UpdateDate = value; } }

        #region NonSerializable

        [XmlIgnore] public StationWorkersDto StationWorkerDto { get; set; }
        [XmlIgnore] public GovernmentsDto GovernmentDto { get; set; }

        #endregion

        #region IEquatable<ParticipantPropertiesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(StationWorkersAccreditationDto)) return false;
            return Equals((StationWorkersAccreditationDto)obj);
        }

        public bool Equals(StationWorkersAccreditationDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.StationWorker.Equals(StationWorker) &&
                other.DocumentNumberAsKey.Equals(DocumentNumberAsKey) &&
                other.Region == Region;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 37 + StationWorker.ToString().GetHashCode();
                result = result * 37 + (!string.IsNullOrEmpty(DocumentNumberAsKey) ? DocumentNumberAsKey.GetHashCode() : 0);
                result = result * 37 + Region.GetHashCode();
                return result;
            }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as StationWorkersAccreditationDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.StationWorker.Equals(StationWorker), "Работник");
            result &= CheckChanges(StringEquals(other.DocumentNumberAsKey, DocumentNumberAsKey), "Номер удостоверения");
            result &= CheckChanges(Equals(other.Government, Government), "МСУ места аккредитации");
            result &= CheckChanges(StringEquals(other.NotGovernmentAccreditation, NotGovernmentAccreditation), "Место регистрации ");
            result &= CheckChanges(other.IsFamilyInt == IsFamilyInt, "Наличие близких родственников, сдающих ЕГЭ");
            result &= CheckChanges(other.DateFrom == DateFrom, "Дата начала действия удостоверения");
            result &= CheckChanges(other.DateTo == DateTo, "Дата окончания действия удостоверения");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;            
        }
        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
