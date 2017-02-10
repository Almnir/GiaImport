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
        public static Dictionary<string, string> TABLES_INFO = new Dictionary<string, string>()
        {
            { "ac_Appeals", "Информация об апелляциях" },
            { "ac_AppealTasks", "Информация об измененных заданиях по апелляции" },
            { "ac_Changes", "Данные об изменениях баллов в апелляции" },
            { "dats_Borders", "шкалы ГИА-9"},
            { "dats_Groups", "группы критериев функционала шкалирования" },
            { "prnf_CertificatePrintMain", "Данные о справках ГИА-9" },
            { "rbd_Address", "Адреса объектов" },
            { "rbd_Areas", "Список АТЕ" },
            { "rbd_Auditoriums", "Справочник аудиторий ППЭ" },
            { "rbd_AuditoriumsSubjects", "Предметная специализация аудиторий" },
            { "rbd_CurrentRegion", "Информация о субъекте РФ" },
            { "rbd_CurrentRegionAddress", "Адреса РЦОИ" },
            { "rbd_Experts", "Данные об экспертах" },
            { "rbd_ExpertsExams", "Распределение экспертов по экзаменам" },
            { "rbd_ExpertsSubjects", "Предметная специализация экспертов" },
            { "rbd_Governments", "Справочник ОИВ субъекта РФ" },
            { "rbd_ParticipantProperties", "Данные о параметрах участников" },
            { "rbd_Participants", "Список участников ГИА" },
            { "rbd_ParticipantsExamPStation", "Данные об автоматизированном распределении участников по  аудиториям ППЭ" },
            { "rbd_ParticipantsExams", "Данные о выборе экзаменов участниками" },
            { "rbd_ParticipantsExamsOnStation", "Данные о распределении участников по ППЭ" },
            { "rbd_ParticipantsProfSubject", "Профильные предметы участников" },
            { "rbd_ParticipantsSubject", "Предметы по сокращенной программе" },
            { "rbd_Places", "Справочник мест в аудитории" },
            { "rbd_SchoolAddress", "Адреса школ" },
            { "rbd_Schools", "Список школ" },
            { "rbd_StationExamAuditory", "Данные о назначении аудиторий на экзамен" },
            { "rbd_StationForm", "Данные по форме 13-02 МАШ" },
            { "rbd_StationFormAct", "Данные по форме 18-МАШ" },
            { "rbd_StationFormAuditoryFields", "Данные по форме 13-02 МАШ" },
            { "rbd_StationFormFields", "Данные по форме 13-02 МАШ" },
            { "rbd_Stations", "Справочник ППЭ" },
            { "rbd_StationsExams", "Распределение ППЭ по экзаменам" },
            { "rbd_StationWorkerOnExam", "Распределение работника на экзамен в ППЭ" },
            { "rbd_StationWorkerOnStation", "Прикрепление работника к ППЭ" },
            { "rbd_StationWorkers", "Данные о работниках ППЭ" },
            { "rbd_StationWorkersAccreditation", "данные об аккредитации" },
            { "rbd_StationWorkersSubjects", "Предметная специализация организаторов ГИА" },
            { "res_Answers", "Ответы участника на задания КИМ" },
            { "res_Complects", "Связки бланков по штрих - кодам в комплект" },
            { "res_HumanTests", "Человеко-тесты" },
            { "res_Marks", "Оценки" },
            { "res_SubComplects", "Данные о комплектах устной части иностранных языков" },
            { "res_Subtests", "Данные о человеко-тестах устной части и детальной математики" },
            { "sht_Alts", "Протоколы у экспертов" },
            { "sht_FinalMarks_C", "Окончательные оценки по письменной части тестирования" },
            { "sht_FinalMarks_D", "Данные об окончательных оценках по устной части тести-рования" },
            { "sht_Marks_AB", "Ответы на бланках №1" },
            { "sht_Marks_C", "Оценки, выставленные экспертами, по письменной части тестирования" },
            { "sht_Marks_D", "Данные об оценках, выставленных экспертами, по устной части тестирования" },
            { "sht_Packages", "Пакеты с обработанными бланками всех типов" },
            { "sht_Sheets_AB", "Данные с бланков ответов №1 – регистрационная часть" },
            { "sht_Sheets_C", "Бланки по письменной части тестирования" },
            { "sht_Sheets_D", "Данные о бланках по устной части тестирования" },
            { "sht_Sheets_R", "Данные с бланков ответов №1 – персональные данные" }
        };

    }
}
