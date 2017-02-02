using DataModels;
using linq2dbpro.DataModels;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GiaImport
{
    class MainStage
    {
        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                string xml = string.Empty;
                using (FileStream fs = new FileStream(XmlFilename, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                    {
                        xml = sr.ReadToEnd();
                    }
                }

                using (StringReader stringReader = new StringReader(xml))
                using (var xmlReader = new CustomDateTimeReader(stringReader))
                {
                    var ser = new XmlSerializer(typeof(T));
                    returnObject = (T)ser.Deserialize(xmlReader);
                }
            }
            catch (Exception ex)
            {
                throw new DeserializeException(ex.ToString());
            }
            return returnObject;
        }


        private static void BulkLoad_rbd_Address(IEnumerable<rbd_AddressSet> obj)
        {
            try
            {
                using (GIA_DB db = new GIA_DB())
                {
                    using (DataConnection dc = new DataConnection(ProviderName.SqlServer, "Data Source=YARNYKH;Initial Catalog=ftc_test;Integrated Security=True;"))
                    {

                        var sp = db.DataProvider.GetSchemaProvider();
                        var dbSchema = sp.GetSchema(db);
                        if (!dbSchema.Tables.Any(t => t.TableName == "rbd_Address"))
                        {
                            db.CreateTable<rbd_Address>();
                        }
                        //db.DataProvider.GetSchemaProvider();
                        BulkCopyOptions bco = new BulkCopyOptions();
                        bco.BulkCopyType = BulkCopyType.Default;
                        bco.MaxBatchSize = 1000;
                        bco.RowsCopiedCallback += Handler;
                        bco.NotifyAfter = 1;
                        db.DataProvider.BulkCopy(dc, bco, obj);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new MyBulkException(ex.ToString());
            }
        }

        private static void Handler(BulkCopyRowsCopied obj)
        {
            
        }
    }
}
