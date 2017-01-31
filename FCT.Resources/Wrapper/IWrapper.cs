using System;

namespace RBD.Resources.Wrapper
{
    public interface IWrapper
    {
        object Icon();
        string AplicationName { get; }
        string SkinName { get; }
        string MainFormPlanTab{ get;}
        void MainFormSetupMenu();
        bool CheckLicense(Func<bool> validFunction);
        string GEK { get; }
        string AplicationShortName { get;  }

        event EventHandler OnMainFormSetupMenuGia;
        event EventHandler OnMainFormSetupMenuEge;
    }
}