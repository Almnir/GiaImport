using System.Collections.Generic;
using System.IO;

namespace GiaImport
{
    public static class Globals
    {
        public static string ROOT_ELEMENT = "ns1:GIADBSet";

        public static string TEMP_DIR = Directory.GetCurrentDirectory() + @"\Tempdir\";

        public static FormSettings frmSettings = new FormSettings();

        public static string GetConnectionString()
        {
            return string.Format("Server={0};Database={1};User Id={2};Password={3};", frmSettings.ServerText, frmSettings.DatabaseText, frmSettings.LoginText, frmSettings.PasswordText);
        }

        public static List<string> TABLES_NAMES = new List<string>()
        {
            "ac_Appeals",
            "ac_AppealTasks",
            "ac_Changes",
            "dats_Borders",
            "dats_Groups",
            "prnf_CertificatePrintMain",
            "rbd_Address",
            "rbd_Areas",
            "rbd_Auditoriums",
            "rbd_AuditoriumsSubjects",
            "rbd_CurrentRegion",
            "rbd_CurrentRegionAddress",
            "rbd_Experts",
            "rbd_ExpertsExams",
            "rbd_ExpertsSubjects",
            "rbd_Governments",
            "rbd_ParticipantProperties",
            "rbd_Participants",
            "rbd_ParticipantsExamPStation",
            "rbd_ParticipantsExams",
            "rbd_ParticipantsExamsOnStation",
            "rbd_ParticipantsProfSubject",
            "rbd_ParticipantsSubject",
            "rbd_Places",
            "rbd_SchoolAddress",
            "rbd_Schools",
            "rbd_StationExamAuditory",
            "rbd_StationForm",
            "rbd_StationFormAct",
            "rbd_StationFormAuditoryFields",
            "rbd_StationFormFields",
            "rbd_Stations",
            "rbd_StationsExams",
            "rbd_StationWorkerOnExam",
            "rbd_StationWorkerOnStation",
            "rbd_StationWorkers",
            "rbd_StationWorkersAccreditation",
            "rbd_StationWorkersSubjects",
            "res_Answers",
            "res_Complects",
            "res_HumanTests",
            "res_Marks",
            "res_SubComplects",
            "res_Subtests",
            "sht_Alts",
            "sht_FinalMarks_C",
            "sht_FinalMarks_D",
            "sht_Marks_AB",
            "sht_Marks_C",
            "sht_Marks_D",
            "sht_Packages",
            "sht_Sheets_AB",
            "sht_Sheets_C",
            "sht_Sheets_D",
            "sht_Sheets_R"
        };
    }
}
