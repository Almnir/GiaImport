using DataModels;
using linq2dbpro.DataModels;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GiaImport
{
    class BulkManager
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

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

        public ConcurrentDictionary<string, string> errorDict = new ConcurrentDictionary<string, string>();

        class Bulkload<T, Y>
        {
            private static Logger log = LogManager.GetCurrentClassLogger();

            public ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> errorDict = new ConcurrentDictionary<string, Tuple<string, long, TimeSpan>>();

            public void Load(IEnumerable<Y> obj, string tablename, Action<BulkCopyRowsCopied> handler)
            {
                BulkCopyRowsCopied bcrc = null;
                try
                {
                    {
                        using (DataConnection dc = new DataConnection(ProviderName.SqlServer, Globals.GetConnectionString()))
                        using (var db = SqlServerTools.CreateDataConnection(Globals.GetConnectionString()))
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
                            bcrc = db.DataProvider.BulkCopy(dc, bco, obj);
                            TimeSpan timeTaken = DateTime.Now - bcrc.StartTime;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string status = string.Format("При импорте таблицы произошла ошибка {0}.", ex.ToString());
                    this.errorDict.TryAdd(tablename, new Tuple<string, long, TimeSpan>(status, bcrc != null ? bcrc.RowsCopied : 0, TimeSpan.Zero));
                    log.Error(status);
                }
            }
        }

        public StringBuilder outLog;

        public void RunStoredSynchronize()
        {
            int errorCount = 0;
            this.outLog = new StringBuilder();
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
                    conn.InfoMessage += WriteProcedureLog;
                    conn.FireInfoMessageEventOnUserErrors = true;
                    conn.Open();
                    command.ExecuteNonQuery();
                    errorCount = (int)returnParameter.Value;
                    if (errorCount != 0)
                    {
                        log.Error("Ошибки слияния: " + errorCount);
                    }
                }
            }
            catch (Exception ex)
            {
                string status = string.Format("При выполнении слияния была обнаружена ошибка: {0}", ex.ToString());
                log.Error(status);
                throw new SyncException(status);
            }
        }

        private void WriteProcedureLog(object sndr, SqlInfoMessageEventArgs evt)
        {
            List<int> error_codes = new List<int>();
            if (evt.Errors.Count > 0)
            {
                foreach (SqlError err in evt.Errors)
                {
                    if (err.Number > 0)
                    {
                        //msg = string.Format("[{0} {1}]: Код [{2}] {3}\r\n",
                        //                    DateTime.Now.ToShortDateString(),
                        //                    DateTime.Now.ToLongTimeString(),
                        //                    err.Number,
                        //                    err.Message);
                        this.outLog.Append(err.Message).Append(Environment.NewLine);
                        log.Info(err.Message);
                    }
                    else
                    {
                        //msg = string.Format("[{0} {1}]: {2}\r\n",
                        //                    DateTime.Now.ToShortDateString(),
                        //                    DateTime.Now.ToShortTimeString(),
                        //                    err.Message);
                    }
                }
            }
            return;
        }

        public static DataTable GetStoredStatistics()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(Globals.GetConnectionString()))
                using (var command = new SqlCommand("loader.Statistics", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    command.CommandTimeout = 3600;
                    conn.Open();
                    dt.Load(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                string status = string.Format("При выполнении запроса статистики была обнаружена ошибка: {0}", ex.ToString());
                log.Error(status);
                throw new SyncException(status);
            }
            return dt;
        }

        public static DataTable PrepareStatistics(Dictionary<string, long> importStatictics)
        {
            DataTable statTable = GetStoredStatistics();
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add(new DataColumn(Globals.GRID_NAME, typeof(string)));
            resultTable.Columns.Add(new DataColumn(Globals.GRID_DESCRIPTION, typeof(string)));
            resultTable.Columns.Add(new DataColumn(Globals.GRID_XML, typeof(long)));
            resultTable.Columns.Add(new DataColumn(Globals.GRID_LOADER, typeof(int)));
            resultTable.Columns.Add(new DataColumn(Globals.GRID_TOTAL, typeof(int)));
            foreach (DataRow st in statTable.Rows)
            {
                string tname = st.Field<string>("TableName");
                // если есть в статистике по xml подсчёту
                if (importStatictics.ContainsKey(tname))
                {
                    DataRow row = resultTable.NewRow();
                    row[Globals.GRID_NAME] = tname;
                    row[Globals.GRID_DESCRIPTION] = st.Field<string>("TableDescription");
                    row[Globals.GRID_XML] = importStatictics[tname];
                    row[Globals.GRID_LOADER] = st.Field<int>("LoaderAmount");
                    row[Globals.GRID_TOTAL] = st.Field<int>("DboAmount");
                    resultTable.Rows.Add(row);
                }
            }
            return resultTable;
        }

        public void BulkStart(string tablename, string xmlfilename, Action<BulkCopyRowsCopied> handler, ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            switch (tablename)
            {

                case "ac_Appeals":
                    Bulkload_ac_Appeals(xmlfilename, handler, out outParam);
                    break;

                case "ac_AppealTasks":
                    Bulkload_ac_AppealTasks(xmlfilename, handler, out outParam);
                    break;

                case "ac_Changes":
                    Bulkload_ac_Changes(xmlfilename, handler, out outParam);
                    break;

                case "dats_Borders":
                    Bulkload_dats_Borders(xmlfilename, handler, out outParam);
                    break;

                case "dats_Groups":
                    Bulkload_dats_Groups(xmlfilename, handler, out outParam);
                    break;

                case "prnf_CertificatePrintMain":
                    Bulkload_prnf_CertificatePrintMain(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Address":
                    Bulkload_rbd_Address(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Areas":
                    Bulkload_rbd_Areas(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Auditoriums":
                    Bulkload_rbd_Auditoriums(xmlfilename, handler, out outParam);
                    break;

                case "rbd_AuditoriumsSubjects":
                    Bulkload_rbd_AuditoriumsSubjects(xmlfilename, handler, out outParam);
                    break;

                case "rbd_CurrentRegion":
                    Bulkload_rbd_CurrentRegion(xmlfilename, handler, out outParam);
                    break;

                case "rbd_CurrentRegionAddress":
                    Bulkload_rbd_CurrentRegionAddress(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Experts":
                    Bulkload_rbd_Experts(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ExpertsExams":
                    Bulkload_rbd_ExpertsExams(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ExpertsSubjects":
                    Bulkload_rbd_ExpertsSubjects(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Governments":
                    Bulkload_rbd_Governments(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ParticipantProperties":
                    Bulkload_rbd_ParticipantProperties(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Participants":
                    Bulkload_rbd_Participants(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ParticipantsExamPStation":
                    Bulkload_rbd_ParticipantsExamPStation(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ParticipantsExams":
                    Bulkload_rbd_ParticipantsExams(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ParticipantsExamsOnStation":
                    Bulkload_rbd_ParticipantsExamsOnStation(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ParticipantsProfSubject":
                    Bulkload_rbd_ParticipantsProfSubject(xmlfilename, handler, out outParam);
                    break;

                case "rbd_ParticipantsSubject":
                    Bulkload_rbd_ParticipantsSubject(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Places":
                    Bulkload_rbd_Places(xmlfilename, handler, out outParam);
                    break;

                case "rbd_SchoolAddress":
                    Bulkload_rbd_SchoolAddress(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Schools":
                    Bulkload_rbd_Schools(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationExamAuditory":
                    Bulkload_rbd_StationExamAuditory(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationForm":
                    Bulkload_rbd_StationForm(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationFormAct":
                    Bulkload_rbd_StationFormAct(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationFormAuditoryFields":
                    Bulkload_rbd_StationFormAuditoryFields(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationFormFields":
                    Bulkload_rbd_StationFormFields(xmlfilename, handler, out outParam);
                    break;

                case "rbd_Stations":
                    Bulkload_rbd_Stations(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationsExams":
                    Bulkload_rbd_StationsExams(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationWorkerOnExam":
                    Bulkload_rbd_StationWorkerOnExam(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationWorkerOnStation":
                    Bulkload_rbd_StationWorkerOnStation(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationWorkers":
                    Bulkload_rbd_StationWorkers(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationWorkersAccreditation":
                    Bulkload_rbd_StationWorkersAccreditation(xmlfilename, handler, out outParam);
                    break;

                case "rbd_StationWorkersSubjects":
                    Bulkload_rbd_StationWorkersSubjects(xmlfilename, handler, out outParam);
                    break;

                case "res_Answers":
                    Bulkload_res_Answers(xmlfilename, handler, out outParam);
                    break;

                case "res_Complects":
                    Bulkload_res_Complects(xmlfilename, handler, out outParam);
                    break;

                case "res_HumanTests":
                    Bulkload_res_HumanTests(xmlfilename, handler, out outParam);
                    break;

                case "res_Marks":
                    Bulkload_res_Marks(xmlfilename, handler, out outParam);
                    break;

                case "res_SubComplects":
                    Bulkload_res_SubComplects(xmlfilename, handler, out outParam);
                    break;

                case "res_Subtests":
                    Bulkload_res_Subtests(xmlfilename, handler, out outParam);
                    break;

                case "sht_Alts":
                    Bulkload_sht_Alts(xmlfilename, handler, out outParam);
                    break;

                case "sht_FinalMarks_C":
                    Bulkload_sht_FinalMarks_C(xmlfilename, handler, out outParam);
                    break;

                case "sht_FinalMarks_D":
                    Bulkload_sht_FinalMarks_D(xmlfilename, handler, out outParam);
                    break;

                case "sht_Marks_AB":
                    Bulkload_sht_Marks_AB(xmlfilename, handler, out outParam);
                    break;

                case "sht_Marks_C":
                    Bulkload_sht_Marks_C(xmlfilename, handler, out outParam);
                    break;

                case "sht_Marks_D":
                    Bulkload_sht_Marks_D(xmlfilename, handler, out outParam);
                    break;

                case "sht_Packages":
                    Bulkload_sht_Packages(xmlfilename, handler, out outParam);
                    break;

                case "sht_Sheets_AB":
                    Bulkload_sht_Sheets_AB(xmlfilename, handler, out outParam);
                    break;

                case "sht_Sheets_C":
                    Bulkload_sht_Sheets_C(xmlfilename, handler, out outParam);
                    break;

                case "sht_Sheets_D":
                    Bulkload_sht_Sheets_D(xmlfilename, handler, out outParam);
                    break;

                case "sht_Sheets_R":
                    Bulkload_sht_Sheets_R(xmlfilename, handler, out outParam);
                    break;
            }

        }



        public void Bulkload_ac_Appeals(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<ac_AppealsSet, ac_Appeal> bl = new Bulkload<ac_AppealsSet, ac_Appeal>();
            bl.Load(MainStage.DeserializeXMLFileToObject<ac_AppealsSet>(xmlfilename).Items, "ac_Appeals", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_ac_AppealTasks(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<ac_AppealTasksSet, ac_AppealTask> bl = new Bulkload<ac_AppealTasksSet, ac_AppealTask>();
            bl.Load(MainStage.DeserializeXMLFileToObject<ac_AppealTasksSet>(xmlfilename).Items, "ac_AppealTasks", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_ac_Changes(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<ac_ChangesSet, ac_Change> bl = new Bulkload<ac_ChangesSet, ac_Change>();
            bl.Load(MainStage.DeserializeXMLFileToObject<ac_ChangesSet>(xmlfilename).Items, "ac_Changes", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_dats_Borders(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<dats_BordersSet, dats_Border> bl = new Bulkload<dats_BordersSet, dats_Border>();
            bl.Load(MainStage.DeserializeXMLFileToObject<dats_BordersSet>(xmlfilename).Items, "dats_Borders", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_dats_Groups(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<dats_GroupsSet, dats_Group> bl = new Bulkload<dats_GroupsSet, dats_Group>();
            bl.Load(MainStage.DeserializeXMLFileToObject<dats_GroupsSet>(xmlfilename).Items, "dats_Groups", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_prnf_CertificatePrintMain(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<prnf_CertificatePrintMainSet, prnf_CertificatePrintMain> bl = new Bulkload<prnf_CertificatePrintMainSet, prnf_CertificatePrintMain>();
            bl.Load(MainStage.DeserializeXMLFileToObject<prnf_CertificatePrintMainSet>(xmlfilename).Items, "prnf_CertificatePrintMain", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Address(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_AddressSet, rbd_Address> bl = new Bulkload<rbd_AddressSet, rbd_Address>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_AddressSet>(xmlfilename).Items, "rbd_Address", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Areas(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_AreasSet, rbd_Area> bl = new Bulkload<rbd_AreasSet, rbd_Area>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_AreasSet>(xmlfilename).Items, "rbd_Areas", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Auditoriums(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_AuditoriumsSet, rbd_Auditorium> bl = new Bulkload<rbd_AuditoriumsSet, rbd_Auditorium>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_AuditoriumsSet>(xmlfilename).Items, "rbd_Auditoriums", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_AuditoriumsSubjects(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_AuditoriumsSubjectsSet, rbd_AuditoriumsSubject> bl = new Bulkload<rbd_AuditoriumsSubjectsSet, rbd_AuditoriumsSubject>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_AuditoriumsSubjectsSet>(xmlfilename).Items, "rbd_AuditoriumsSubjects", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_CurrentRegion(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_CurrentRegionSet, rbd_CurrentRegion> bl = new Bulkload<rbd_CurrentRegionSet, rbd_CurrentRegion>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_CurrentRegionSet>(xmlfilename).Items, "rbd_CurrentRegion", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_CurrentRegionAddress(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_CurrentRegionAddressSet, rbd_CurrentRegionAddress> bl = new Bulkload<rbd_CurrentRegionAddressSet, rbd_CurrentRegionAddress>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_CurrentRegionAddressSet>(xmlfilename).Items, "rbd_CurrentRegionAddress", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Experts(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ExpertsSet, rbd_Expert> bl = new Bulkload<rbd_ExpertsSet, rbd_Expert>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsSet>(xmlfilename).Items, "rbd_Experts", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ExpertsExams(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ExpertsExamsSet, rbd_ExpertsExam> bl = new Bulkload<rbd_ExpertsExamsSet, rbd_ExpertsExam>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsExamsSet>(xmlfilename).Items, "rbd_ExpertsExams", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ExpertsSubjects(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ExpertsSubjectsSet, rbd_ExpertsSubject> bl = new Bulkload<rbd_ExpertsSubjectsSet, rbd_ExpertsSubject>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsSubjectsSet>(xmlfilename).Items, "rbd_ExpertsSubjects", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Governments(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_GovernmentsSet, rbd_Government> bl = new Bulkload<rbd_GovernmentsSet, rbd_Government>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_GovernmentsSet>(xmlfilename).Items, "rbd_Governments", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ParticipantProperties(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ParticipantPropertiesSet, rbd_ParticipantProperty> bl = new Bulkload<rbd_ParticipantPropertiesSet, rbd_ParticipantProperty>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantPropertiesSet>(xmlfilename).Items, "rbd_ParticipantProperties", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Participants(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ParticipantsSet, rbd_Participant> bl = new Bulkload<rbd_ParticipantsSet, rbd_Participant>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsSet>(xmlfilename).Items, "rbd_Participants", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ParticipantsExamPStation(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ParticipantsExamPStationSet, rbd_ParticipantsExamPStation> bl = new Bulkload<rbd_ParticipantsExamPStationSet, rbd_ParticipantsExamPStation>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamPStationSet>(xmlfilename).Items, "rbd_ParticipantsExamPStation", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ParticipantsExams(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ParticipantsExamsSet, rbd_ParticipantsExam> bl = new Bulkload<rbd_ParticipantsExamsSet, rbd_ParticipantsExam>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamsSet>(xmlfilename).Items, "rbd_ParticipantsExams", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ParticipantsExamsOnStation(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ParticipantsExamsOnStationSet, rbd_ParticipantsExamsOnStation> bl = new Bulkload<rbd_ParticipantsExamsOnStationSet, rbd_ParticipantsExamsOnStation>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamsOnStationSet>(xmlfilename).Items, "rbd_ParticipantsExamsOnStation", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ParticipantsProfSubject(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ParticipantsProfSubjectSet, rbd_ParticipantsProfSubject> bl = new Bulkload<rbd_ParticipantsProfSubjectSet, rbd_ParticipantsProfSubject>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsProfSubjectSet>(xmlfilename).Items, "rbd_ParticipantsProfSubject", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_ParticipantsSubject(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_ParticipantsSubjectSet, rbd_ParticipantsSubject> bl = new Bulkload<rbd_ParticipantsSubjectSet, rbd_ParticipantsSubject>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsSubjectSet>(xmlfilename).Items, "rbd_ParticipantsSubject", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Places(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_PlacesSet, rbd_Place> bl = new Bulkload<rbd_PlacesSet, rbd_Place>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_PlacesSet>(xmlfilename).Items, "rbd_Places", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_SchoolAddress(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_SchoolAddressSet, rbd_SchoolAddress> bl = new Bulkload<rbd_SchoolAddressSet, rbd_SchoolAddress>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_SchoolAddressSet>(xmlfilename).Items, "rbd_SchoolAddress", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Schools(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_SchoolsSet, rbd_School> bl = new Bulkload<rbd_SchoolsSet, rbd_School>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_SchoolsSet>(xmlfilename).Items, "rbd_Schools", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationExamAuditory(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationExamAuditorySet, rbd_StationExamAuditory> bl = new Bulkload<rbd_StationExamAuditorySet, rbd_StationExamAuditory>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationExamAuditorySet>(xmlfilename).Items, "rbd_StationExamAuditory", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationForm(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationFormSet, rbd_StationForm> bl = new Bulkload<rbd_StationFormSet, rbd_StationForm>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormSet>(xmlfilename).Items, "rbd_StationForm", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationFormAct(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationFormActSet, rbd_StationFormAct> bl = new Bulkload<rbd_StationFormActSet, rbd_StationFormAct>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormActSet>(xmlfilename).Items, "rbd_StationFormAct", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationFormAuditoryFields(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationFormAuditoryFieldsSet, rbd_StationFormAuditoryField> bl = new Bulkload<rbd_StationFormAuditoryFieldsSet, rbd_StationFormAuditoryField>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormAuditoryFieldsSet>(xmlfilename).Items, "rbd_StationFormAuditoryFields", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationFormFields(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationFormFieldsSet, rbd_StationFormField> bl = new Bulkload<rbd_StationFormFieldsSet, rbd_StationFormField>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormFieldsSet>(xmlfilename).Items, "rbd_StationFormFields", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_Stations(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationsSet, rbd_Station> bl = new Bulkload<rbd_StationsSet, rbd_Station>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationsSet>(xmlfilename).Items, "rbd_Stations", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationsExams(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationsExamsSet, rbd_StationsExam> bl = new Bulkload<rbd_StationsExamsSet, rbd_StationsExam>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationsExamsSet>(xmlfilename).Items, "rbd_StationsExams", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationWorkerOnExam(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationWorkerOnExamSet, rbd_StationWorkerOnExam> bl = new Bulkload<rbd_StationWorkerOnExamSet, rbd_StationWorkerOnExam>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkerOnExamSet>(xmlfilename).Items, "rbd_StationWorkerOnExam", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationWorkerOnStation(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationWorkerOnStationSet, rbd_StationWorkerOnStation> bl = new Bulkload<rbd_StationWorkerOnStationSet, rbd_StationWorkerOnStation>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkerOnStationSet>(xmlfilename).Items, "rbd_StationWorkerOnStation", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationWorkers(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationWorkersSet, rbd_StationWorker> bl = new Bulkload<rbd_StationWorkersSet, rbd_StationWorker>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersSet>(xmlfilename).Items, "rbd_StationWorkers", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationWorkersAccreditation(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationWorkersAccreditationSet, rbd_StationWorkersAccreditation> bl = new Bulkload<rbd_StationWorkersAccreditationSet, rbd_StationWorkersAccreditation>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersAccreditationSet>(xmlfilename).Items, "rbd_StationWorkersAccreditation", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_rbd_StationWorkersSubjects(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<rbd_StationWorkersSubjectsSet, rbd_StationWorkersSubject> bl = new Bulkload<rbd_StationWorkersSubjectsSet, rbd_StationWorkersSubject>();
            bl.Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersSubjectsSet>(xmlfilename).Items, "rbd_StationWorkersSubjects", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_res_Answers(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<res_AnswersSet, res_Answer> bl = new Bulkload<res_AnswersSet, res_Answer>();
            bl.Load(MainStage.DeserializeXMLFileToObject<res_AnswersSet>(xmlfilename).Items, "res_Answers", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_res_Complects(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<res_ComplectsSet, res_Complect> bl = new Bulkload<res_ComplectsSet, res_Complect>();
            bl.Load(MainStage.DeserializeXMLFileToObject<res_ComplectsSet>(xmlfilename).Items, "res_Complects", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_res_HumanTests(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<res_HumanTestsSet, res_HumanTest> bl = new Bulkload<res_HumanTestsSet, res_HumanTest>();
            bl.Load(MainStage.DeserializeXMLFileToObject<res_HumanTestsSet>(xmlfilename).Items, "res_HumanTests", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_res_Marks(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<res_MarksSet, res_Mark> bl = new Bulkload<res_MarksSet, res_Mark>();
            bl.Load(MainStage.DeserializeXMLFileToObject<res_MarksSet>(xmlfilename).Items, "res_Marks", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_res_SubComplects(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<res_SubComplectsSet, res_SubComplect> bl = new Bulkload<res_SubComplectsSet, res_SubComplect>();
            bl.Load(MainStage.DeserializeXMLFileToObject<res_SubComplectsSet>(xmlfilename).Items, "res_SubComplects", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_res_Subtests(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<res_SubtestsSet, res_SubTest> bl = new Bulkload<res_SubtestsSet, res_SubTest>();
            bl.Load(MainStage.DeserializeXMLFileToObject<res_SubtestsSet>(xmlfilename).Items, "res_Subtests", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Alts(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_AltsSet, sht_Alt> bl = new Bulkload<sht_AltsSet, sht_Alt>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_AltsSet>(xmlfilename).Items, "sht_Alts", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_FinalMarks_C(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_FinalMarks_CSet, sht_FinalMarks_C> bl = new Bulkload<sht_FinalMarks_CSet, sht_FinalMarks_C>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_FinalMarks_CSet>(xmlfilename).Items, "sht_FinalMarks_C", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_FinalMarks_D(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_FinalMarks_DSet, sht_FinalMarks_D> bl = new Bulkload<sht_FinalMarks_DSet, sht_FinalMarks_D>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_FinalMarks_DSet>(xmlfilename).Items, "sht_FinalMarks_D", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Marks_AB(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_Marks_ABSet, sht_Marks_AB> bl = new Bulkload<sht_Marks_ABSet, sht_Marks_AB>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_Marks_ABSet>(xmlfilename).Items, "sht_Marks_AB", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Marks_C(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_Marks_CSet, sht_Marks_C> bl = new Bulkload<sht_Marks_CSet, sht_Marks_C>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_Marks_CSet>(xmlfilename).Items, "sht_Marks_C", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Marks_D(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_Marks_DSet, sht_Marks_D> bl = new Bulkload<sht_Marks_DSet, sht_Marks_D>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_Marks_DSet>(xmlfilename).Items, "sht_Marks_D", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Packages(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_PackagesSet, sht_Package> bl = new Bulkload<sht_PackagesSet, sht_Package>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_PackagesSet>(xmlfilename).Items, "sht_Packages", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Sheets_AB(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_Sheets_ABSet, sht_Sheets_AB> bl = new Bulkload<sht_Sheets_ABSet, sht_Sheets_AB>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_ABSet>(xmlfilename).Items, "sht_Sheets_AB", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Sheets_C(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_Sheets_CSet, sht_Sheets_C> bl = new Bulkload<sht_Sheets_CSet, sht_Sheets_C>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_CSet>(xmlfilename).Items, "sht_Sheets_C", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Sheets_D(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_Sheets_DSet, sht_Sheets_D> bl = new Bulkload<sht_Sheets_DSet, sht_Sheets_D>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_DSet>(xmlfilename).Items, "sht_Sheets_D", handler);
            outParam = bl.errorDict;
        }

        public void Bulkload_sht_Sheets_R(string xmlfilename, Action<BulkCopyRowsCopied> handler, out ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> outParam)
        {
            Bulkload<sht_Sheets_RSet, sht_Sheets_R> bl = new Bulkload<sht_Sheets_RSet, sht_Sheets_R>();
            bl.Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_RSet>(xmlfilename).Items, "sht_Sheets_R", handler);
            outParam = bl.errorDict;
        }
    }
}
