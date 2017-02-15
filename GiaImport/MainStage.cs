using NLog;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Serialization;

namespace GiaImport
{
    class MainStage
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static ConcurrentDictionary<string, Tuple<string, long, TimeSpan>> errorDict = new ConcurrentDictionary<string, Tuple<string, long, TimeSpan>>();

        public static T DeserializeXMLFileToObject<T>(string xmlFileName)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(xmlFileName)) return default(T);

            try
            {
                using (var ms = new MemoryStream(File.ReadAllBytes(xmlFileName)))
                {
                    var ser = new XmlSerializer(typeof(T));
                    returnObject = (T)ser.Deserialize(ms);
                }
            }
            catch (Exception ex)
            {
                string status = string.Format(string.Format("При десериализации файла {0} произошла ошибка: {1}.", xmlFileName, ex.ToString()));
                if (!errorDict.ContainsKey(xmlFileName))
                {
                    errorDict.TryAdd(xmlFileName, new Tuple<string, long, TimeSpan>(status, 0, TimeSpan.Zero));
                }
                log.Error(status);
            }
            return returnObject;
        }

        //public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        //{
        //    T returnObject = default(T);
        //    if (string.IsNullOrEmpty(XmlFilename)) return default(T);

        //    try
        //    {
        //        string xml = string.Empty;
        //        using (FileStream fs = new FileStream(XmlFilename, FileMode.Open, FileAccess.Read))
        //        {
        //            using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
        //            {
        //                xml = sr.ReadToEnd();
        //            }
        //        }

        //        using (StringReader stringReader = new StringReader(xml))
        //        using (var xmlReader = new CustomDateTimeReader(stringReader))
        //        {
        //            var ser = new XmlSerializer(typeof(T));
        //            returnObject = (T)ser.Deserialize(xmlReader);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DeserializeException(string.Format("При десериализации файла {0} произошла ошибка: {1}.", XmlFilename, ex.ToString()));
        //    }
        //    return returnObject;
        //}
    }
}
