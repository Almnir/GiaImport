using System;
using System.Globalization;
using System.Resources;

namespace RBD.Resources.Wrapper
{
    public abstract class AbstractWrapper : AbstractMainFormWrapper
    {
        protected abstract ResourceManager Manager();

        public object Icon()
        {
            return Manager().GetObject("Icon");
        }

        public string AplicationName
        {
            get 
            {
                return Manager().GetString("AplicationName");
            }
        }
        public string AplicationShortName
        {
            get
            {
                return Manager().GetString("AplicationShortName");
            }
        }

        public string GEK
        {
            get
            {
                return Manager().GetString("GEK");
            }
        }
    }

    public abstract class AbstractMainFormWrapper
    {
        protected void MainFormSetupMenuGia()
        {
            if (OnMainFormSetupMenuGia != null)
            {
                OnMainFormSetupMenuGia(this, new EventArgs());
            }
        }

        protected void MainFormSetupMenuEge()
        {
            if (OnMainFormSetupMenuEge != null)
            {
                OnMainFormSetupMenuEge(this, new EventArgs());
            }
        }

        public event EventHandler OnMainFormSetupMenuGia;

        public event EventHandler OnMainFormSetupMenuEge;
    }
}