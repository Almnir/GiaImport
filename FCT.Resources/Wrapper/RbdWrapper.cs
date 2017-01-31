using System;
using System.Globalization;
using System.Resources;
using RBD.Resources.Properties;
using RBD.Resources.Wrapper;

namespace RBD.Resources.Wrapper
{
    public class RbdWrapper : AbstractWrapper, IWrapper
    {
        protected override ResourceManager Manager()
        {
            return RbdResource.ResourceManager;
        }

        public string SkinName
        {
            get { return "Caramel"; }
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

    public class ShareWrapper : AbstractWrapper, IWrapper
    {
        protected override ResourceManager Manager()
        {
            return Properties.Resources.ResourceManager;
        }

        public T GetResorses<T>(string objectName)
        {
            var obj = Manager().GetObject(objectName, CultureInfo.CurrentCulture);
            return (T)obj;
        }

        public string SkinName
        {
            get { return ""; }
        }

        public string MainFormPlanTab
        {
            get { return ""; }
        }

        public void MainFormSetupMenu()
        {
           
        }

        public bool CheckLicense(Func<bool> validFunction)
        {
            return false;
        }
    }
}