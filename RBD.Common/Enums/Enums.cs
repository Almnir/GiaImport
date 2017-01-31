using System;
using System.ComponentModel;
using System.Xml.Serialization;
using RBD.Common.Attributes;

namespace RBD.Common.Enums
{
    public enum RBDExtensions
    {
        None = 0,
        StringToDate = 1,
        EnumGenderFix = 2,
        EnumGovTypeFix = 4,
        StringToDateFromLongString = 8
    }

    public enum TaskType
    {
        [Description("A"), XmlEnum("0")]
        A = 0,
        [Description("B"), XmlEnum("1")]
        B = 1,
        [Description("C"), XmlEnum("2")]
        C = 2,
        [Description("D"), XmlEnum("3")]
        D = 3
    }

    public enum WorkerTypeOrder
    {
        [Description("По месту работы")]
        AllWorkerInSchool,
        [Description("По ППЭ, к которому прикреплён")]
        AllWorkerInStation,
        [Description("По должности")]
        Position,
        [Description("Не группировать")]
        None
    }

    public enum ShowType
    {
        All,
        OnlyExamAppiontment
    }

    public enum SearchTemplateType
    {
        [Description("Системный")]
        System,
        [Description("Пользовательский")]
        User,
        [Description("Системный")]
        Autostart
    }

    public enum PrintFailure
    {
        [Description("Двойные предметы")]
        DoubleSubjects = 1,

        [Description("Апелляция по основному этапу")]
        AppealMainWave = 2,

        [Description("Апелляция по дополнительному этапу")]
        AppealDopWave = 3,

        [Description("Все предметы на оценку 2")]
        Looser = 6
    }

    [Flags]
    public enum ImportPlanningType
    {
        None = 0,
        Auditory = 1,
        Workers = 2,
        Participants = 4,
        //Seating = 8,
        CSVfromPPE = 16
    }

    public enum LayerTypeEnum
    {
        [Description("Справочник")]
        Dictionary,
        [Description("Сбор")]
        Sbor,
        [Description("Планирование")]
        Planning,
        [Description("---")]
        Undefined
    }

    public enum ImportSeatAction
    {
        Cancel = 0,
        DelSeat = 1
    }

#if GIA
    public enum Marks
    {
        Unknown = 0,
        VeryBad = 1,
        Bad = 2,
        NotBad = 3,
        Good= 4,
        Excelent = 5
    }
#else
    public enum Marks
    {
        Unknown = 0,
        Bad = 2,
        Good = 5
    }
#endif
    public enum ImportPlanningMethod
    {
        Addon = 0,
        AllRefresh = 1
    }

    public enum ImportSource
    {
        PPE,
        CSV,
        Inner
    }

    [Flags]
    public enum ImportObjectType
    {
        All = 0,
        MOYO = 1,
        OY = 2,
        Participant = 4,
        PPE = 8,
        StationWorker = 16,
        //Seating = 32,
        CSVfromPPE = 64
    }

    public enum ConnectionType
    {
        Local,
        Net
    }

    public enum  DocumentTypes
    {
        [Description("Удостоверение личности")]
#if GIA
        IdentityCard = 15,
#else
        IdentityCard = 14,
#endif
        [Description("Свидетельство о рождении")]
#if GIA
        CertificateOfBirth = 14,
#else
        CertificateOfBirth = -1
#endif
    }
    
    public enum WorkerType
    {
        [Description("Прикреплен к ППЭ")]
        WorkerOnStation = 0,
        [Description("Назначен на экзамен")]
        WorkerOnExam = 1
    }

    public enum ImportVersionResult
    {
        OK,
        Fail,
        UpdateNeeded
    }

    public enum ExportTypeDirection
    {
        Up,
        Down
    }

    public enum MaterialsCalculateType
    {
        [Description("По количеству участников")]
        ParticipantOnly = 0,
        [Description("По количеству участников и аудиториям")]
        ParticipantAuditories = 1
    }

    public enum AuditoriumFillOrder
    {
        [Description("Заполнять все назначенные аудитории равномерно")]
        UniformlyAllAppointed = 0,
        [Description("Заполнять последовательно по возрастанию кода аудитории")]
        СonsequentiallyCodeAscending = 1,
        [Description("Заполнять последовательно по убыванию вместимости аудитории")]
        СonsequentiallySizeDescending = 2,
        [Description("Заполнять последовательно по возрастанию вместимости аудитории")]
        СonsequentiallySizeAscending = 3
            ,
    }

    public enum LinkObjectAction
    {
        NoAction,
        CascadeUpdate,
        FailLink
    }

    public enum ExportDataType
    {
        // Dictionary = 0,
        // Data = 1,
        All = 2,
        ExportPlaning = 3,
        WithoutPlaning = 4
    }

    public enum ControlMode
    {
        View,
        New,
        Edit
    }

    public enum MOYOType
    {
        [Description("Муниципальный ОИВ"), XmlEnum("0")]
        Munitipal = 0,
        [Description("ОИВ субъекта РФ"), XmlEnum("1")]
        SubeckRF = 1,
        [Description("Неизвестный тип"), XmlEnum("2")]
        UnknownType = 2
    }

    public enum ExamType
    {
        [ExtendedDescription("Основной", "осн.")]
        Main = 0,
        [ExtendedDescription("Резервный", "резерв."), XmlEnum("1")]
        Reserv = 1
    }

    [Flags]
    public enum Gia
    {
        None = 0,
#if GIA
        [Description("ОГЭ"), XmlEnum("1")]
#else
        [Description("ЕГЭ"), XmlEnum("1")]
#endif
        Ege = 1,
        [Description("ГВЭ"), XmlEnum("2")]
        Gve = 2,
        [Description("Другое"), XmlEnum("4")]
        Other = 4,
    }

    public enum TestTypeCode
    {
#if GIA
        [Description("Сочинение/Изложение"), XmlEnum("3")]
        Opus = 3,
        [Description("ОГЭ"), XmlEnum("6")]
        Ege = 6,
        [Description("ГВЭ"), XmlEnum("7")]
        Gve = 7,
        [Description("Другое"), XmlEnum("8")]
        Other = 8
#else
        [Description("ЕГЭ"), XmlEnum("4")]
        Ege = 4,
        [Description("ГВЭ"), XmlEnum("5")]
        Gve = 5,
        [Description("Другое"), XmlEnum("6")]
        Other = 6,
        [Description("Сочинение/Изложение"), XmlEnum("3")]
        Opus = 3
#endif
    }

    [Flags]
    public enum GiaAccept
    {
        [Description("Результатов ГИА нет"), XmlEnum("0")]
        None = 0,
        [Description("Прошёл ГИА по русскому"), XmlEnum("1")]
        Russian = 1,
        [Description("Прошёл ГИА по математике"), XmlEnum("2")]
        Mathematic = 2,
        [Description("Прошёл ГИА по математике и по русскому"), XmlEnum("3")]
        Both = 3
    }

    /// <summary>
    /// Пол
    /// </summary>
    public enum Gender
    {
        [Description("Мужской"), XmlEnum("0")]
        Male = 0,
        [Description("Женский"), XmlEnum("1")]
        Female = 1
    }

    /// <summary>
    /// Признак специализированной рассадки
    /// </summary>
    public enum LimitPotencial
    {
        [Description("Общий принцип"), XmlEnum("0")]
        Share = 0,
        [Description("Специализированный"), XmlEnum("1")]
        Limit = 1
    }

    /// <summary>
    /// Расположение рядов в аудитории
    /// </summary>
    public enum AuditoriumDirection
    {
        [Description("Вертикальный"), XmlEnum("0")]
        Vertical = 0,
        [Description("Горизонтальный"), XmlEnum("1")]
        Horizontal = 1
    }

    public enum ReportDataType
    {
        [Description("МСУ")]
        MOYO = 0,
        [Description("АТЕ")]
        ATE,
        [Description("ОО")]
        OY,
        [Description("Город/район")]
        Town,
        [Description("Класс")]
        Room,
        [Description("Не разбивать")]
        None,
        [Description("ППЭ")]
        Station,
        [Description("Участник")]
        Participant,
        [Description("Работник ППЭ")]
        StationWorker
    }

    public enum StationWorkersReportSplit
    {
        [Description("Не разбивать")]
        NONE = 0,
        [Description("ППЭ")]
        STATION = 1,
        [Description("ОО")]
        OY = 2
    }

    public enum MainTreeType
    {
        [Description("Регион")]
        Region = 0,
        [Description("ОИВ, ГЭК, РЦОИ")]
        [GiaDescription("ОИВ, ГЭК(РЭК), РЦОИ")]
        RCOI = 1,
        [Description("МСУ")]
        MOYO = 2,
        [Description("ОО")]
        OY = 3,
        [Description("АТЕ")]
        ATE = 4,
        [Description("ППОИ")]
        PPOI = 5,
        [Description("МСУ {0} ({1})")]
        ConcreteMOYO = 6,
        [Description("Участники")]
        Participants = 7,
        [Description("ОО {0} ({1})")]
        ConcreteOY = 8,
        [Description("ППЭ")]
        PPE = 9,
        [Description("Эксперты по специализации")]
        Experts = 10,
        [Description("Предметы")]
        ExpertSubjects = 11,
        [Description("Пользователи")]
        Users = 12,
        [Description("Журнал действий пользователя")]
        Audit = 13,
        [Description("Роли")]
        Roles = 14,
        [Description("Работники ППЭ")]
        PPEWorkers = 15,
        [Description("АТЕ для выбора")]
        ConcreteATE = 16,
        [Description("АТЕ-папка")]
        FolderATE = 17,
        [Description("Резервное копирование")]
        ReservCopy = 18,
        [Description("Удаленные объекты")]
        DeletedObjects = 19,
        [Description("Обновление программы")]
        Update = 20,
        [Description("Проверка целостности РБД")]
        [GiaDescription("Проверка целостности базы данных")]
        Integrity = 21,
        [Description("Подключение к базе данных")]
        ConnectionEditor = 22,
        [Description("Массовая регистрация на экзамен")]
        ExamRegistry = 23,
        [Description("Дубликаты участника")]
        DoubleParticipant = 24,
        [Description("Проверка повторных регистраций")]
        ReregistrationCheck = 25,
        [Description("Резервное копирование локальной БД")]
        LocalBackup = 26,
        [Description("Апелляции")]
        Appeals = 27,
        [Description("Бланки ЕГЭ")]
        [GiaDescription("Бланки ГИА")]
        Blanks = 28,
        [Description("Свидетельства ЕГЭ")]
        [GiaDescription("Свидетельства ГИА")]
        Certificates = 29,
        [Description("Результаты ЕГЭ")]
        [GiaDescription("Результаты ГИА")]
        Answers = 30,
        [Description("Планирование")]
        Planning = 31,
        [Description("Роли организаторов вне аудитории")]
        OrganizationRoles = 32,
        [Description("Teхнологический портал ЕГЭ")]
        [GiaDescription("Teхнологический портал ГИА")]
        TechnologyPortal = 33,
        [Description("Эксперты по МСУ")]
        ExpertsMoyo = 34,
        [Description("Эксперты по АТЕ")]
        ExpertsAte = 35,
        [Description("Настройки приложения")]
        AppSettings = 36,
        [Description("Журнал действия пользователя")]
        UserActionLog = 37,
        [Description("Результаты сочинения (изложения)")]
        AnswersOpus = 38,
        [Description("Бланки сочинения (изложения)")]
        BlanksOpus = 39
    }

    public enum DeleteType
    {
        [Description("Активен")]
        [XmlEnum("0")]
        OK = 0,
        [Description("Удален МСУ")]
        [XmlEnum("1")]
        MOYODelete = 1,
        [Description("Удален ОО")]
        [XmlEnum("2")]
        YODelete = 2,
        [Description("Удален Участник")]
        [XmlEnum("4")]
        ParticipantDelete = 4,
        [Description("Удален ППЭ")]
        [XmlEnum("5")]
        StationDelete = 5,
        [Description("Удалена Аудитория")]
        [XmlEnum("6")]
        AuditoriumDelete = 6,
        [Description("Удален Работник ППЭ")]
        [XmlEnum("7")]
        PPEWorkerDelete = 7,
        [Description("Удален ППОИ")]
        [XmlEnum("8")]
        PPOIDelete = 8,
        [Description("Удален АТЕ")]
        [XmlEnum("9")]
        ATEDelete = 9,
        [Description("Дубликат")]
        [XmlEnum("10")]
        Dublicate = 10,
        [Description("Удалено физически")]
        [XmlEnum("11")]
        PermanentlyDeleted = 11,
        [Description("Удалено при коррекции аудиторного фонда ППЭ")]
        [XmlEnum("12")]
        CorrectionDeleted = 12,
        [Description("Удален Эксперт")]
        [XmlEnum("13")]
        ExpertDelete = 13,

        [XmlEnum("30")]
        Tmp30 = 30,
        [XmlEnum("31")]
        Tmp31 = 31,
        [XmlEnum("32")]
        Tmp32 = 32,

        [Description("Удалено TestReader")]
        [XmlEnum("33")]
        TestReaderDelete = 33,

        [XmlEnum("34")]
        Tmp34 = 34,
        [XmlEnum("35")]
        Tmp35 = 35,
        [XmlEnum("36")]
        Tmp36 = 36,
        [XmlEnum("37")]
        Tmp37 = 37,
        [XmlEnum("38")]
        Tmp38 = 38,
        [XmlEnum("39")]
        Tmp39 = 39,
        [XmlEnum("40")]
        Tmp40 = 40
    }

    public enum ReceiverTypeEnum
    {
        [Description("МСУ")]
        MOYO,
        [Description("ОО")]
        YO
    }

    public enum JournalResultEnum
    {
        [Description("Успешно")]
        Successfully,
        [Description("Неуспешно")]
        Failed
    }

    public enum OldLicenseConditionEnum
    {
        [Description("Не распечатан")]
        Notprinted = 1,
        [Description("Предназначен к печати после коррекции на паспортные данные")]
        AfterPassCorection = 11,
        [Description("Предназначен к печати после переклейки результатов")]
        AfterResult = 12,
        [Description("Предназначен к печати после удовлетворенной апелляции")]
        AfterAppeal = 13,
        [Description("Свидетельство напечатано")]
        Printed = 55,
    }

    public enum AuditActionType
    {
        [Description("Создание")]
        Create = 0,
        [Description("Удаление")]
        Delete = 1,
        [Description("Удаление из БД")]
        AdminDelete = 2,
        [Description("Изменение")]
        Update = 3,
        [Description("Импорт-создание")]
        ImportCreate = 4,
        [Description("Импорт-изменение")]
        ImportUpdate = 5,
        [Description("Откат данных")]
        Rollback = 6,
    }

    public enum AuditObjectType
    {
        MOY = 0,
        RCOI = 1,
        // и так далее
    }

    public enum LocalPermissionSet
    {
        RCOI = 0,
        MOYO = 1,
        OY = 2
    }

    public enum StationWorkerType
    {
        None = 0,
        [Description("Прикрепленный к ППЭ")]
        WorkerOnStation = 1,
        [Description("Назначенный на экзамен")]
        WorkerOnExam = 2
    }

    public enum ParticipantExamType
    {
        [Description("Тип не определен")]
        None = 0,
        [Description("Назначен на 2 или более экзаменов в один день")]
        DoubleDate,
        [Description("Назначен на резервный день")]
        ReserveDate,
        [Description("И то и другое")]
        Both
    }

    public enum ReportExportType
    {
        [Description("Excel")]
        Excel = 0,
        [Description("PDF")]
        Pdf,
        [Description("RTF")]
        RTF
    }

    public enum ContentToolbarActions
    {
        Print,
        Xls,
        Search,
        FontDefault,
        FontIncrease,
        FontDecrease,
        Mass,
        GroupRegister,
        GroupDoubles,
        GroupReregisterCheck,
        GroupSelectAll,
        GroupDeselectAll,
        GroupSeparator,
        GroupDeleteAll,
        GroupMassRegistration,
        GroupMassStationCancel,
        GroupMassSectionPassPrint,
        [Description("Сочинение/Изложение")]
        GroupMassOpusPassPrint,
        GroupMassOpusSchools,
        GroupMassPassPrint,
        FindDouble,
        FindByCSV,
        ClearFilter,
        Paging,
        GroupCapacity,
        GroupMassAppointments,
        ParticipantAllSchools,
        BlockRegistry,
        FbsCheck,
        ParticipantsDoublesMergeCancel,
        GroupCancelExport,
        [Description("Массовое изменение ОО")]
        GroupMassChangeSchool,
        [Description("Массовая генерация кодов")]
        GroupGenerateParticipantCode,
        [Description("ГИА(ЕГЭ)")]
        GroupMassPassIssueDate,
        [Description("Сочинение/Изложение")]
        GroupMassOpusPassIssueDate,
#if GIA
        [Description("Допуск к ГИА-9")]
#else
        [Description("Допуск к ГИА")]
#endif
        GroupMassGiaAllowed
    }

    public enum SheetComplectType
    {
        [ExtendedDescription("Бланк Регистрации", "Рег")]
        R = 0,
        [ExtendedDescription("Бланк №1", "№1")]
        AB = 1,
        [ExtendedDescription("Бланк №2", "№2")]
        C = 2,
        [ExtendedDescription("Бланки записи", "Зап")]
        Opus = 3
    }

    public enum MinorCertReasonPrint
    {
        [Description("Блок снят")]
        Removed = 1,
        [Description("Печать отменена")]
        PrintCanceled = 3,
        [Description("Сознательный отказ от печати")]
        PrintDenied = 4,
        [Description("Печать подтверждена")]
        PrintApproved = 55,
        [Description("Напечатано и подтверждено")]
        BillCreated = 77,
        [Description("Не напечатано или не подтверждено")]
        NotBillCreated
    }

    public enum ParticipantPropopertyType
    {
        [Description("Участник заблокирован"), XmlEnum("0")]
        Blocked = 0,
        [Description("Дата блокировки"), XmlEnum("1")]
        BlockedDate = 1,
        [Description("Основание блокировки"), XmlEnum("2")]
        BlockedReason = 2,
        [Obsolete("Не используется, код регистрации перенесен в RbdParticipants.ParticipantCode", true)]
        [Description("Код регистрации"), XmlEnum("3")]
        AuthCode = 3,

#if GIA
        [Description("Допуск к ГИА-9"), XmlEnum("4")]
#else 
        [Description("Допуск к ГИА"), XmlEnum("4")]
#endif
        GiaAllowed = 4,
#if GIA
        [Description("Основное общее образование получено в иностранном государстве"), XmlEnum("5")]
#else
        [Description("Среднее общее образование получено в иностранном государстве"), XmlEnum("5")]
#endif
        FormationForeignState = 5,
        [Description("Проходит обучение в организации закрытого типа"), XmlEnum("6")]
        ClosedEducation = 6,
        [Description("Участник с ОВЗ"), XmlEnum("7")]
        Disability = 7,
        [Description("Участник явлется беженцем или переселенцем"), XmlEnum("8")]
        Refugee = 8,
        [Description("СНИЛС"), XmlEnum("9")]
        Snils = 9
    }

    /// <summary>
    /// 
    /// </summary>
    public enum Triplet
    {
        [Description("Не задано")]
        NotInitialize = 0,
        [Description("Да")]
        Yes = 1,
        [Description("Нет")]
        No = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ParticipantSearchTriplet
    {
        [Description("")]
        NotInitialize = 0,
        [Description("Да")]
        Yes = 1,
        [Description("Нет")]
        No = 2
    }

    /// <summary>
    /// Используется на форме поиска ОО
    /// </summary>
    public enum Sign
    {
        [Description("=")]
        Eq = 0,
        [Description(">")]
        Gt = 1,
        [Description("<")]
        Lt = 2
    }

    /// <summary>
    /// Должность сотрудника ППЭ
    /// </summary>
    public enum WorkerPositionCode
    {
        /// <summary>
        /// Руководитель ППЭ
        /// </summary>
        [Description("Руководитель ППЭ")]
        StationManager = 1,
        /// <summary>
        /// Организатор в аудитории ППЭ
        /// </summary>
        [Description("Организатор в аудитории ППЭ")]
        OrganizerInAuditorium = 2,
        /// <summary>
        /// Организатор вне аудитории ППЭ
        /// </summary>
        [Description("Организатор вне аудитории ППЭ")]
        OrganizerOutAuditorium = 3,
        /// <summary>
        /// Ассистент
        /// </summary>
        [Description("Ассистент")]
        Assistant = 4,
        /// <summary>
        /// Член ГЭК
        /// </summary>
        [Description("Член ГЭК")]
        AuthorizedGek = 5,
        /// <summary>
        /// Общественный наблюдатель
        /// </summary>
        [Description("Общественный наблюдатель")]
        PublicObserver = 6,
        /// <summary>
        /// Технический специалист ППЭ
        /// </summary>
        [Description("Технический специалист ППЭ")]
        TechnicalSpecialist = 7,
        /// <summary>
        /// Спец. по инструктажу и лаб.раб.
        /// </summary>
        [Description("Спец. по инструктажу и лаб.раб.")]
        Laborant = 8,
        /// <summary>
        /// Экзаменатор собеседник
        /// </summary>
        [Description("Экзаменатор-собеседник")]
        Interlocutor = 9
    }

    /// <summary>
    /// Роли работников ППЭи назначения работников
    /// 
    /// </summary>
    public enum WorkerRoleCode
    {
        [Description("Федеральный общественный наблюдатель")]
        FederalPublicObserver = 1,
        [Description("Региональный общественный наблюдатель")]
        RegionPublicObserver = 2,
        [Description("Федеральный член ГЭК")]
        FederalAuthorizedGek = 3,
        [Description("Региональный член ГЭК")]
        RegionAuthorizedGek = 4,
        [Description("Организатор в аудитории подготовки")]
        OrganizerInPrepareAuditorium = 5,
        [Description("Организатор в аудитории проведения")]
        OrganizerInVoiceAuditorium = 6,
        [Description("Оператор ПК в аудитории проведения")] 
        OperatorPcInVoiceAuditorium = 7
    }

    /// <summary>Состояние уведомлений участников</summary>
    [Flags]
    public enum ExamPassType
    {
        /// <summary>Актуальное уведомление</summary>
        [Description("Актуальное уведомление"), XmlEnum("0")]
        Actual = 0,
        /// <summary>Изменено распределение в ППЭ</summary>
        [Description("Изменено распределение в ППЭ"), XmlEnum("1")]
        ParticExamOnStationChanged = 1,
        /// <summary>Изменены данные участника</summary>
        [Description("Изменены данные участника"), XmlEnum("2")]
        ParticipantInfoChanged = 2,
        /// <summary>Изменены данные о ППЭ</summary>
        [Description("Изменены данные о ППЭ"), XmlEnum("4")]
        StationInfoChanged = 4,
        /// <summary>Уведомление выдано</summary>
        [Description("Уведомление выдано"), XmlEnum("8")]
        IssueDate = 8
    }

    [Flags]
    public enum LockType
    {
        [Description("Блокировки отсутствуют")]
        None = 0,
        [Description("Наличие рассадки")]
        Seating = 1,
        [Description("Наличие экспорта в ППЭ")]
        ExportPpe = 2,
        [Description("Наличие бланков регистрации")]
        Blanks = 4,
        [Description("Наличие прошедших экзаменов по сочинению(изложению)")]
        Opus = 8,
        [Description("Наличие назначения работника на экзамен с указанным предметом")]
        Appointment2Subject = 16,
        [Description("Назначение на экзамен")]
        AppointmentOnExam = 32
    }

    public enum ParticipantCertType
    {
        [Description("РБД")]
        [GiaDescription("ГИА")]
        Rbd,
        [Description("ФИС \"Результаты ЕГЭ\"")]
        Fbs
    }

    public enum Subjects
    {
        [Description("Химия")]
        Chemical = 4,
        [Description("Физика")]
        Physics = 3,
        [Description("Информатика")]
        Informatics = 5
    }

    public enum Waves
    {
        [Description("Досрочный февральский")]
        Trial = 0,
        [Description("Досрочный этап")]
        Preschedule = 1,
        [Description("Основной этап")]
        Common = 2,
        [Description("Дополнительный этап")]
        Additional = 3
    }

    public enum AutoGeneralRule
    {
        [Description("Запись, которая не может быть дублем")]
        CanNotBeDouble,
        [Description("С максимальным количеством распределений в ППЭ")]
        MaxExamOnStation,
        [Description("С максимальным количеством имеющихся дублей")]
        MaxDoubles,
        [Description("С самой ранней датой регистрации")]
        MinDateCreate
    }

    public enum FindDoublesMethod
    {
        [Description("ФИО, номер и серия документа")]
        FioAndDocumentNumber,
        [Description("Тип, номер и серия документа")]
        DocumentTypeAndNumber,
        [Obsolete]
        [Description("ФИО и код участника")]
        FioAndCode,
        [Description("ФИО и дата рождения участника")]
        FioAndBirhday
    }

    public enum RequisiteTypes
    {
        PpeAct,
        WorkerAct,
        [Description("Сведения о результатах государственной итоговой аттестации")]
        [GiaDescription("Сведения о результатах государственной итоговой аттестации 9-х классов")]
        GiaResultDetails
    }

    public enum PEActionType
    {
        [Description("Снят")]
        Deleted = 0,
        [Description("Зарегистрирован")]
        Registered = 1,
        [Description("Заблокирован")]
        Blocked = 2,
        [Description("От дубликата")]
        AddedByClone = 3
    }

    [Flags]
    public enum ExamForm
    {
        [Description("Стандартная"), XmlEnum("0")]
        Standard = 0,
        [Description("Компьютерная"), XmlEnum("1")]
        Computer = 1,
        [Description("Проведение экзамена в устной форме"), XmlEnum("2")]
        ExecuteVerbal = 2,
        [Description("Подготовка экзамена в устной форме"), XmlEnum("4")]
        PreparationVerbal = 4
    }

    public enum GiaForm
    {
        [Description("Стандартная")]
        Standard,
        [Description("Устная")]
        Verbal
    }

    public enum ChemicalLabSettingValue
    {
        [Description("Стандартное проведение экзамена по Химии")]
        StandartExam = 0,
        [Description("Выполнение реального химического эксперимента на экзамене по Химии")]
        RealExam = 1
    }

    [Flags]
    public enum SchoolFlags
    {
        None = 0,
        [Description("Спец пункт регистрации"), XmlEnum("1")]
        RegistrationSpecialPoint = 1,
        [Description("Спец пункт сдачи сочинения (изложения)"), XmlEnum("2")]
        OpusExecuteSpecialPoint = 2,
        [Description("ОО на дому"), XmlEnum("4")]
        IsHome = 4
    }

    [Flags]
    public enum StationFlags
    {
        [Description("ППЭ на дому"), XmlEnum("1")]
        HomePPE = 1,
        [Description("НЦП"), XmlEnum("2")]
        Ncp = 2,
        [Description("ЕГЭ"), XmlEnum("4")]
        Ege = 4,
        [Description("ГВЭ"), XmlEnum("8")]
        Gve = 8,
        [Description ("Родной язык/литература"), XmlEnum("16")]
        NationalLanguageAndLiterature = 16,
        [Description("Печать КИМ"), XmlEnum("32")]
        PrintKim = 32,
        [Description("Отдаленное ППЭ"), XmlEnum("64")]
        FarawayPPE = 64,
        [Description("Резервное ППЭ"), XmlEnum("128")]
        BackingPPE = 128
    }

    [Flags]
    [Description("Тип места")]
    public enum PlaceType
    {
        [Description("Стандартное")]
        Standard = 0,
        [Description("Место пригодное для экзамена в устной форме"), XmlEnum("1")]
        Verbal = 1
    }

    public enum DistribType
    {
        [Description("Неизвестность")]
        Unknown,
        [Description("ГИА(ЕГЭ)")]
        Ege,
        [Description("ГИА-9")]
        Gia9,
        [Description("Апробация")]
        Uege2015App
    }

    [Description("Наличие сертификата")]
    public enum HaveCert
    {
        [Description("Есть"), XmlEnum("0")]
        Have = 0,
        [Description("Нет"), XmlEnum("1")]
        Havent = 1
    }
}