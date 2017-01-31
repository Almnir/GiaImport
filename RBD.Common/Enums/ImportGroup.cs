using System.ComponentModel;

namespace RBD.Common.Enums
{
    public enum ImportGroup
    {
        [Description("Справочник")] Dictionary,
        [Description("Объект сбора")] Sbor,
        [Description("Связка сбора")] SborLeaf,
        [Description("Объект планирования")] Planning,
        Undefined
    }
}
