using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace GiaImport
{
    public class CustomDateTimeReader : XmlTextReader
    {
        public CustomDateTimeReader(TextReader input) : base(input) { }

        public override string ReadElementString()
        {
            string data = base.ReadElementString();
            DateTime dt;

            if (DateTime.TryParse(data, null, DateTimeStyles.AdjustToUniversal, out dt))
            {
                var ret = dt.ToString("o");
                return ret;
            }
            else
                return data;
        }
    }
}
