using System;
using System.Resources;

namespace RBD.Resources.Wrapper
{
    public class GiaWrapper : AbstractWrapper, IWrapper
    {
        protected override ResourceManager Manager()
        {
            return Properties.GiaResource.ResourceManager;
        }

        public string SkinName
        {
            get { return "gia"; }
        }

        public string MainFormPlanTab
        {
            get { return "Планирование ГИА-9"; }
        }

        void IWrapper.MainFormSetupMenu()
        {
            MainFormSetupMenuGia();
        }

        public bool CheckLicense(Func<bool> validFunction)
        {
            return true;
            //return validFunction();
        }
    }
}