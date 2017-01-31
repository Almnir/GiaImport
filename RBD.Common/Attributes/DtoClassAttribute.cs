using System;

namespace RBD.Client.Dto.Attributes
{
    /// <summary>
    /// Маппинг класс Domain - Dto
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DtoClassAttribute : Attribute
    {
        public DtoClassAttribute(Type DtoType)
        {
            this.DtoType = DtoType;
        }

        public Type DtoType { get; private set; }
    }
}