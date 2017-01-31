using System;
using System.ComponentModel;
using RBD.Common.Attributes;

namespace RBD.Client.Common.Enums
{
    public enum SecureObjectType
    {
        [Description("Федеральные справочники")]
        FederalDictionaries = 0,
        [Description("ОИВ, ГЭК, РЦОИ")]
        [GiaDescription("ОИВ, ГЭК(РЭК), РЦОИ")]
        RCOI = 1,
        [Description("АТЕ")]
        ATE = 2,
        [Description("МСУ")]
        OYO = 3,
        [Description("ОО")]
        OY = 4,
        [Description("Участники")]
        Participants = 5,
        [Description("ППЭ")]
        PPE = 6,
        [Description("Аудитории ППЭ")]
        PPERooms = 7,
        [Description("Работники ППЭ")]
        PPEWorkers = 8,
        [Description("Эксперты")]
        Experts = 9,
        [Description("ППОИ")]
        PPOI = 10,
        [Description("Импорт")]
        Import = 11,
        [Description("Экспорт")]
        Export = 12,
        [Description("Пользователи")]
        Users = 13,
        [Description("Роли")]
        SecurityRules = 14,
        [Description("Удаленные объекты")]
        DeletedObjects = 15,
        [Description("Журнал действий пользователя")]
        Audit = 16,
        [Description("Роли организаторов вне аудитории")]
        OrganizationRoles = 17,
        /* Разделы планирования > 100 для разделения в ролях */
        [Description("Назначение ППЭ на экзамены")]
        PpeAppointment = 101,
        [Description("Назначение аудиторий ППЭ на экзамены")]
        AuditoriumsAppointment = 102,
        [Description("Распределение участников по ППЭ")]
        ParticipantsPpeAppointment = 103,
        [Description("Назначение работников ППЭ")]
        WorkersAppointment = 104,
        [Description("Рассадка участников и организаторов в ППЭ")]
        ParticipantsSeating = 105,
        [Description("Назначение экспертов на экзамены")]
        ExpertAppointment = 106
    }

    [Flags]
    public enum Permissions
    {
        None = 0,
        View = 1,
        Create = 2,
        UpdateExceptCode = 4,
        Update = 8,
        Delete = 16,
        AllExceptCodeEditing = View | Create | Delete | Update,
        All = View | Create | Delete | UpdateExceptCode | Update
    }

    public enum AddressTypeCode
    {
        [Description("Фактический адрес")]
        Fact = 1,
        [Description("Юридический адрес")]
        Law
    }

    public enum CodeRangeOwner
    {
        Unmapped,
        Rcoi,
        Msu,
        Oo,
        PrintingHouse
    }

    public enum CodeRangeType
    {
        [Description("Код работы")]
        OpusBlanks = 0,
        [Description("Код работы")]
        GveBlanks = 13
    }
}
