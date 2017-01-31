using System;

namespace RBD.Common.Attributes
{
    public class GiaDescriptionAttribute : Attribute
    {
        public string Description
        {
            get { return DescriptionValue; }
        }

        protected string DescriptionValue { get; set; }

        public GiaDescriptionAttribute(string descr)
        {
            DescriptionValue = descr;
        }
    }
}