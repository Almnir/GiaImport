using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD;
using RBD.Common.Enums;
using RBD.Common.Extensions;

namespace FCT.Client.Dto
{
    [Serializable][Description("Эксперт")]
    [BulkTable("rbd_Experts", "Experts", RootTagName = "ArrayOfExpertsDto")]
    public class ExpertsDto : DtoCreateDateBase, IEquatable<ExpertsDto>, IDtoCollectorAccepter, IUidableDto, IDtoWithDocument, IPeopleDto
    {
        [BulkColumn("ExpertID")]
        public override Guid DtoID { get; set; }

        [BulkColumn]
        [Description("Код эксперта")]
        public int ExpertCode { get; set; }

        [BulkColumn]
        [Description("Фамилия")]
        public string Surname { get; set; }

        [BulkColumn]
        [Description("Имя")]
        public string Name { get; set; }

        [BulkColumn]
        [Description("Отчество")]
        public string SecondName { get; set; }
        
        [Description("Тип документа")]
        [XmlIgnore] public string DocumentTypeName { get { return DocumentTypeDto.Return(x => x.DocumentTypeName, "---"); } }

        [BulkColumn]
        [Description("Серия документа")]
        public string DocumentSeries { get; set; }

        [BulkColumn]
        [Description("Номер документа")]
        public string DocumentNumber { get; set; }

        [BulkColumn]
        public int EduTypeFK { get; set; }

        [BulkColumn]
        public int EduKindFK { get; set; }

        public int? EducationType { get; set; }
        public int? EducationTypeCode { get; set; }
        [Description("Уровень образования")]
        [XmlIgnore]
        public string EducationTypeName { get { return EducationTypeDto.Return(x => x.EduTypeName, "---"); } }

        public int? EducationKind { get; set; }
        public int? EducationKindCode { get; set; }
        [Description("Ученая степень")]
        [XmlIgnore]
        public string EducationKindName { get { return EducationKindDto.Return(x => x.EduKindName, "---"); } }

        [BulkColumn]
        [Description("Допуск к третьей проверке")]
        [XmlIgnore] public bool ThirdVerifyAcc { get; set; }

        [Description("Пол")]
        [XmlIgnore] public string SexName { get { return Sex != null ? Sex.Value.GetDescription() : "---"; } }

        [BulkColumn]
        [Description("Стаж работы")]
        public int Seniority { get; set; }

        [BulkColumn("PrecedingYear", typeof(int))]
        public int? PrecedingYear { get; set; }

        [Description("Участвовал в ЕГЭ ранее")]
        public string PrecedingYearName
        {
            get
            {
                if (!PrecedingYear.HasValue) return "---";
                return PrecedingYear.Value == 1 ? "Да" : "Нет";
            }
        }

        [BulkColumn]
        [Description("Год рождения")]
        public string BirthYear { get; set; }

        [XmlIgnore]
        public int BirthYearInt
        {
            get
            {
                if (string.IsNullOrEmpty(BirthYear))
                    return 0;

                int year;
                if (Int32.TryParse(BirthYear, out year))
                    return year;

                return 0;
            }
        }

        [Description("ОО - основное место работы")]
        [XmlIgnore]
        public string SchoolName { get { return SchoolDto.Return(x => x.ToString(), "---"); } }

        [XmlIgnore]
        public Guid SchoolID { get; set; }

        [Description("МСУ эксперта")]
        [XmlIgnore] public string GovernmentName { get { return GovernmentDto.Return(x => x.ToString(), "---"); } }

        [Description("Основное место работы (если не ОО)")]
        public string NotSchoolJob { get; set; }

        [BulkColumn]
        [Description("Должность по основному месту работы")]
        [XmlElement("Positions")]
        public string Positions { get; set; }

        [Description("Квалификация")]
        public string Qualification { get; set; }

        #region InConflictCommission
        [XmlIgnore] public int InConflictCommissionProperty { set { InConflictCommission = Convert.ToBoolean(value); } }

        [BulkColumn]
        [Description("Вхождение в конфликтную комиссию")]
        [XmlIgnore] public bool InConflictCommission { get; set; }

        [XmlElement("InConflictCommission")]
        public string InConflictCommissionSerialize
        {
            get { return InConflictCommission ? "1" : "0"; }
            set { InConflictCommission = XmlConvert.ToBoolean(value); }
        }
        #endregion

        #region NonSerializable

        [XmlIgnore] public string FIO { get { return string.Format("{0} {1} {2}", Surname, Name, SecondName); } }
        [XmlIgnore] public DocumentTypesDto DocumentTypeDto { get; set; }
        [XmlIgnore] public EducationTypesDto EducationTypeDto { get; set; }
        [XmlIgnore] public EducationKindsDto EducationKindDto { get; set; }
        [XmlIgnore] public SchoolsDto SchoolDto { get; set; }
        [XmlIgnore] public GovernmentsDto GovernmentDto { get; set; }
        [XmlIgnore] public int? SexProperty { set { if (value != null) Sex = (Gender)value; } }
        [XmlIgnore] public int IsDeletedProperty { set { IsDeleted = Convert.ToBoolean(value); } }
        [XmlIgnore] public string PositionProperty { set { Positions = value; } }
        [XmlIgnore] public int ThirdVerifyAccProperty { set { ThirdVerifyAcc = Convert.ToBoolean(value); } }

        #endregion

#if GiaDataCollect
        [XmlElement("Region")]
#else
        [XmlElement("Region")]
#endif
        [BulkColumn("REGION")]
        public override int Region { get; set; }

        
#if GiaDataCollect
        [XmlElement("DocumentTypeCode")]
#else
        [XmlElement("DocumentTypeCode")]
#endif
        [BulkColumn]
        public int DocumentTypeCode { get; set; }

        public Gender? Sex { get; set; }

        [BulkColumn]
        [XmlElement("IsDeleted")]
        public string IsDeletedSerialize
        {
            get { return IsDeleted ? "1" : "0"; }
            set { IsDeleted = XmlConvert.ToBoolean(value); }
        }

        public Guid? School { get; set; }

        public Guid? GovernmentID { get; set; }

        [BulkColumn]
        public int ExpertCategoryID { get; set; }

        [BulkColumn]
        [XmlElement("ThirdVerifyAcc")]
        public string ThirdVerifyAccSerialize
        {
            get { return ThirdVerifyAcc ? "1" : "0"; }
            set { ThirdVerifyAcc = XmlConvert.ToBoolean(value); }
        }

        #region IEquatable<ExpertsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ExpertsDto)) return false;
            return Equals((ExpertsDto) obj);
        }

        public bool Equals(ExpertsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.DtoID.Equals(DtoID);
        }

        public override int GetHashCode()
        {
            unchecked { return DtoID.ToString().GetHashCode(); } 
        }

        public override int CompareTo(object obj)
        {
            var other = obj as ExpertsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.IsDeleted.Equals(IsDeleted), TypeExtensions.Description<ExpertsDto>(c => c.IsDeleted));
            result &= CheckChanges(other.ExpertCode == ExpertCode, TypeExtensions.Description<ExpertsDto>(c => c.ExpertCode));
            //result &= CheckChanges(Equals(other.EducationTypeCode, EducationTypeCode), TypeExtensions.Description<ExpertsDto>(c => c.EducationTypeName));
            //result &= CheckChanges(Equals(other.EducationKindCode, EducationKindCode), TypeExtensions.Description<ExpertsDto>(c => c.EducationKindName));
            result &= CheckChanges(StringEquals(other.Surname, Surname), TypeExtensions.Description<ExpertsDto>(c => c.Surname));
            result &= CheckChanges(StringEquals(other.Name, Name), TypeExtensions.Description<ExpertsDto>(c => c.Name));
            result &= CheckChanges(StringEquals(other.SecondName, SecondName), TypeExtensions.Description<ExpertsDto>(c => c.SecondName));
            result &= CheckChanges(StringEquals(other.DocumentSeries, DocumentSeries), TypeExtensions.Description<ExpertsDto>(c => c.DocumentSeries));
            result &= CheckChanges(StringEquals(other.DocumentNumber, DocumentNumber), TypeExtensions.Description<ExpertsDto>(c => c.DocumentNumber));
            result &= CheckChanges(other.ThirdVerifyAcc.Equals(ThirdVerifyAcc), TypeExtensions.Description<ExpertsDto>(c => c.ThirdVerifyAcc));
            result &= CheckChanges(other.Seniority == Seniority, TypeExtensions.Description<ExpertsDto>(c => c.Seniority));
            result &= CheckChanges(Equals(other.PrecedingYear, PrecedingYear), TypeExtensions.Description<ExpertsDto>(c => c.PrecedingYearName));
            result &= CheckChanges(StringEquals(other.BirthYear, BirthYear), TypeExtensions.Description<ExpertsDto>(c => c.BirthYear));
            result &= CheckChanges(StringEquals(other.NotSchoolJob, NotSchoolJob), TypeExtensions.Description<ExpertsDto>(c => c.NotSchoolJob));
            result &= CheckChanges(StringEquals(other.Positions, Positions), TypeExtensions.Description<ExpertsDto>(c => c.Positions));
            result &= CheckChanges(other.DocumentTypeCode == DocumentTypeCode, TypeExtensions.Description<ExpertsDto>(c => c.DocumentTypeName));
            result &= CheckChanges(other.Sex.Equals(Sex), TypeExtensions.Description<ExpertsDto>(c => c.SexName));
            result &= CheckChanges(Equals(other.SchoolID, SchoolID), TypeExtensions.Description<ExpertsDto>(c => c.SchoolName));
            result &= CheckChanges(Equals(other.GovernmentID, GovernmentID), TypeExtensions.Description<ExpertsDto>(c => c.GovernmentName));
            result &= CheckChanges(StringEquals(other.Qualification, Qualification), TypeExtensions.Description<ExpertsDto>(c => c.Qualification));
            result &= CheckChanges(Equals(other.InConflictCommission, InConflictCommission), TypeExtensions.Description<ExpertsDto>(c => c.InConflictCommission));

            return result ? 0 : 1;
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID { get; set; }
#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string GovernmentUID { get; set; }
#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string SchoolUID { get; set; }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public void Collect(IDtoDataCollector collector)
        {
            collector.Collect(this);
        }
    }
}
