using System.ComponentModel;

namespace RBD.Common.Enums
{
    public enum SourceType
    {
        [Description("Объект из БД")] FromDb,
        [Description("Объект создан автоматически")] AutoCreated,
        [Description("Объект импорта")] Import
    }
}
