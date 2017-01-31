namespace FCT.Client.Dto.Extensions
{
    public static class CreateDtoExtensions
    {
        //public static DtoBase CreateDto<TInput>(this TInput domain) where TInput : class
        //{
        //    Type domainType = typeof(TInput);
        //    DtoBase dto = null;

        //    /*
        //     * Создаем dto
        //     */
        //    var classAttribute = domainType.GetCustomAttributes(typeof(DtoClassAttribute), true).FirstOrDefault() as DtoClassAttribute;
        //    if (classAttribute == null)
        //        return dto;

        //    dto = Activator.CreateInstance(classAttribute.DtoType) as DtoBase;
        //    if (dto == null)
        //        return dto;

        //    /*
        //     * Заполняем поля dto
        //     */
        //    foreach (var prop in domainType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        //    {
        //        var domainField = prop.GetCustomAttributes(typeof (DtoPropertyAttribute), true).FirstOrDefault() as DtoPropertyAttribute;
        //        if (domainField == null)
        //            continue;

        //        var propertyName = domainField.DtoPropertyName ?? prop.Name;
        //        PropertyInfo dtoField = dto.GetType().GetProperty(propertyName);
        //        if (dtoField == null)
        //            continue;

        //        object domainValue;
        //        if (domainField.BaseType != null)
        //            domainValue = GetPropertyValue(prop.GetValue(domain, null), domainField.BaseType, domainField.BaseProperty);
        //        else
        //            domainValue = prop.GetValue(domain, null);

        //        dtoField.SetValue(dto, domainValue, null);
        //    }

        //    return dto;
        //}

        //static object GetPropertyValue(object obj, Type neededType, string neededName)
        //{
        //    object result = null;
        //    var activeObj = obj.GetType().GetCustomAttributes(typeof (ActiveRecordAttribute), true);
        //    if (activeObj.Length == 0)
        //        return null;

        //    foreach (var prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        //    {
        //        try
        //        {
        //            object value = prop.GetValue(obj, null);
        //            if (value is ICollection || value == null)
        //                continue;
                    
        //            if ((prop.ReflectedType == neededType || prop.ReflectedType.BaseType == neededType) && 
        //                prop.Name.Equals(neededName, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                return value;
        //            }

        //            GetPropertyValue(value, neededType, neededName);
        //        }
        //        catch (Exception e)
        //        {
        //            var ex = e.Message;
        //        }
        //    }

        //    return result;
        //}
    }
}
