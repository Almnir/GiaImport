using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace GiaImport
{
    class Verifier
    {
        public static List<string> xsdList = new List<string>()
        {
            "ac_Appeals.xsd",
            "ac_AppealTasks.xsd",
            "ac_Changes.xsd",
            "dats_Borders.xsd",
            "dats_Groups.xsd",
            "prnf_CertificatePrintMain.xsd",
            "rbd_Address.xsd",
            "rbd_Areas.xsd",
            "rbd_Auditoriums.xsd",
            "rbd_AuditoriumsSubjects.xsd",
            "rbd_CurrentRegion.xsd",
            "rbd_CurrentRegionAddress.xsd",
            "rbd_Experts.xsd",
            "rbd_ExpertsExams.xsd",
            "rbd_ExpertsSubjects.xsd",
            "rbd_Governments.xsd",
            "rbd_ParticipantProperties.xsd",
            "rbd_Participants.xsd",
            "rbd_ParticipantsExamPStation.xsd",
            "rbd_ParticipantsExams.xsd",
            "rbd_ParticipantsExamsOnStation.xsd",
            "rbd_ParticipantsProfSubject.xsd",
            "rbd_ParticipantsSubject.xsd",
            "rbd_Places.xsd",
            "rbd_SchoolAddress.xsd",
            "rbd_Schools.xsd",
            "rbd_StationExamAuditory.xsd",
            "rbd_StationForm.xsd",
            "rbd_StationFormAct.xsd",
            "rbd_StationFormAuditoryFields.xsd",
            "rbd_StationFormFields.xsd",
            "rbd_Stations.xsd",
            "rbd_StationsExams.xsd",
            "rbd_StationWorkerOnExam.xsd",
            "rbd_StationWorkerOnStation.xsd",
            "rbd_StationWorkers.xsd",
            "rbd_StationWorkersAccreditation.xsd",
            "rbd_StationWorkersSubjects.xsd",
            "res_Answers.xsd",
            "res_Complects.xsd",
            "res_HumanTests.xsd",
            "res_Marks.xsd",
            "res_SubComplects.xsd",
            "res_Subtests.xsd",
            "sht_Alts.xsd",
            "sht_FinalMarks_C.xsd",
            "sht_FinalMarks_D.xsd",
            "sht_Marks_AB.xsd",
            "sht_Marks_C.xsd",
            "sht_Marks_D.xsd",
            "sht_Packages.xsd",
            "sht_Sheets_AB.xsd",
            "sht_Sheets_C.xsd",
            "sht_Sheets_D.xsd",
            "sht_Sheets_R.xsd"
        };

        public ConcurrentDictionary<string, string> errorDict = new ConcurrentDictionary<string, string>();

        public static string GetPath(string filename)
        {
            string curdir = Directory.GetCurrentDirectory();
            return curdir + @"\XSD\" + filename;
        }

        public bool errorState { get; set; }

        public string errorString { get; set; }

        public Verifier()
        {
            this.errorState = false;
            this.errorString = string.Empty;
        }

        //public async void VerifySingleFile(string xsdFileName, string xmlFileName, IProgress<int> progress)
        //{
        //    XmlReaderSettings readerSettings = new XmlReaderSettings();
        //    readerSettings.Async = true;
        //    // TODO: хардкод, вынести в константы
        //    readerSettings.Schemas.Add("http://www.rustest.ru/giadbset", xsdFileName);
        //    readerSettings.ValidationType = ValidationType.Schema;
        //    readerSettings.ValidationEventHandler += (sender, e) => ValidationEventHandler(sender, e, xsdFileName);

        //    XmlReader xml = XmlReader.Create(xmlFileName, readerSettings);
        //    int progressCounter = 0;
        //    while (await xml.ReadAsync())
        //    {
        //        //progress.Report(progressCounter);
        //        //progressCounter++;
        //    }
        //}
        public void VerifySingleFile(string xsdFileName, string xmlFileName, CancellationToken ct)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.Async = false;
            // TODO: хардкод, вынести в константы
            readerSettings.Schemas.Add("http://www.rustest.ru/giadbset", xsdFileName);
            readerSettings.ValidationType = ValidationType.Schema;
            string tableName = Path.GetFileNameWithoutExtension(xmlFileName);
            readerSettings.ValidationEventHandler += (sender, e) => ValidationEventHandler(sender, e, tableName);

            XmlReader xml = XmlReader.Create(xmlFileName, readerSettings);
            while (xml.Read())
            {
                ct.ThrowIfCancellationRequested();
                //progress.Report(progressCounter);
                //progressCounter++;
            }
        }

        public void ValidationEventHandler(object sender, ValidationEventArgs e, string tableName)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                // TODO: что-то сделать с предупреждениями
                //log.Warn("WARNING: ");
                //log.Warn(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                this.errorState = true;
                this.errorString += e.Message;
                this.errorString += Environment.NewLine;
                this.errorDict.TryAdd(tableName, this.errorString);
            }
        }

    }
}
