using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using RBD.Client.Interfaces;

namespace RBD.Client.Services
{
    public class Serializer : ISerializer
    {
        /// <summary>
        /// Сериализация с шифрованием MD5
        /// </summary>
        /// <typeparam name="T">Тип сериализуемого объекта (можно не указывать)</typeparam>
        /// <param name="filePath">Путь к результирующему файлу</param>
        /// <param name="data">Данные для сериализации</param>
        public void Serialize<T>(string filePath, T data) where T : class
        {
            Serialize<T>(filePath, data, true);
        }

        public void Serialize<T>(string filePath, T data, bool isEncryptionUsed) where T : class
        {
            if (data == null)
                return;

            var serializer = new XmlSerializer(typeof(T));
            using (var file = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(file, data);
            }

            /*
             * Шифруем и сохраняем
             */
            var buffer = File.ReadAllBytes(filePath);
            
            if (isEncryptionUsed)
                buffer = buffer.Encrypt();

            File.WriteAllBytes(filePath, buffer);
        }

        public byte[] Serialize<T>(T data) where T : class
        {
            if (data == null)
                return null;

            var serializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, data);
                return ms.GetBuffer().Encrypt();
            }
        }

        public string Serialize<T>(T[] dtos) where T:class 
        {
            using (var ms = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(typeof(T[]));
                xmlSerializer.Serialize(ms, dtos);
                byte[] buffer = ms.GetBuffer();
                string stringResult = Encoding.UTF8.GetString(buffer);
                var doc = new XmlDocument();
                doc.LoadXml(stringResult);
                return doc.InnerXml;
            }
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public T Deserialize<T>(string filePath) where T : class
        {
            return Deserialize<T>(filePath, true);
        }

        public T Deserialize<T>(string filePath, bool isEncryptionUsed) where T : class
        {
            if (!File.Exists(filePath))
            {
                var msg = "Десереализация невозможна! Файл '{0}' не найден!";
                //Logger.GetLogger().WarnFormat(msg, filePath);
                throw new FileNotFoundException(string.Format(msg, filePath));
            }

            var bytes = File.ReadAllBytes(filePath);
            if (bytes.Length == 0)
            {
                //Logger.GetLogger().WarnFormat("Десириализация пустого потока в типе {0}", typeof (T).FullName);
                return null;
            }

            /*
             * Прочитали и расшифровали
             */
            if (isEncryptionUsed)
                bytes = bytes.Decrypt();

            using (var ms = new MemoryStream(bytes))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(ms) as T;
            }
        }

        /// <summary>
        /// Десериализация из Base64 строки
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public T DeserializeFromText<T>(string text) where T : class
        {
            using (var ms = new MemoryStream(text.DecryptAsBase64()))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(ms) as T;
            }
        }

        public T Deserialize<T>(byte[] bytes) where T : class
        {
            using (MemoryStream ms = new MemoryStream(bytes.Decrypt()))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(ms) as T;
            }
        }

        public object Deserialize(MemoryStream ms, Type arrayType)
        {
            var serializer = new XmlSerializer(arrayType);
            return serializer.Deserialize(ms);
        }
    }
}