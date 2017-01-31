using System;
using System.Collections.Generic;

namespace RBD.Client.Services.Export
{
    [Serializable]
    public class ExportSettings
    {
        public ExportSettings()
        {
            ExportedExamsIds = new HashSet<int>();
            ExportedExamsDates = new HashSet<string>();

            ExportParticipants =
                ExportPpe =
                ExportExperts =
                ExportWorkers =
                ExportPlanning =
                ExportAuditoriumsPlanning =
                ExportParticipantsPlanning =
                ExportPpeWorkersPlanning = true;
        }

        public HashSet<int> ExportedExamsIds { get; set; }
        public HashSet<string> ExportedExamsDates { get; set; }

        public bool BlockTypeSelections { get; set; }
        public bool ExportParticipants { get; set; }
        public bool ExportPpe { get; set; }
        public bool ExportExperts { get; set; }
        public bool ExportWorkers { get; set; }
        public bool ExportPlanning { get; set; }
        public bool ExportAuditoriumsPlanning { get; set; }
        public bool ExportParticipantsPlanning { get; set; }
        public bool ExportPpeWorkersPlanning { get; set; }
        public bool CorrectedExport { get; set; }
        public bool SectionByArea { get; set; }
        public bool GroupByArea { get; set; }
    }
}
