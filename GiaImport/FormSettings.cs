using System;
using System.Configuration;

namespace GiaImport
{
    public class FormSettings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        public String ServerText
        {
            get { return (String)this["ServerText"]; }
            set { this["ServerText"] = value; }
        }

        [UserScopedSettingAttribute()]
        public String DatabaseText
        {
            get { return (String)(this["DatabaseText"]); }
            set { this["DatabaseText"] = value; }
        }
        [UserScopedSettingAttribute()]
        public String LoginText
        {
            get { return (String)(this["LoginText"]); }
            set { this["LoginText"] = value; }
        }
        [UserScopedSettingAttribute()]
        public String PasswordText
        {
            get { return (String)(this["PasswordText"]); }
            set { this["PasswordText"] = value; }
        }
    }

}
