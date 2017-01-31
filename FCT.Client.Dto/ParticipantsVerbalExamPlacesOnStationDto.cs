using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto
{
    [BulkTable( ExportExclude = true)]
    public class ParticipantsVerbalExamPlacesOnStationDto : ParticipantsExamPlacesOnStationDto
    {
        [Description("Очередь")]
        public int Queue { get; set; }

        public override int CompareTo(object obj)
        {
            var other = obj as ParticipantsVerbalExamPlacesOnStationDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.Station.Equals(Station), "ППЭ");
            result &= CheckChanges(other.Exam == Exam, "Экзамен");
            result &= CheckChanges(other.Participant.Equals(Participant), "Участник");
            result &= CheckChanges(other.AuditoriumCode.Equals(AuditoriumCode), "Аудитория");
            result &= CheckChanges(other.Row == Row, "Ряд");
            result &= CheckChanges(other.Col == Col, "Место");
            result &= CheckChanges(other.Queue == Queue, "Номер в очереди");
            result &= CheckChanges(other.IsManual.Equals(IsManual), "Ручная рассадка");
            result &= CheckChanges(other.RegistrationCode == RegistrationCode, "Код рассадки");
            return result ? 0 : 1;
        }

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ParticipantsVerbalExamPlacesOnStationDto)) return false;
            return Equals((ParticipantsVerbalExamPlacesOnStationDto)obj);
        }

        public bool Equals(ParticipantsVerbalExamPlacesOnStationDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.Region == Region &&
                other.Participant.Equals(Participant) &&
                other.Exam == Exam;
        }

        public override StationExamAuditoryDto GetDirtyStationExamAuditory(Func<int,bool> isExamVoice)
        {
            if (string.IsNullOrEmpty(AuditoriumCode)) return null;
            var res = new StationExamAuditoryDto
            {
                AuditoriumCode = AuditoriumCode,
                Station = Station,
                Exam = Exam,
                Region = Region
            };
            res.IsPreparation = false;

            return res;
        }
        
        public StationExamAuditoryDto GetDirtyStationExamAuditory()
        {
            if (string.IsNullOrEmpty(AuditoriumCode)) return null;
            var res = new StationExamAuditoryDto
            {
                AuditoriumCode = AuditoriumCode,
                Station = Station,
                Exam = Exam,
                Region = Region
            };
            res.IsPreparation = false;

            return res;
        }
    }
}
