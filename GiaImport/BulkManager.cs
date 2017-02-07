using DataModels;
using linq2dbpro.DataModels;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GiaImport
{
    class BulkManager
    {
        public static List<string> tablesList = new List<string>()
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

        class Bulkload<T, Y>
        {

            public void Load(IEnumerable<Y> obj, string tablename, Action<BulkCopyRowsCopied> handler)
            {
                try
                {
                    using (GIA_DB db = new GIA_DB())
                    {
                        using (DataConnection dc = new DataConnection(ProviderName.SqlServer, Globals.GetConnectionString()))
                        {
                            if (!DatabaseHelper.IsDataTableExists(Globals.GetConnectionString(), "loader", tablename))
                            {
                                throw new MyBulkException(string.Format("Таблицы {0} нет в базе данных.", tablename));
                            }
                            BulkCopyOptions bco = new BulkCopyOptions();
                            bco.BulkCopyType = BulkCopyType.Default;
                            bco.MaxBatchSize = 10000;
                            bco.RowsCopiedCallback += handler;
                            bco.NotifyAfter = 1;
                            db.DataProvider.BulkCopy(dc, bco, obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new MyBulkException(string.Format("При обработке таблицы {0} произошла ошибка {1}.", tablename, ex.ToString()));
                }
            }

        }

        public static void RunStoredSynchronize()
        {
            int errorCount = 0;
            try
            {
                using (var conn = new SqlConnection(Globals.GetConnectionString()))
                using (var command = new SqlCommand("loader.Synchronize", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    command.Parameters.Add("@TableGroup", SqlDbType.SmallInt).Value = 0;
                    command.Parameters.Add("@SkipErrors", SqlDbType.Bit).Value = 0;
                    command.CommandTimeout = 3600;
                    SqlParameter returnParameter = command.Parameters.Add("@error_count", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    conn.Open();
                    command.ExecuteNonQuery();
                    errorCount = (int)returnParameter.Value;
                }
            }
            catch (Exception ex)
            {
                throw new SyncException(string.Format("При выполнении слияния было обнаружено ошибок: {0}", ex.ToString()));
            }
        }

        public static void BulkStart(string tablename, string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            switch (tablename)
            {

                case "ac_Appeals":
                    Bulkload_ac_Appeals(xmlfilename, handler);
                    break;

                case "ac_AppealTasks":
                    Bulkload_ac_AppealTasks(xmlfilename, handler);
                    break;

                case "ac_Changes":
                    Bulkload_ac_Changes(xmlfilename, handler);
                    break;

                case "dats_Borders":
                    Bulkload_dats_Borders(xmlfilename, handler);
                    break;

                case "dats_Groups":
                    Bulkload_dats_Groups(xmlfilename, handler);
                    break;

                case "prnf_CertificatePrintMain":
                    Bulkload_prnf_CertificatePrintMain(xmlfilename, handler);
                    break;

                case "rbd_Address":
                    Bulkload_rbd_Address(xmlfilename, handler);
                    break;

                case "rbd_Areas":
                    Bulkload_rbd_Areas(xmlfilename, handler);
                    break;

                case "rbd_Auditoriums":
                    Bulkload_rbd_Auditoriums(xmlfilename, handler);
                    break;

                case "rbd_AuditoriumsSubjects":
                    Bulkload_rbd_AuditoriumsSubjects(xmlfilename, handler);
                    break;

                case "rbd_CurrentRegion":
                    Bulkload_rbd_CurrentRegion(xmlfilename, handler);
                    break;

                case "rbd_CurrentRegionAddress":
                    Bulkload_rbd_CurrentRegionAddress(xmlfilename, handler);
                    break;

                case "rbd_Experts":
                    Bulkload_rbd_Experts(xmlfilename, handler);
                    break;

                case "rbd_ExpertsExams":
                    Bulkload_rbd_ExpertsExams(xmlfilename, handler);
                    break;

                case "rbd_ExpertsSubjects":
                    Bulkload_rbd_ExpertsSubjects(xmlfilename, handler);
                    break;

                case "rbd_Governments":
                    Bulkload_rbd_Governments(xmlfilename, handler);
                    break;

                case "rbd_ParticipantProperties":
                    Bulkload_rbd_ParticipantProperties(xmlfilename, handler);
                    break;

                case "rbd_Participants":
                    Bulkload_rbd_Participants(xmlfilename, handler);
                    break;

                case "rbd_ParticipantsExamPStation":
                    Bulkload_rbd_ParticipantsExamPStation(xmlfilename, handler);
                    break;

                case "rbd_ParticipantsExams":
                    Bulkload_rbd_ParticipantsExams(xmlfilename, handler);
                    break;

                case "rbd_ParticipantsExamsOnStation":
                    Bulkload_rbd_ParticipantsExamsOnStation(xmlfilename, handler);
                    break;

                case "rbd_ParticipantsProfSubject":
                    Bulkload_rbd_ParticipantsProfSubject(xmlfilename, handler);
                    break;

                case "rbd_ParticipantsSubject":
                    Bulkload_rbd_ParticipantsSubject(xmlfilename, handler);
                    break;

                case "rbd_Places":
                    Bulkload_rbd_Places(xmlfilename, handler);
                    break;

                case "rbd_SchoolAddress":
                    Bulkload_rbd_SchoolAddress(xmlfilename, handler);
                    break;

                case "rbd_Schools":
                    Bulkload_rbd_Schools(xmlfilename, handler);
                    break;

                case "rbd_StationExamAuditory":
                    Bulkload_rbd_StationExamAuditory(xmlfilename, handler);
                    break;

                case "rbd_StationForm":
                    Bulkload_rbd_StationForm(xmlfilename, handler);
                    break;

                case "rbd_StationFormAct":
                    Bulkload_rbd_StationFormAct(xmlfilename, handler);
                    break;

                case "rbd_StationFormAuditoryFields":
                    Bulkload_rbd_StationFormAuditoryFields(xmlfilename, handler);
                    break;

                case "rbd_StationFormFields":
                    Bulkload_rbd_StationFormFields(xmlfilename, handler);
                    break;

                case "rbd_Stations":
                    Bulkload_rbd_Stations(xmlfilename, handler);
                    break;

                case "rbd_StationsExams":
                    Bulkload_rbd_StationsExams(xmlfilename, handler);
                    break;

                case "rbd_StationWorkerOnExam":
                    Bulkload_rbd_StationWorkerOnExam(xmlfilename, handler);
                    break;

                case "rbd_StationWorkerOnStation":
                    Bulkload_rbd_StationWorkerOnStation(xmlfilename, handler);
                    break;

                case "rbd_StationWorkers":
                    Bulkload_rbd_StationWorkers(xmlfilename, handler);
                    break;

                case "rbd_StationWorkersAccreditation":
                    Bulkload_rbd_StationWorkersAccreditation(xmlfilename, handler);
                    break;

                case "rbd_StationWorkersSubjects":
                    Bulkload_rbd_StationWorkersSubjects(xmlfilename, handler);
                    break;

                case "res_Answers":
                    Bulkload_res_Answers(xmlfilename, handler);
                    break;

                case "res_Complects":
                    Bulkload_res_Complects(xmlfilename, handler);
                    break;

                case "res_HumanTests":
                    Bulkload_res_HumanTests(xmlfilename, handler);
                    break;

                case "res_Marks":
                    Bulkload_res_Marks(xmlfilename, handler);
                    break;

                case "res_SubComplects":
                    Bulkload_res_SubComplects(xmlfilename, handler);
                    break;

                case "res_Subtests":
                    Bulkload_res_Subtests(xmlfilename, handler);
                    break;

                case "sht_Alts":
                    Bulkload_sht_Alts(xmlfilename, handler);
                    break;

                case "sht_FinalMarks_C":
                    Bulkload_sht_FinalMarks_C(xmlfilename, handler);
                    break;

                case "sht_FinalMarks_D":
                    Bulkload_sht_FinalMarks_D(xmlfilename, handler);
                    break;

                case "sht_Marks_AB":
                    Bulkload_sht_Marks_AB(xmlfilename, handler);
                    break;

                case "sht_Marks_C":
                    Bulkload_sht_Marks_C(xmlfilename, handler);
                    break;

                case "sht_Marks_D":
                    Bulkload_sht_Marks_D(xmlfilename, handler);
                    break;

                case "sht_Packages":
                    Bulkload_sht_Packages(xmlfilename, handler);
                    break;

                case "sht_Sheets_AB":
                    Bulkload_sht_Sheets_AB(xmlfilename, handler);
                    break;

                case "sht_Sheets_C":
                    Bulkload_sht_Sheets_C(xmlfilename, handler);
                    break;

                case "sht_Sheets_D":
                    Bulkload_sht_Sheets_D(xmlfilename, handler);
                    break;

                case "sht_Sheets_R":
                    Bulkload_sht_Sheets_R(xmlfilename, handler);
                    break;
            }
        }


        public static void Bulkload_ac_Appeals(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<ac_AppealsSet, ac_Appeal>().Load(MainStage.DeserializeXMLFileToObject<ac_AppealsSet>(xmlfilename).Items, "ac_Appeals", handler);
        }

        public static void Bulkload_ac_AppealTasks(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<ac_AppealTasksSet, ac_AppealTask>().Load(MainStage.DeserializeXMLFileToObject<ac_AppealTasksSet>(xmlfilename).Items, "ac_AppealTasks", handler);
        }

        public static void Bulkload_ac_Changes(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<ac_ChangesSet, ac_Change>().Load(MainStage.DeserializeXMLFileToObject<ac_ChangesSet>(xmlfilename).Items, "ac_Changes", handler);
        }

        public static void Bulkload_dats_Borders(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<dats_BordersSet, dats_Border>().Load(MainStage.DeserializeXMLFileToObject<dats_BordersSet>(xmlfilename).Items, "dats_Borders", handler);
        }

        public static void Bulkload_dats_Groups(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<dats_GroupsSet, dats_Group>().Load(MainStage.DeserializeXMLFileToObject<dats_GroupsSet>(xmlfilename).Items, "dats_Groups", handler);
        }

        public static void Bulkload_prnf_CertificatePrintMain(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<prnf_CertificatePrintMainSet, prnf_CertificatePrintMain>().Load(MainStage.DeserializeXMLFileToObject<prnf_CertificatePrintMainSet>(xmlfilename).Items, "prnf_CertificatePrintMain", handler);
        }

        public static void Bulkload_rbd_Address(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_AddressSet, rbd_Address>().Load(MainStage.DeserializeXMLFileToObject<rbd_AddressSet>(xmlfilename).Items, "rbd_Address", handler);
        }

        public static void Bulkload_rbd_Areas(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_AreasSet, rbd_Area>().Load(MainStage.DeserializeXMLFileToObject<rbd_AreasSet>(xmlfilename).Items, "rbd_Areas", handler);
        }

        public static void Bulkload_rbd_Auditoriums(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_AuditoriumsSet, rbd_Auditorium>().Load(MainStage.DeserializeXMLFileToObject<rbd_AuditoriumsSet>(xmlfilename).Items, "rbd_Auditoriums", handler);
        }

        public static void Bulkload_rbd_AuditoriumsSubjects(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_AuditoriumsSubjectsSet, rbd_AuditoriumsSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_AuditoriumsSubjectsSet>(xmlfilename).Items, "rbd_AuditoriumsSubjects", handler);
        }

        public static void Bulkload_rbd_CurrentRegion(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_CurrentRegionSet, rbd_CurrentRegion>().Load(MainStage.DeserializeXMLFileToObject<rbd_CurrentRegionSet>(xmlfilename).Items, "rbd_CurrentRegion", handler);
        }

        public static void Bulkload_rbd_CurrentRegionAddress(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_CurrentRegionAddressSet, rbd_CurrentRegionAddress>().Load(MainStage.DeserializeXMLFileToObject<rbd_CurrentRegionAddressSet>(xmlfilename).Items, "rbd_CurrentRegionAddress", handler);
        }

        public static void Bulkload_rbd_Experts(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ExpertsSet, rbd_Expert>().Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsSet>(xmlfilename).Items, "rbd_Experts", handler);
        }

        public static void Bulkload_rbd_ExpertsExams(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ExpertsExamsSet, rbd_ExpertsExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsExamsSet>(xmlfilename).Items, "rbd_ExpertsExams", handler);
        }

        public static void Bulkload_rbd_ExpertsSubjects(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ExpertsSubjectsSet, rbd_ExpertsSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsSubjectsSet>(xmlfilename).Items, "rbd_ExpertsSubjects", handler);
        }

        public static void Bulkload_rbd_Governments(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_GovernmentsSet, rbd_Government>().Load(MainStage.DeserializeXMLFileToObject<rbd_GovernmentsSet>(xmlfilename).Items, "rbd_Governments", handler);
        }

        public static void Bulkload_rbd_ParticipantProperties(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ParticipantPropertiesSet, rbd_ParticipantProperty>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantPropertiesSet>(xmlfilename).Items, "rbd_ParticipantProperties", handler);
        }

        public static void Bulkload_rbd_Participants(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ParticipantsSet, rbd_Participant>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsSet>(xmlfilename).Items, "rbd_Participants", handler);
        }

        public static void Bulkload_rbd_ParticipantsExamPStation(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ParticipantsExamPStationSet, rbd_ParticipantsExamPStation>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamPStationSet>(xmlfilename).Items, "rbd_ParticipantsExamPStation", handler);
        }

        public static void Bulkload_rbd_ParticipantsExams(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ParticipantsExamsSet, rbd_ParticipantsExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamsSet>(xmlfilename).Items, "rbd_ParticipantsExams", handler);
        }

        public static void Bulkload_rbd_ParticipantsExamsOnStation(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ParticipantsExamsOnStationSet, rbd_ParticipantsExamsOnStation>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamsOnStationSet>(xmlfilename).Items, "rbd_ParticipantsExamsOnStation", handler);
        }

        public static void Bulkload_rbd_ParticipantsProfSubject(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ParticipantsProfSubjectSet, rbd_ParticipantsProfSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsProfSubjectSet>(xmlfilename).Items, "rbd_ParticipantsProfSubject", handler);
        }

        public static void Bulkload_rbd_ParticipantsSubject(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_ParticipantsSubjectSet, rbd_ParticipantsSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsSubjectSet>(xmlfilename).Items, "rbd_ParticipantsSubject", handler);
        }

        public static void Bulkload_rbd_Places(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_PlacesSet, rbd_Place>().Load(MainStage.DeserializeXMLFileToObject<rbd_PlacesSet>(xmlfilename).Items, "rbd_Places", handler);
        }

        public static void Bulkload_rbd_SchoolAddress(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_SchoolAddressSet, rbd_SchoolAddress>().Load(MainStage.DeserializeXMLFileToObject<rbd_SchoolAddressSet>(xmlfilename).Items, "rbd_SchoolAddress", handler);
        }

        public static void Bulkload_rbd_Schools(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_SchoolsSet, rbd_School>().Load(MainStage.DeserializeXMLFileToObject<rbd_SchoolsSet>(xmlfilename).Items, "rbd_Schools", handler);
        }

        public static void Bulkload_rbd_StationExamAuditory(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationExamAuditorySet, rbd_StationExamAuditory>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationExamAuditorySet>(xmlfilename).Items, "rbd_StationExamAuditory", handler);
        }

        public static void Bulkload_rbd_StationForm(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationFormSet, rbd_StationForm>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormSet>(xmlfilename).Items, "rbd_StationForm", handler);
        }

        public static void Bulkload_rbd_StationFormAct(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationFormActSet, rbd_StationFormAct>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormActSet>(xmlfilename).Items, "rbd_StationFormAct", handler);
        }

        public static void Bulkload_rbd_StationFormAuditoryFields(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationFormAuditoryFieldsSet, rbd_StationFormAuditoryField>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormAuditoryFieldsSet>(xmlfilename).Items, "rbd_StationFormAuditoryFields", handler);
        }

        public static void Bulkload_rbd_StationFormFields(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationFormFieldsSet, rbd_StationFormField>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormFieldsSet>(xmlfilename).Items, "rbd_StationFormFields", handler);
        }

        public static void Bulkload_rbd_Stations(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationsSet, rbd_Station>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationsSet>(xmlfilename).Items, "rbd_Stations", handler);
        }

        public static void Bulkload_rbd_StationsExams(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationsExamsSet, rbd_StationsExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationsExamsSet>(xmlfilename).Items, "rbd_StationsExams", handler);
        }

        public static void Bulkload_rbd_StationWorkerOnExam(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationWorkerOnExamSet, rbd_StationWorkerOnExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkerOnExamSet>(xmlfilename).Items, "rbd_StationWorkerOnExam", handler);
        }

        public static void Bulkload_rbd_StationWorkerOnStation(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationWorkerOnStationSet, rbd_StationWorkerOnStation>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkerOnStationSet>(xmlfilename).Items, "rbd_StationWorkerOnStation", handler);
        }

        public static void Bulkload_rbd_StationWorkers(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationWorkersSet, rbd_StationWorker>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersSet>(xmlfilename).Items, "rbd_StationWorkers", handler);
        }

        public static void Bulkload_rbd_StationWorkersAccreditation(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationWorkersAccreditationSet, rbd_StationWorkersAccreditation>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersAccreditationSet>(xmlfilename).Items, "rbd_StationWorkersAccreditation", handler);
        }

        public static void Bulkload_rbd_StationWorkersSubjects(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<rbd_StationWorkersSubjectsSet, rbd_StationWorkersSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersSubjectsSet>(xmlfilename).Items, "rbd_StationWorkersSubjects", handler);
        }

        public static void Bulkload_res_Answers(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<res_AnswersSet, res_Answer>().Load(MainStage.DeserializeXMLFileToObject<res_AnswersSet>(xmlfilename).Items, "res_Answers", handler);
        }

        public static void Bulkload_res_Complects(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<res_ComplectsSet, res_Complect>().Load(MainStage.DeserializeXMLFileToObject<res_ComplectsSet>(xmlfilename).Items, "res_Complects", handler);
        }

        public static void Bulkload_res_HumanTests(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<res_HumanTestsSet, res_HumanTest>().Load(MainStage.DeserializeXMLFileToObject<res_HumanTestsSet>(xmlfilename).Items, "res_HumanTests", handler);
        }

        public static void Bulkload_res_Marks(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<res_MarksSet, res_Mark>().Load(MainStage.DeserializeXMLFileToObject<res_MarksSet>(xmlfilename).Items, "res_Marks", handler);
        }

        public static void Bulkload_res_SubComplects(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<res_SubComplectsSet, res_SubComplect>().Load(MainStage.DeserializeXMLFileToObject<res_SubComplectsSet>(xmlfilename).Items, "res_SubComplects", handler);
        }

        public static void Bulkload_res_Subtests(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<res_SubtestsSet, res_SubTest>().Load(MainStage.DeserializeXMLFileToObject<res_SubtestsSet>(xmlfilename).Items, "res_Subtests", handler);
        }

        public static void Bulkload_sht_Alts(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_AltsSet, sht_Alt>().Load(MainStage.DeserializeXMLFileToObject<sht_AltsSet>(xmlfilename).Items, "sht_Alts", handler);
        }

        public static void Bulkload_sht_FinalMarks_C(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_FinalMarks_CSet, sht_FinalMarks_C>().Load(MainStage.DeserializeXMLFileToObject<sht_FinalMarks_CSet>(xmlfilename).Items, "sht_FinalMarks_C", handler);
        }

        public static void Bulkload_sht_FinalMarks_D(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_FinalMarks_DSet, sht_FinalMarks_D>().Load(MainStage.DeserializeXMLFileToObject<sht_FinalMarks_DSet>(xmlfilename).Items, "sht_FinalMarks_D", handler);
        }

        public static void Bulkload_sht_Marks_AB(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_Marks_ABSet, sht_Marks_AB>().Load(MainStage.DeserializeXMLFileToObject<sht_Marks_ABSet>(xmlfilename).Items, "sht_Marks_AB", handler);
        }

        public static void Bulkload_sht_Marks_C(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_Marks_CSet, sht_Marks_C>().Load(MainStage.DeserializeXMLFileToObject<sht_Marks_CSet>(xmlfilename).Items, "sht_Marks_C", handler);
        }

        public static void Bulkload_sht_Marks_D(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_Marks_DSet, sht_Marks_D>().Load(MainStage.DeserializeXMLFileToObject<sht_Marks_DSet>(xmlfilename).Items, "sht_Marks_D", handler);
        }

        public static void Bulkload_sht_Packages(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_PackagesSet, sht_Package>().Load(MainStage.DeserializeXMLFileToObject<sht_PackagesSet>(xmlfilename).Items, "sht_Packages", handler);
        }

        public static void Bulkload_sht_Sheets_AB(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_Sheets_ABSet, sht_Sheets_AB>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_ABSet>(xmlfilename).Items, "sht_Sheets_AB", handler);
        }

        public static void Bulkload_sht_Sheets_C(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_Sheets_CSet, sht_Sheets_C>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_CSet>(xmlfilename).Items, "sht_Sheets_C", handler);
        }

        public static void Bulkload_sht_Sheets_D(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_Sheets_DSet, sht_Sheets_D>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_DSet>(xmlfilename).Items, "sht_Sheets_D", handler);
        }

        public static void Bulkload_sht_Sheets_R(string xmlfilename, Action<BulkCopyRowsCopied> handler)
        {
            new Bulkload<sht_Sheets_RSet, sht_Sheets_R>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_RSet>(xmlfilename).Items, "sht_Sheets_R", handler);
        }
    }
}
