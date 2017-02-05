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

            if (data.Equals("NULL"))
            {
                return DBNull.Value.ToString();
            }
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
