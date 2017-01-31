using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using RBD.Client.Services.Import.Common.Entities.ImportEntities;

namespace RBD.Client.Services.Import.Common
{
    public static class Extensions
    {
        private static readonly Regex isGuid =
            new Regex(@"^({)?([0-9a-fA-F]){8}(-([0-9a-fA-F]){4}){3}-([0-9a-fA-F]){12}(?(1)})$", RegexOptions.Compiled);

        public static Guid? GetNullableGuid(this string text, string name, ref IImportEntity entity, int line)
        {
            if (string.IsNullOrEmpty(text)) 
                return null;

            try
            {
                text = text.Replace(" ", "");
                if (!isGuid.IsMatch(text))
                {
                    text = text.Replace("-", "").Replace("_", "").Replace("{", "").Replace("}", "");
                    if (text.Length == 32)
                    {
                        text = text.Insert(8, "-");
                        text = text.Insert(13, "-");
                        text = text.Insert(18, "-");
                        text = text.Insert(23, "-");
                    }
                }
                
                var guid = new Guid(text);
                if (guid == Guid.Empty)
                {
                    entity.AddCsvError("Не удалось извлечь " + name + ". Нулевой идентификатор", line);
                    return Guid.Empty;
                }

                return guid;
            }
            catch (Exception)
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return null;
            }
        }

        public static int? GetNullableInt32(this string text, string name, ref IImportEntity entity, int line)
        {
            if (string.IsNullOrEmpty(text)) 
                return null;

            int stub;
            if (!Int32.TryParse(text, out stub))
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return null;
            }

            return stub;
        }

        public static long? GetInt64(this string text, string name, ref IImportEntity entity, int line)
        {
            if (string.IsNullOrEmpty(text)) 
                return null;

            long stub;
            if (!long.TryParse(text, out stub))
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return null;
            }
            
            return stub;
        }

        public static short? GetNullableShort(this string text, string name, ref IImportEntity entity, int line)
        {
            if (string.IsNullOrEmpty(text)) 
                return null;

            short stub;
            if (!short.TryParse(text, out stub))
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return null;
            }
           
            return stub;
        }

        public static bool? GetNullableBoolean(this string text, string name, ref IImportEntity entity, int line)
        {
            text = text.Trim();

            if (string.IsNullOrEmpty(text))
                return null;

            if (text == "0" || text == "1")
                return text == "0" ? false : true;

            bool stub;
            if (!Boolean.TryParse(text, out stub))
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return null;
            }
            
            return stub;
        }

        public static bool GetBoolean(this string text, string name, ref IImportEntity entity, int line)
        {
            bool? id = text.GetNullableBoolean(name, ref entity, line);
            if (!id.HasValue)
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return false;
            }

            return id.Value;            
        }

        public static DateTime? GetNullableDateTime(this string text, string name, ref IImportEntity entity, int line)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            if (text.Contains(' '))
            {
                var arr = text.Split(' ');
                if (arr.Length > 1)
                    text = string.Format("{0} {1}", arr[0].Replace('-', '.'), arr[1].Replace('-', ':'));
            }

            DateTime stub;
            if (!DateTime.TryParse(text, out stub) &&
                !DateTime.TryParseExact(text, "dd.MM.yyyy HH:mm:ss", new CultureInfo("ru-Ru"), DateTimeStyles.None, out stub))
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return DateTime.MinValue;
            }
            if (stub < new DateTime(1861, 1, 1))
            {
                entity.AddCsvError("Не удалось извлечь " + name + ". Неправильная дата", line);
                return DateTime.MinValue;
            }

            return stub;
        }

        public static Guid GetGuid(this string text, string name, ref IImportEntity entity, int line)
        {
            Guid? id = text.GetNullableGuid(name, ref entity, line);
            if (!id.HasValue)
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return Guid.Empty;
            }
            return id.Value;
        }

        public static int GetInt32(this string text, string name, int max, ref IImportEntity entity, int line)
        {
            var id = GetInt32(text, name, ref entity, line);
            if (id > max || id < 0)
            {
                entity.AddCsvError("Превышено максимальное значение - " + name, line);
                return id;
            }

            return id;
        }

        public static int GetInt32(this string text, string name, ref IImportEntity entity, int line)
        {
            int? id = text.GetNullableInt32(name, ref entity, line);
            if (!id.HasValue)
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return -1;
            }

            return id.Value;
        }

        public static short GetShort(this string text, string name, ref IImportEntity entity, int line)
        {
            short? id = text.GetNullableShort(name, ref entity, line);
            if (!id.HasValue)
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return -1;
            }

            return id.Value;
        }

        public static DateTime GetDateTime(this string text, string name, ref IImportEntity entity, int line)
        {
            DateTime? date = text.GetNullableDateTime(name, ref entity, line);
            if (!date.HasValue)
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return DateTime.MinValue;
            }
            
            return date.Value;
        }

        public static int GetSex(this string text, string name, ref IImportEntity entity, int line)
        {
            if (text.ToLower() != "м" && text.ToLower() != "ж" && text != "0" && text != "1")
            {
                entity.AddCsvError("Не удалось извлечь " + name, line);
                return -1;
            }
            
            return (text.ToLower().Trim() == "м" || text.ToLower().Trim() == "0") ? 0 : 1;
        }

        public static int? GetNullableSex(this string text, string name, ref IImportEntity entity, int line)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            return GetSex(text, name, ref entity, line);
        }
    }
}
