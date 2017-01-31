using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RBD
{
    public static class StringExtension
	{
		public static string ListToString<K>(this IEnumerable<K> items)
		{
			var result = new StringBuilder();
			foreach (var item in items)
			{
				if (item == null) continue;
				result.Append("'");
				result.Append(item);
				result.Append("',");
			}
			return result.ToString().TrimEnd(',');
		}

		public static string ListToString<K>(this IEnumerable<K> items, bool withOutQuote, string prefix)
		{
            if (!items.Any()) return string.Empty;
			var result = new StringBuilder();
			foreach (var item in items)
			{
				if (item == null) continue;
				if (!withOutQuote) result.Append("'");
				result.Append(item);
				result.Append(prefix);
				if (!withOutQuote) result.Append("'");
				result.Append(",");
			}
			return result.ToString().TrimEnd(',');
		}

        public static string ListToString<K>(this IEnumerable<K> items, string delimiter)
        {
            if (!items.Any()) return string.Empty;
            var arrayOfString = items.Select(x => x.ToString()).OrderBy(x => x).ToArray();
            return string.Join(delimiter, arrayOfString);
        }

        public static string ListToString<K>(this IEnumerable<K> items, char delimiter)
        {
            return ListToString(items, delimiter.ToString());
        }

		public static bool IsGUID(this string expression)
		{
			try
			{
				new Guid(expression);
				return true;
			}
			catch (Exception)
			{
				
			}
			return false;
		}

		public static T To<T>(this string text)
		{
			try
			{
				if (typeof(T) == typeof(bool) && (text.ToLower().Trim() != "true" || text.ToLower().Trim() != "false"))
				{
					text = (int)Convert.ChangeType(text, typeof(int)) > 0 ? bool.TrueString : bool.FalseString;
				}
				if (typeof(T).IsEnum)
				{
					return (T)Enum.Parse(typeof(T), text);
				}
				if (!typeof(T).IsInterface && typeof(T).IsGenericType)
				{
					Type innerType = typeof(T).GetGenericArguments()[0];
					return (T)To(text, innerType);
				}
				if (typeof(T) == typeof(Guid))
				{
					throw new Exception("Guid не поддерживается");
				}
				return (T)Convert.ChangeType(text, typeof(T));
			}
			catch (Exception)
			{
				return default(T);
			}
		}

		public static string ToDateString(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;
			
			DateTime date;
			return DateTime.TryParseExact(str, "yyyy.MM.dd", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out date) ?  date.ToString("dd.MM.yyyy") : str;
		}

		public static string ToExamDateString(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;
			
		    var result = str.ToDate().ToExamDateString();
			return result;
		}

		public static string ToExamDateString(this DateTime str)
		{
		    return str.ToString("yyyy.MM.dd");
		}

        public static DateTime NotMillisecondsDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

	    public static string ToExamDateString(this DateTime? str)
        {
            if (str == null) return string.Empty;
            return str.Value.ToString("yyyy.MM.dd");
        }

		public static bool IsDate(this string text)
		{
			if (string.IsNullOrEmpty(text))
				return false;

			DateTime date;
			return DateTime.TryParseExact(text, "dd.MM.yyyy HH:mm:ss", new CultureInfo("ru-Ru"), DateTimeStyles.None, out date);
		}

		public static DateTime ToDate(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return DateTime.MinValue;

			DateTime date;
			if (DateTime.TryParseExact(str, "yyyy.MM.dd", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return date;

			if (DateTime.TryParseExact(str, "dd.MM.yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
		        return date;

			if (DateTime.TryParse(str, out date))
		        return date;

		    return DateTime.MinValue;
		}

		public static bool IsCyrillic(this string str, string additional)
		{
            if (string.IsNullOrEmpty(str)) return true;
			const string cyrillic = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЭЮЯЬЪЫабвгдеёжзийклмнопрстуфхцчшщэюяьъы";
			const string marks = ".- ";
			return HasChar(str, string.Concat(cyrillic, marks, additional));
		}

		public static bool IsCyrillic(this string str)
		{
			return IsCyrillic(str, string.Empty);
		}

		public static bool IsCyrillicWithDigits(this string str)
		{
			return IsCyrillic(str, "0123456789");
		}
        /// <summary> Кириллица, цифры, пробелы, пробел (« »), точку («.»), запятую («,»), дефис («-»), наклонную черту («/»)
        /// Изменение формата адресов РЦОИ, ОО и ППЭ - RBD-4586
        /// </summary>
        public static bool IsAddressValidSymbols(this string str)
        {
            return IsCyrillic(str, "0123456789 .,-/");
        }

		public static bool IsAllCharsGt32(this string str)
		{
            return !str.ToCharArray().Any(c => c < 32);
		}


		public static  bool IsClass(this string str)
		{
			return str.IsAllCharsGt32();
		}

		public static bool IsDocSeries(this string str)
		{
			// Настоящий индийский код можно петь и танцевать!
            var cyrillic = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЭЮЯЬЪЫабвгдеёжзийклмнопрстуфхцчшщэюяьъы-—";
            var latin = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-—";
            var arabicNumerals = "0123456789-—";
            var romanNumerals = "IVXCLMD1ivxclmd-—";

			// Добавить возможность в номере документа указывать латинские буквы и знак минус/тире
			// Можно было в одну строчку написать, но с несколькими if`ами понятнее
			// Серия документа может содержать:

			// 1. буквы кириллицы и арабские цифры
			if (HasChar(str, arabicNumerals, cyrillic)) return true;

			// 2. Латинские буквы, арабские цифры
			if (HasChar(str, latin, arabicNumerals)) return true;

			// 3. Буквы кириллицы, римские цифры
			if (HasChar(str, cyrillic, romanNumerals)) return true;
				
			return false;
		}


		private static bool HasChar(string str, params string[] chars)
		{
			var l = string.Join("", chars);

			return !str.ToCharArray().Any(c => !l.Contains(c));
		}
        
		static object To(this string text, Type type)
		{
			return Convert.ChangeType(text, type);
		}

        public static string ToUpperLetter(this string text)
        {
            if (string.IsNullOrEmpty(text.Trim()))
                return text;

            return text.Trim().Replace(text.Trim(),
                                       text.Trim().Substring(0, 1).ToUpper() +
                                       text.Trim().Substring(1, text.Trim().Length - 1));
        }

        public static string GetUpperLetter(this string field)
        {
            if (!string.IsNullOrEmpty(field)) return field.ToUpperLetter();
            return field;
        }

        public static string GetUpperLetter(this string field, int length)
        {
            if (!string.IsNullOrEmpty(field))
            {
                string fieldInUpperLetter = field.ToUpperLetter();
                if (length > 0 && fieldInUpperLetter.Length > length)
                {
                    return fieldInUpperLetter.Substring(0, 255);
                }
                return fieldInUpperLetter;
            }
            return field;
        }

        public static string ToAuthCodeFormat(this string authCode)
        {
            if (string.IsNullOrEmpty(authCode))
                return string.Empty;

            for (int i = 4; i < authCode.Length; i += 5)
            {
                authCode = authCode.Insert(i, "-");
            }
            return authCode;
        }

        public static string ToAuditoriumCodeFormat(this string code)
        {
            if (string.IsNullOrEmpty(code)) return code;
            int codeInt; Int32.TryParse(code, out codeInt);
            return codeInt.ToString("0000");
        }

        public static int? GetAreaCodeFromImportFileName(this string fileName)
        {
            var index = fileName.IndexOf("ATE_", StringComparison.Ordinal);
            if (index > 0)
            {
                var ateStr = fileName.Substring(index + 4);
                index = ateStr.IndexOf("_", StringComparison.Ordinal);
                var areaCodeStr = ateStr.Substring(0, index);
                int areaCode;
                if (Int32.TryParse(areaCodeStr, out areaCode))
                {
                    return areaCode;
                }
            }
            return null;
        }

        public static string GetNumericSymbols(this string text)
        {
            return new string(text.Where(Char.IsDigit).ToArray());
        }
    }
}
