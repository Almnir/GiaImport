using DataModels;
using linq2dbpro.DataModels;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GiaImport
{
    class BulkManager
    {

        class Bulkload<T, Y>
        {

            public void Load(IEnumerable<Y> obj, string tablename)
            {
                try
                {
                    using (GIA_DB db = new GIA_DB())
                    {
                        using (DataConnection dc = new DataConnection(ProviderName.SqlServer, System.Configuration.ConfigurationManager.ConnectionStrings["gia"].ConnectionString))
                        {

                            var sp = db.DataProvider.GetSchemaProvider();
                            var dbSchema = sp.GetSchema(db);
                            if (!dbSchema.Tables.Any(t => t.TableName == tablename))
                            {
                                db.CreateTable<T>();
                            }
                            BulkCopyOptions bco = new BulkCopyOptions();
                            bco.BulkCopyType = BulkCopyType.Default;
                            bco.MaxBatchSize = 10000;
                            bco.RowsCopiedCallback += Handler;
                            bco.NotifyAfter = 1;
                            db.DataProvider.BulkCopy(dc, bco, obj);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new MyBulkException(ex.ToString());
                }
            }

        }


        public static void Bulkload_ac_Appeals(string xmlfilename)
        {
            new Bulkload<ac_AppealsSet, ac_Appeal>().Load(MainStage.DeserializeXMLFileToObject<ac_AppealsSet>(xmlfilename).Items, "ac_Appeals");
        }

        public static void Bulkload_ac_AppealTasks(string xmlfilename)
        {
            new Bulkload<ac_AppealTasksSet, ac_AppealTask>().Load(MainStage.DeserializeXMLFileToObject<ac_AppealTasksSet>(xmlfilename).Items, "ac_AppealTasks");
        }

        public static void Bulkload_ac_Changes(string xmlfilename)
        {
            new Bulkload<ac_ChangesSet, ac_Change>().Load(MainStage.DeserializeXMLFileToObject<ac_ChangesSet>(xmlfilename).Items, "ac_Changes");
        }

        public static void Bulkload_dats_Borders(string xmlfilename)
        {
            new Bulkload<dats_BordersSet, dats_Border>().Load(MainStage.DeserializeXMLFileToObject<dats_BordersSet>(xmlfilename).Items, "dats_Borders");
        }

        public static void Bulkload_dats_Groups(string xmlfilename)
        {
            new Bulkload<dats_GroupsSet, dats_Group>().Load(MainStage.DeserializeXMLFileToObject<dats_GroupsSet>(xmlfilename).Items, "dats_Groups");
        }

        public static void Bulkload_prnf_CertificatePrintMain(string xmlfilename)
        {
            new Bulkload<prnf_CertificatePrintMainSet, prnf_CertificatePrintMain>().Load(MainStage.DeserializeXMLFileToObject<prnf_CertificatePrintMainSet>(xmlfilename).Items, "prnf_CertificatePrintMain");
        }

        public static void Bulkload_rbd_Address(string xmlfilename)
        {
            new Bulkload<rbd_AddressSet, rbd_Address>().Load(MainStage.DeserializeXMLFileToObject<rbd_AddressSet>(xmlfilename).Items, "rbd_Address");
        }

        public static void Bulkload_rbd_Areas(string xmlfilename)
        {
            new Bulkload<rbd_AreasSet, rbd_Area>().Load(MainStage.DeserializeXMLFileToObject<rbd_AreasSet>(xmlfilename).Items, "rbd_Areas");
        }

        public static void Bulkload_rbd_Auditoriums(string xmlfilename)
        {
            new Bulkload<rbd_AuditoriumsSet, rbd_Auditorium>().Load(MainStage.DeserializeXMLFileToObject<rbd_AuditoriumsSet>(xmlfilename).Items, "rbd_Auditoriums");
        }

        public static void Bulkload_rbd_AuditoriumsSubjects(string xmlfilename)
        {
            new Bulkload<rbd_AuditoriumsSubjectsSet, rbd_AuditoriumsSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_AuditoriumsSubjectsSet>(xmlfilename).Items, "rbd_AuditoriumsSubjects");
        }

        public static void Bulkload_rbd_CurrentRegion(string xmlfilename)
        {
            new Bulkload<rbd_CurrentRegionSet, rbd_CurrentRegion>().Load(MainStage.DeserializeXMLFileToObject<rbd_CurrentRegionSet>(xmlfilename).Items, "rbd_CurrentRegion");
        }

        public static void Bulkload_rbd_CurrentRegionAddress(string xmlfilename)
        {
            new Bulkload<rbd_CurrentRegionAddressSet, rbd_CurrentRegionAddress>().Load(MainStage.DeserializeXMLFileToObject<rbd_CurrentRegionAddressSet>(xmlfilename).Items, "rbd_CurrentRegionAddress");
        }

        public static void Bulkload_rbd_Experts(string xmlfilename)
        {
            new Bulkload<rbd_ExpertsSet, rbd_Expert>().Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsSet>(xmlfilename).Items, "rbd_Experts");
        }

        public static void Bulkload_rbd_ExpertsExams(string xmlfilename)
        {
            new Bulkload<rbd_ExpertsExamsSet, rbd_ExpertsExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsExamsSet>(xmlfilename).Items, "rbd_ExpertsExams");
        }

        public static void Bulkload_rbd_ExpertsSubjects(string xmlfilename)
        {
            new Bulkload<rbd_ExpertsSubjectsSet, rbd_ExpertsSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_ExpertsSubjectsSet>(xmlfilename).Items, "rbd_ExpertsSubjects");
        }

        public static void Bulkload_rbd_Governments(string xmlfilename)
        {
            new Bulkload<rbd_GovernmentsSet, rbd_Government>().Load(MainStage.DeserializeXMLFileToObject<rbd_GovernmentsSet>(xmlfilename).Items, "rbd_Governments");
        }

        public static void Bulkload_rbd_ParticipantProperties(string xmlfilename)
        {
            new Bulkload<rbd_ParticipantPropertiesSet, rbd_ParticipantProperty>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantPropertiesSet>(xmlfilename).Items, "rbd_ParticipantProperties");
        }

        public static void Bulkload_rbd_Participants(string xmlfilename)
        {
            new Bulkload<rbd_ParticipantsSet, rbd_Participant>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsSet>(xmlfilename).Items, "rbd_Participants");
        }

        public static void Bulkload_rbd_ParticipantsExamPStation(string xmlfilename)
        {
            new Bulkload<rbd_ParticipantsExamPStationSet, rbd_ParticipantsExamPStation>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamPStationSet>(xmlfilename).Items, "rbd_ParticipantsExamPStation");
        }

        public static void Bulkload_rbd_ParticipantsExams(string xmlfilename)
        {
            new Bulkload<rbd_ParticipantsExamsSet, rbd_ParticipantsExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamsSet>(xmlfilename).Items, "rbd_ParticipantsExams");
        }

        public static void Bulkload_rbd_ParticipantsExamsOnStation(string xmlfilename)
        {
            new Bulkload<rbd_ParticipantsExamsOnStationSet, rbd_ParticipantsExamsOnStation>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsExamsOnStationSet>(xmlfilename).Items, "rbd_ParticipantsExamsOnStation");
        }

        public static void Bulkload_rbd_ParticipantsProfSubject(string xmlfilename)
        {
            new Bulkload<rbd_ParticipantsProfSubjectSet, rbd_ParticipantsProfSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsProfSubjectSet>(xmlfilename).Items, "rbd_ParticipantsProfSubject");
        }

        public static void Bulkload_rbd_ParticipantsSubject(string xmlfilename)
        {
            new Bulkload<rbd_ParticipantsSubjectSet, rbd_ParticipantsSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_ParticipantsSubjectSet>(xmlfilename).Items, "rbd_ParticipantsSubject");
        }

        public static void Bulkload_rbd_Places(string xmlfilename)
        {
            new Bulkload<rbd_PlacesSet, rbd_Place>().Load(MainStage.DeserializeXMLFileToObject<rbd_PlacesSet>(xmlfilename).Items, "rbd_Places");
        }

        public static void Bulkload_rbd_SchoolAddress(string xmlfilename)
        {
            new Bulkload<rbd_SchoolAddressSet, rbd_SchoolAddress>().Load(MainStage.DeserializeXMLFileToObject<rbd_SchoolAddressSet>(xmlfilename).Items, "rbd_SchoolAddress");
        }

        public static void Bulkload_rbd_Schools(string xmlfilename)
        {
            new Bulkload<rbd_SchoolsSet, rbd_School>().Load(MainStage.DeserializeXMLFileToObject<rbd_SchoolsSet>(xmlfilename).Items, "rbd_Schools");
        }

        public static void Bulkload_rbd_StationExamAuditory(string xmlfilename)
        {
            new Bulkload<rbd_StationExamAuditorySet, rbd_StationExamAuditory>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationExamAuditorySet>(xmlfilename).Items, "rbd_StationExamAuditory");
        }

        public static void Bulkload_rbd_StationForm(string xmlfilename)
        {
            new Bulkload<rbd_StationFormSet, rbd_StationForm>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormSet>(xmlfilename).Items, "rbd_StationForm");
        }

        public static void Bulkload_rbd_StationFormAct(string xmlfilename)
        {
            new Bulkload<rbd_StationFormActSet, rbd_StationFormAct>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormActSet>(xmlfilename).Items, "rbd_StationFormAct");
        }

        public static void Bulkload_rbd_StationFormAuditoryFields(string xmlfilename)
        {
            new Bulkload<rbd_StationFormAuditoryFieldsSet, rbd_StationFormAuditoryField>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormAuditoryFieldsSet>(xmlfilename).Items, "rbd_StationFormAuditoryFields");
        }

        public static void Bulkload_rbd_StationFormFields(string xmlfilename)
        {
            new Bulkload<rbd_StationFormFieldsSet, rbd_StationFormField>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationFormFieldsSet>(xmlfilename).Items, "rbd_StationFormFields");
        }

        public static void Bulkload_rbd_Stations(string xmlfilename)
        {
            new Bulkload<rbd_StationsSet, rbd_Station>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationsSet>(xmlfilename).Items, "rbd_Stations");
        }

        public static void Bulkload_rbd_StationsExams(string xmlfilename)
        {
            new Bulkload<rbd_StationsExamsSet, rbd_StationsExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationsExamsSet>(xmlfilename).Items, "rbd_StationsExams");
        }

        public static void Bulkload_rbd_StationWorkerOnExam(string xmlfilename)
        {
            new Bulkload<rbd_StationWorkerOnExamSet, rbd_StationWorkerOnExam>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkerOnExamSet>(xmlfilename).Items, "rbd_StationWorkerOnExam");
        }

        public static void Bulkload_rbd_StationWorkerOnStation(string xmlfilename)
        {
            new Bulkload<rbd_StationWorkerOnStationSet, rbd_StationWorkerOnStation>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkerOnStationSet>(xmlfilename).Items, "rbd_StationWorkerOnStation");
        }

        public static void Bulkload_rbd_StationWorkers(string xmlfilename)
        {
            new Bulkload<rbd_StationWorkersSet, rbd_StationWorker>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersSet>(xmlfilename).Items, "rbd_StationWorkers");
        }

        public static void Bulkload_rbd_StationWorkersAccreditation(string xmlfilename)
        {
            new Bulkload<rbd_StationWorkersAccreditationSet, rbd_StationWorkersAccreditation>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersAccreditationSet>(xmlfilename).Items, "rbd_StationWorkersAccreditation");
        }

        public static void Bulkload_rbd_StationWorkersSubjects(string xmlfilename)
        {
            new Bulkload<rbd_StationWorkersSubjectsSet, rbd_StationWorkersSubject>().Load(MainStage.DeserializeXMLFileToObject<rbd_StationWorkersSubjectsSet>(xmlfilename).Items, "rbd_StationWorkersSubjects");
        }

        public static void Bulkload_res_Answers(string xmlfilename)
        {
            new Bulkload<res_AnswersSet, res_Answer>().Load(MainStage.DeserializeXMLFileToObject<res_AnswersSet>(xmlfilename).Items, "res_Answers");
        }

        public static void Bulkload_res_Complects(string xmlfilename)
        {
            new Bulkload<res_ComplectsSet, res_Complect>().Load(MainStage.DeserializeXMLFileToObject<res_ComplectsSet>(xmlfilename).Items, "res_Complects");
        }

        public static void Bulkload_res_HumanTests(string xmlfilename)
        {
            new Bulkload<res_HumanTestsSet, res_HumanTest>().Load(MainStage.DeserializeXMLFileToObject<res_HumanTestsSet>(xmlfilename).Items, "res_HumanTests");
        }

        public static void Bulkload_res_Marks(string xmlfilename)
        {
            new Bulkload<res_MarksSet, res_Mark>().Load(MainStage.DeserializeXMLFileToObject<res_MarksSet>(xmlfilename).Items, "res_Marks");
        }

        public static void Bulkload_res_SubComplects(string xmlfilename)
        {
            new Bulkload<res_SubComplectsSet, res_SubComplect>().Load(MainStage.DeserializeXMLFileToObject<res_SubComplectsSet>(xmlfilename).Items, "res_SubComplects");
        }

        public static void Bulkload_res_Subtests(string xmlfilename)
        {
            new Bulkload<res_SubtestsSet, res_SubTest>().Load(MainStage.DeserializeXMLFileToObject<res_SubtestsSet>(xmlfilename).Items, "res_Subtests");
        }

        public static void Bulkload_sht_Alts(string xmlfilename)
        {
            new Bulkload<sht_AltsSet, sht_Alt>().Load(MainStage.DeserializeXMLFileToObject<sht_AltsSet>(xmlfilename).Items, "sht_Alts");
        }

        public static void Bulkload_sht_FinalMarks_C(string xmlfilename)
        {
            new Bulkload<sht_FinalMarks_CSet, sht_FinalMarks_C>().Load(MainStage.DeserializeXMLFileToObject<sht_FinalMarks_CSet>(xmlfilename).Items, "sht_FinalMarks_C");
        }

        public static void Bulkload_sht_FinalMarks_D(string xmlfilename)
        {
            new Bulkload<sht_FinalMarks_DSet, sht_FinalMarks_D>().Load(MainStage.DeserializeXMLFileToObject<sht_FinalMarks_DSet>(xmlfilename).Items, "sht_FinalMarks_D");
        }

        public static void Bulkload_sht_Marks_AB(string xmlfilename)
        {
            new Bulkload<sht_Marks_ABSet, sht_Marks_AB>().Load(MainStage.DeserializeXMLFileToObject<sht_Marks_ABSet>(xmlfilename).Items, "sht_Marks_AB");
        }

        public static void Bulkload_sht_Marks_C(string xmlfilename)
        {
            new Bulkload<sht_Marks_CSet, sht_Marks_C>().Load(MainStage.DeserializeXMLFileToObject<sht_Marks_CSet>(xmlfilename).Items, "sht_Marks_C");
        }

        public static void Bulkload_sht_Marks_D(string xmlfilename)
        {
            new Bulkload<sht_Marks_DSet, sht_Marks_D>().Load(MainStage.DeserializeXMLFileToObject<sht_Marks_DSet>(xmlfilename).Items, "sht_Marks_D");
        }

        public static void Bulkload_sht_Packages(string xmlfilename)
        {
            new Bulkload<sht_PackagesSet, sht_Package>().Load(MainStage.DeserializeXMLFileToObject<sht_PackagesSet>(xmlfilename).Items, "sht_Packages");
        }

        public static void Bulkload_sht_Sheets_AB(string xmlfilename)
        {
            new Bulkload<sht_Sheets_ABSet, sht_Sheets_AB>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_ABSet>(xmlfilename).Items, "sht_Sheets_AB");
        }

        public static void Bulkload_sht_Sheets_C(string xmlfilename)
        {
            new Bulkload<sht_Sheets_CSet, sht_Sheets_C>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_CSet>(xmlfilename).Items, "sht_Sheets_C");
        }

        public static void Bulkload_sht_Sheets_D(string xmlfilename)
        {
            new Bulkload<sht_Sheets_DSet, sht_Sheets_D>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_DSet>(xmlfilename).Items, "sht_Sheets_D");
        }

        public static void Bulkload_sht_Sheets_R(string xmlfilename)
        {
            new Bulkload<sht_Sheets_RSet, sht_Sheets_R>().Load(MainStage.DeserializeXMLFileToObject<sht_Sheets_RSet>(xmlfilename).Items, "sht_Sheets_R");
        }


        private static void Handler(BulkCopyRowsCopied obj)
        {

        }

    }
}
