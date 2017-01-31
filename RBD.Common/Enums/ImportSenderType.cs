using System.ComponentModel;

namespace RBD.Common.Enums
{
    /// <summary>
    /// Источники импорта
    /// </summary>
    public enum ImportSenderType
    {
        [Description("Ключ")] KEY,
        [Description("МСУ")] MOYO,
        [Description("ОО")] OY,
        [Description("РЦОИ")] RCOI,
        [Description("CSV")] CSV,
        [Description("ППЭ")] PPE,
        
        /// <summary>
        /// Коллектор данных ГИА из сторонних источников
        /// </summary>
        [Description("XML")] GiaDataCollect,

        Export
    }
}
