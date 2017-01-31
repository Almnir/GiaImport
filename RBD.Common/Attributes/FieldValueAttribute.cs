using System;

namespace RBD.Common.Attributes
{
    public class FieldValueAttribute : Attribute
    {
        public string Value { get; private set; }

        public FieldValueAttribute(string value)
        {
            Value = value;
        }
    }
}