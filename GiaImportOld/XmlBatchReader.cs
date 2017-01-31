using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using FCT.Client.Dto.Interfaces;

namespace RBD.Client.Services.Import.Bulk
{
    public class XmlBatchReader<TDto> where TDto : DtoBase, new()
    {
        private int _batchSize;
        public XmlBatchReader(int batchSize)
        {
            _batchSize = batchSize;
        }

        private readonly XNamespace _xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
        private readonly XNamespace _xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");

        /// <summary>
        /// Последовательно читаем пачками данные из файла в XDocument и выполняем над ними action
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="action"></param>
        public void ProcessFileWithAction(string filePath, Action<XDocument> action)
        {
            var settings = new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true };
            using (var reader = XmlReader.Create(filePath, settings))
            {
                reader.MoveToContent();

                var root = new XElement(reader.Name, new []
                    {
                        new XAttribute(XNamespace.Xmlns + "xsi", _xsi.NamespaceName),
                        new XAttribute(XNamespace.Xmlns + "xsd", _xsd.NamespaceName)
                    });

                var batchOfXml = new XDocument(root);

                /* Грузим пачками в БД */
                //if (typeof (TDto) == typeof (StationWorkersDto)) _batchSize = 1;
                foreach (var batch in ReadBatch(reader, typeof(TDto).Name, _batchSize))
                {
                    root.Add(batch);
                    action(batchOfXml);
                    root.RemoveAll();
                }
            }            
        }

        /// <summary>
        /// Последовательно читает файл и набирает пачку элементов размера batchSize
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tagName"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        private static IEnumerable<List<XElement>> ReadBatch(XmlReader reader, string tagName, int batchSize)
        {
            var elements = new List<XElement>(batchSize);
            bool nodesExists = reader.ReadToDescendant(tagName);
            while (nodesExists)
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == tagName)
                {
                    elements.Add(XNode.ReadFrom(reader) as XElement);
                    if (elements.Count == batchSize)
                    {
                        yield return elements;
                        elements.Clear();
                    }

                    if (!reader.IsStartElement(tagName))
                        nodesExists = reader.ReadToNextSibling(tagName);
                }
            }

            if (elements.Count > 0)
                yield return elements;
        }
    }
}
