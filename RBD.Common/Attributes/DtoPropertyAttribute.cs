using System;

namespace RBD.Client.Dto.Attributes
{
    /// <summary>
    /// Маппинг свойства Domain - Dto
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DtoPropertyAttribute : Attribute
    {
        public DtoPropertyAttribute()
        {
            DtoPropertyName = null;
        }

        public DtoPropertyAttribute(string DtoPropertyName)
        {
            this.DtoPropertyName = DtoPropertyName;
        }

        public DtoPropertyAttribute(string DtoPropertyName, Type BaseType, string BaseProperty)
        {
            this.DtoPropertyName = DtoPropertyName;
            this.BaseType = BaseType;
            this.BaseProperty = BaseProperty;
        }

        public Type BaseType { get; private set; }
        public string DtoPropertyName { get; private set; }
        public string BaseProperty { get; private set; }
    }
}