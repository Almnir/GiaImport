using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBD.Common.Plugin
{
    public interface IPluginSetting
    {
        /// <summary>
        /// Глобальные настройки
        /// </summary>
        IPluginInstanceVersion PluginVersion { get; }

        /// <summary>
        /// Настройки меню
        /// </summary>
        IPluginMenu Menu { get; }

        /// <summary>
        /// Настройки окна вызова
        /// </summary>
        IPluginInstance Instance { get; }

        /// <summary>
        /// Настройки окна вызова
        /// </summary>
        bool Enable { get; }

        event Action<string> OnWriteLogError;
        void WriteLog(string message);
    }
}
