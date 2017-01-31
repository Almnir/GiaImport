namespace RBD.Common.Plugin
{
    public interface IPluginMenu
    {
        /// <summary>
        /// В какой группе меню размещать
        /// </summary>
        PluginMenu MenuGroup { get; }

        PluginSubMenu SubMenuGroup { get; }

        /// <summary>
        /// Позиция в группе
        /// </summary>
        int MenuPosition { get; }

        /// <summary>
        /// Позиция в группе
        /// </summary>
        string MenuText { get; }
    }
}