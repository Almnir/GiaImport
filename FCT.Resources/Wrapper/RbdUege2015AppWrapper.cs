using System;
using System.Resources;
using RBD.Resources.Properties;

namespace RBD.Resources.Wrapper
{
    public class RbdUege2015AppWrapper : AbstractWrapper, IWrapper
    {
        protected override ResourceManager Manager()
        {
            return RbdResource.ResourceManager;
        }

        public string SkinName
        {
            get { return "Office 2007 Pink"; }
        }

        public string MainFormPlanTab
        {
            get { return "Планирование ГИА"; }
        }

        void IWrapper.MainFormSetupMenu()
        {
            MainFormSetupMenuEge();
        }

        public bool CheckLicense(Func<bool> validFunction)
        {
            return true;
        }
    }
}