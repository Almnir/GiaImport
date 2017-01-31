using System;

namespace RBD.Common.Plugin.Appilations
{
    public class PersonalData
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string DocumentSeries { get; set; }
        public string DocumentNumber { get; set; }

        public Guid HumanTest { get; set; }
        public int SubjectCode { get; set; }
        public string ExamDate { get; set; }

        public int? AppealCondition { get; set; }
        public string AppealConditionName { get; set; }
          
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Surname, Name, SecondName);
        }

        public string DocToString()
        {
            return string.Format("{0} {1}", DocumentSeries, DocumentNumber);
        }
    }
}