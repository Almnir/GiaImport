using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using RBD.Common.Attributes;
using RBD.Common.Enums;

namespace RBD
{
	public static class EnumExtensions
	{
		public static List<EnumDescription> ToEnumList(this Type enumType)
		{
			var result = new List<EnumDescription>();
			foreach (var field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
			{
			    var obsolete = field.GetCustomAttributes(typeof (ObsoleteAttribute), true).Any();
                if (!obsolete)
                {
                    result.Add(new EnumDescription((int)(field.GetValue(null)), GetFieldInfoDescription(field)));
                }
            }
			return result;
		}

        public static string GetDescriptionShort<TEnum>(this TEnum item)
        {
            return GetExtendedDescription(item).ShortDesctiption;
        }

        public static ExtendedDescriptionAttribute GetExtendedDescription<TEnum>(this TEnum item)
        {
            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                foreach (ExtendedDescriptionAttribute attrib in field.GetCustomAttributes(typeof(ExtendedDescriptionAttribute), true))
                {
                    if ((int)field.GetValue(null) == Convert.ToInt32(item)) 
                        return attrib;
                }
            }
            return new ExtendedDescriptionAttribute("---");
        }

        public static string GetPropertyInfoDescription(this PropertyInfo property)
        {
            if (property == null) return string.Empty;
            return string.Join(", ",
                property.GetCustomAttributes(typeof(DescriptionAttribute), true).AsEnumerable()
                    .OfType<DescriptionAttribute>().Select(c => c.Description).ToArray());
        }

        public static string GetFieldInfoDescription(FieldInfo field)
        {
#if GIA
            foreach (GiaDescriptionAttribute attrib in field.GetCustomAttributes(typeof(GiaDescriptionAttribute), true))
                return attrib.Description;
#endif
            foreach (DescriptionAttribute attrib in field.GetCustomAttributes(typeof(DescriptionAttribute), true))
                return attrib.Description;

            return null;
        }

        public static string GetDescription<TEnum>(this TEnum enumType, int value)
        {
            foreach (FieldInfo field in typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                if ((int)field.GetValue(null) == value) return GetFieldInfoDescription(field);
            }

            return null;
        }

	    public static string GetDescription<TEnum>(this TEnum item)
            where TEnum : struct, IConvertible
	    {
            /* Flag */
            if (item.IsFlags())
            {
                var flag = Convert.ToInt32(item);
                if (flag == 0 && Enum.IsDefined(typeof(TEnum), 0))
                {
                    return item.GetDescription(0);
                }
                var enums = typeof(TEnum).ToEnumList();
                return string.Join(", ",
                    enums.Where(e => e.Value > 0 && (e.Value & flag) == e.Value).Select(e => e.Description).ToArray());
            }

            return item.GetDescription(Convert.ToInt32(item));
	    }

        public static string GetDescription(this Type type)
        {
            return string.Join(", ",
                type.GetCustomAttributes(typeof(DescriptionAttribute), true).AsEnumerable()
                .OfType<DescriptionAttribute>().Select(c => c.Description).ToArray());
        }

        public static string GetFieldValue<TEnum>(this TEnum item)
        {
            foreach (FieldInfo field in typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                if ((int)field.GetValue(null) == Convert.ToInt32(item))
                {
                    foreach (FieldValueAttribute attrib in field.GetCustomAttributes(typeof(FieldValueAttribute), true))
                        return attrib.Value;
                }
            }

            return null;
        }

        public static bool ContainsFlag<TEnum>(this TEnum item, TEnum flag)
            where TEnum : struct, IConvertible
        {
            if (!IsFlags(item))
            {
                throw new ApplicationException("ContainsFlag: Only flag enums");
            }
            return (Convert.ToInt32(item) & Convert.ToInt32(flag)) > 0;
        }

        static bool IsFlags<TEnum>(this TEnum item)
            where TEnum : struct, IConvertible
        {
            return typeof(TEnum).GetCustomAttributes(typeof(FlagsAttribute), true).Length > 0;
        }

        /// <summary>
        /// True == Заблокировано
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool AsBool(this LockType type)
        {
            return type != LockType.None;
        }

	    public static int? ToOrFlags(this int[] variants)
	    {
	        if (!variants.Any()) return null;

	        int flag = 0;
	        foreach (var variant in variants)
                flag |= variant;
            return flag;
	    }
	}
}