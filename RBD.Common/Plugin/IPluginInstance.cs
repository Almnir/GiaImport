using System.Windows.Forms;

namespace RBD.Common.Plugin
{
    public interface IPluginInstance
    {
        void Show(IWin32Window owner);
        ExecutionResultMethod ExecuteQuery { get; set; }
    }

    public delegate object ExecutionResultMethod(ExecuteQueryArg arg);
}