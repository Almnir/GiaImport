using DataModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace linq2dbpro.DataModels
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class ac_AppealsSet
    {
        private ac_Appeal[] itemsField;

        [XmlElement("ac_Appeals", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ac_Appeal[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class ac_AppealTasksSet
    {

        private ac_AppealTask[] itemsField;

        [XmlElement("ac_AppealTasks", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ac_AppealTask[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class ac_ChangesSet
    {

        private ac_Change[] itemsField;

        [XmlElement("ac_Changes", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ac_Change[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class dats_BordersSet
    {

        private dats_Border[] itemsField;

        [XmlElement("dats_Borders", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public dats_Border[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class dats_GroupsSet
    {

        private dats_Group[] itemsField;

        [XmlElement("dats_Groups", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public dats_Group[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class prnf_CertificatePrintMainSet
    {

        private prnf_CertificatePrintMain[] itemsField;

        [XmlElement("prnf_CertificatePrintMain", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public prnf_CertificatePrintMain[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_AddressSet
    {

        private rbd_Address[] itemsField;

        [XmlElement("rbd_Address", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Address[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_AreasSet
    {

        private rbd_Area[] itemsField;

        [XmlElement("rbd_Areas", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Area[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_AuditoriumsSet
    {

        private rbd_Auditorium[] itemsField;

        [XmlElement("rbd_Auditoriums", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Auditorium[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_AuditoriumsSubjectsSet
    {

        private rbd_AuditoriumsSubject[] itemsField;

        [XmlElement("rbd_AuditoriumsSubjects", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_AuditoriumsSubject[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_CurrentRegionSet
    {

        private rbd_CurrentRegion[] itemsField;

        [XmlElement("rbd_CurrentRegion", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_CurrentRegion[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_CurrentRegionAddressSet
    {

        private rbd_CurrentRegionAddress[] itemsField;

        [XmlElement("rbd_CurrentRegionAddress", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_CurrentRegionAddress[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ExpertsSet
    {

        private rbd_Expert[] itemsField;

        [XmlElement("rbd_Experts", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Expert[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ExpertsExamsSet
    {

        private rbd_ExpertsExam[] itemsField;

        [XmlElement("rbd_ExpertsExams", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ExpertsExam[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ExpertsSubjectsSet
    {

        private rbd_ExpertsSubject[] itemsField;

        [XmlElement("rbd_ExpertsSubjects", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ExpertsSubject[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_GovernmentsSet
    {

        private rbd_Government[] itemsField;

        [XmlElement("rbd_Governments", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Government[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ParticipantPropertiesSet
    {

        private rbd_ParticipantProperty[] itemsField;

        [XmlElement("rbd_ParticipantProperties", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ParticipantProperty[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ParticipantsSet
    {

        private rbd_Participant[] itemsField;

        [XmlElement("rbd_Participants", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Participant[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ParticipantsExamPStationSet
    {

        private rbd_ParticipantsExamPStation[] itemsField;

        [XmlElement("rbd_ParticipantsExamPStation", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ParticipantsExamPStation[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ParticipantsExamsSet
    {

        private rbd_ParticipantsExam[] itemsField;

        [XmlElement("rbd_ParticipantsExams", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ParticipantsExam[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ParticipantsExamsOnStationSet
    {

        private rbd_ParticipantsExamsOnStation[] itemsField;

        [XmlElement("rbd_ParticipantsExamsOnStation", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ParticipantsExamsOnStation[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ParticipantsProfSubjectSet
    {

        private rbd_ParticipantsProfSubject[] itemsField;

        [XmlElement("rbd_ParticipantsProfSubject", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ParticipantsProfSubject[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_ParticipantsSubjectSet
    {

        private rbd_ParticipantsSubject[] itemsField;

        [XmlElement("rbd_ParticipantsSubject", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_ParticipantsSubject[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_PlacesSet
    {

        private rbd_Place[] itemsField;

        [XmlElement("rbd_Places", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Place[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_SchoolAddressSet
    {

        private rbd_SchoolAddress[] itemsField;

        [XmlElement("rbd_SchoolAddress", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_SchoolAddress[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_SchoolsSet
    {

        private rbd_School[] itemsField;

        [XmlElement("rbd_Schools", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_School[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationExamAuditorySet
    {

        private rbd_StationExamAuditory[] itemsField;

        [XmlElement("rbd_StationExamAuditory", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationExamAuditory[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationFormSet
    {

        private rbd_StationForm[] itemsField;

        [XmlElement("rbd_StationForm", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationForm[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationFormActSet
    {

        private rbd_StationFormAct[] itemsField;

        [XmlElement("rbd_StationFormAct", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationFormAct[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationFormAuditoryFieldsSet
    {

        private rbd_StationFormAuditoryField[] itemsField;

        [XmlElement("rbd_StationFormAuditoryFields", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationFormAuditoryField[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationFormFieldsSet
    {

        private rbd_StationFormField[] itemsField;

        [XmlElement("rbd_StationFormFields", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationFormField[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationsSet
    {

        private rbd_Station[] itemsField;

        [XmlElement("rbd_Stations", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_Station[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationsExamsSet
    {

        private rbd_StationsExam[] itemsField;

        [XmlElement("rbd_StationsExams", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationsExam[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationWorkerOnExamSet
    {

        private rbd_StationWorkerOnExam[] itemsField;

        [XmlElement("rbd_StationWorkerOnExam", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationWorkerOnExam[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationWorkerOnStationSet
    {

        private rbd_StationWorkerOnStation[] itemsField;

        [XmlElement("rbd_StationWorkerOnStation", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationWorkerOnStation[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationWorkersSet
    {

        private rbd_StationWorker[] itemsField;

        [XmlElement("rbd_StationWorkers", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationWorker[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationWorkersAccreditationSet
    {

        private rbd_StationWorkersAccreditation[] itemsField;

        [XmlElement("rbd_StationWorkersAccreditation", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationWorkersAccreditation[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class rbd_StationWorkersSubjectsSet
    {

        private rbd_StationWorkersSubject[] itemsField;

        [XmlElement("rbd_StationWorkersSubjects", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public rbd_StationWorkersSubject[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class res_AnswersSet
    {

        private res_Answer[] itemsField;

        [XmlElement("res_Answers", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public res_Answer[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class res_ComplectsSet
    {

        private res_Complect[] itemsField;

        [XmlElement("res_Complects", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public res_Complect[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class res_HumanTestsSet
    {

        private res_HumanTest[] itemsField;

        [XmlElement("res_HumanTests", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public res_HumanTest[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class res_MarksSet
    {

        private res_Mark[] itemsField;

        [XmlElement("res_Marks", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public res_Mark[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class res_SubComplectsSet
    {

        private res_SubComplect[] itemsField;

        [XmlElement("res_SubComplects", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public res_SubComplect[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class res_SubtestsSet
    {

        private res_SubTest[] itemsField;

        [XmlElement("res_Subtests", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public res_SubTest[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_AltsSet
    {

        private sht_Alt[] itemsField;

        [XmlElement("sht_Alts", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Alt[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_FinalMarks_CSet
    {

        private sht_FinalMarks_C[] itemsField;

        [XmlElement("sht_FinalMarks_C", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_FinalMarks_C[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_FinalMarks_DSet
    {

        private sht_FinalMarks_D[] itemsField;

        [XmlElement("sht_FinalMarks_D", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_FinalMarks_D[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_Marks_ABSet
    {

        private sht_Marks_AB[] itemsField;

        [XmlElement("sht_Marks_AB", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Marks_AB[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_Marks_CSet
    {

        private sht_Marks_C[] itemsField;

        [XmlElement("sht_Marks_C", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Marks_C[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_Marks_DSet
    {

        private sht_Marks_D[] itemsField;

        [XmlElement("sht_Marks_D", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Marks_D[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_PackagesSet
    {

        private sht_Package[] itemsField;

        [XmlElement("sht_Packages", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Package[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_Sheets_ABSet
    {

        private sht_Sheets_AB[] itemsField;

        [XmlElement("sht_Sheets_AB", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Sheets_AB[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_Sheets_CSet
    {

        private sht_Sheets_C[] itemsField;

        [XmlElement("sht_Sheets_C", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Sheets_C[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_Sheets_DSet
    {

        private sht_Sheets_D[] itemsField;

        [XmlElement("sht_Sheets_D", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Sheets_D[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = false, IncludeInSchema = true, Namespace = "http://www.rustest.ru/giadbset")]
    [XmlRoot(Namespace = "http://www.rustest.ru/giadbset", IsNullable = true, ElementName = "GIADBSet")]
    public class sht_Sheets_RSet
    {

        private sht_Sheets_R[] itemsField;

        [XmlElement("sht_Sheets_R", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public sht_Sheets_R[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}
