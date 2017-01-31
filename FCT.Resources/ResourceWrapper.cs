using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace RBD.Resources
{
    public class ResourceWrapper
    {
        public static byte[] GetLocalDataBaseRar()
        {
            return Properties.Resources.ResourceManager.GetObject("db") as byte[];
        }

		public static byte[] GetBootStrapper()
		{
			return Properties.Resources.ResourceManager.GetObject("rbdbootstrapper") as byte[];
		}

        public static string GetReportResource(string reportName)
        {
            reportName = reportName.Replace("-", "_");
            var obj = Properties.Resources.ResourceManager.GetObject(reportName) as byte [];
            return  Encoding.UTF8.GetString(obj).Trim();
        }

        public static byte[] GetSettingsResource(string settingsName)
        {
            return Properties.Resources.ResourceManager.GetObject(settingsName) as byte [];
        }

        public static byte[] GetTiffInDyte(string settingsName)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(Properties.Resources.ResourceManager.GetObject(settingsName), typeof(byte[]));
        }

        public static XmlReader GetReporterLangXmlReader()
        {
            var reader = new XmlTextReader(new StringReader(Properties.Resources.ReporertLangRussian));
            return reader;
        }

        public static string GetReporterRegKey()
        {
            return Encoding.ASCII.GetString(Properties.Resources.ShartpshooterExpressLicense);
        }
    }
}
 