using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LINQtoCSV;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Журнал отчетов")]
    [BulkTable( "rbd_ReportJournal", "ReportJournal", RootTagName = "ArrayOfParticipantsDto", ExportExclude = true)]
    public class ReportJournalDto : DtoCreateDateBase, IEquatable<ReportJournalDto> 
    {
        [CsvColumn(Name = "Guid", FieldIndex = 1)]
        public Guid DtoID { get; set; }

        [CsvColumn(Name = "Код дня экзамена", FieldIndex = 2)]
        public int ExamGlobalID { get; set; }

        [CsvColumn(Name = "Код формы", FieldIndex = 3)]
        public string RandomCode { get; set; }

        [CsvColumn(Name = "GUID работника ППЭ", FieldIndex = 4)]
        public Guid Worker { get; set; }

        [CsvColumn(Name = "ФИО работника ППЭ", FieldIndex = 5)]
        public string WorkerName { get; set; }

        [CsvColumn(Name = "Регистрационный код рассадки", FieldIndex = 6)]
        public int RegistrationCode { get; set; }

        [CsvColumn(Name = "GUID ППЭ", FieldIndex = 7)]
        public Guid Station { get; set; }
        
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 8, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        public int SubjectCode { get; set; }
        public int TestTypeCode { get; set; }
        public string ExamDate { get; set; }
        public int ReportCode
        {
            get { return 1; }
        }

        /// <summary>
        /// В csv нет этого поля, нужно только чтобы наследовать DtoCreateDateBase
        /// </summary>
        public override int Region { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ReportJournalDto)) return false;
            return Equals((ReportJournalDto)obj);
        }

        public bool Equals(ReportJournalDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                other.DtoID == DtoID;
        }

        public override int GetHashCode()
        {
            unchecked { return DtoID.ToString().GetHashCode(); }
        }

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
