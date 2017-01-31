using System;
using System.ComponentModel;

namespace RBD.Common.Enums
{
    [Flags]
    public enum ExclusionType
    {
        None = 0,

        [Description("Исключен")] Excluded = 1,

        [Description("Исключен как дочерний элемент")] ExcludedByParent = 2,
        [Description("Исключен по типу")] ExcludedByType = 4,
        [Description("Исключен по исключенному экзамену")] ExcludedByExams = 8,
        [Description("Исключен по ППЭ")] ExcludedByStation = 16,
        [Description("Исключен по работнику")] ExcludedByWorker = 32,
        [Description("Исключен по участнику")] ExcludedByParticipant = 64,
        [Description("Исключен по школе")] ExcludedBySchool = 128,
        [Description("Исключен по МСУ")] ExcludedByGovernment = 256,
        [Description("Исключен по эксперту")] ExcludedByExpert = 512,
        [Description("Исключено по назн. аудитории на экз.")] ExcludedByStationExamAuditory = 1024,
        [Description("Обязательно к загрузке")] MustBeLoad = 2048,
        [Description("Исключен по блокировке")] Locked = 4096,

        ExcludedRelationByExam = Excluded | ExcludedByExams,
        ExcludedParentByType = Excluded | ExcludedByType,

        ExcludedRelationByParent = Excluded | ExcludedByParent,

        ExcludedRelationByTypeAndParent = Excluded | ExcludedByParent | ExcludedByType,

        BreakParentExclusions = Excluded | ExcludedByParent | ExcludedByType |
            ExcludedByStation | ExcludedByWorker | ExcludedByParticipant | ExcludedBySchool |
            ExcludedByGovernment | ExcludedByExpert | ExcludedByStationExamAuditory,

        BreakSborExclusions = Excluded | ExcludedByParent |
            ExcludedByStation | ExcludedByWorker | ExcludedByParticipant | ExcludedBySchool |
            ExcludedByGovernment | ExcludedByExpert,

        BreakObjectTypesExclusions = BreakParentExclusions | MustBeLoad,
        BreakExamsExclusions = BreakObjectTypesExclusions | ExcludedRelationByExam
    }

    [Flags]
    public enum DeletionType
    {
        None = 0,
        [Description("Удалено по удаленной связи")] Relation = 1,
        [Description("Удалено по назн. ППЭ на экз.")] StationExam = 2,
        [Description("Удалено по экзамену участника")] ParticipantExam = 4,
        [Description("Удалено по назн. аудитории на экз.")] StationExamAuditory = 8,
        [Description("Удалено по ППЭ")] Station = 16,
        [Description("Удалено по работнику")] Worker = 32,
        [Description("Удалено по участнику")] Participant = 64,
        [Description("Удалено по школе")] School = 128,
        [Description("Удалено по МСУ")] Government = 256,
        [Description("Удалено по эксперту")] Expert = 512,
        [Description("Удалено по работнику назн. в ППЭ")] WorkerOnStation = 1024,
        [Description("Удалено по АТЕ")] Area = 2048,
        [Description("Удалено по ППОИ")] PCenter = 4096,
        [Description("Удалено по работнику назн. на экз.")] WorkerOnExam = 8192,
        [Description("Удалено по участнику назн. на экз.")] ParticipantExamOnStation = 16384,
        [Description("Удалено по аудитории")] Auditorium = 32768
    }

    [Flags]
    public enum CrossmunitipalType
    {
        None = 0,
        [Description("Назначение участников в сторонние ППЭ")] ParticipantsExams = 1,
        //[Description("Межмуниципальное планирование в ППЭ")] StationsExams = 2,
        [Description("Назначение работников в сторонние ППЭ")] Workers = 4
    }
}
