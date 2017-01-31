using System.ComponentModel;

namespace RBD.Common.Attributes
{
    public class ExtendedDescriptionAttribute : DescriptionAttribute
    {
        public string ShortDesctiption { get; set; }
        public ExtendedDescriptionAttribute(string description) : base(description) {}
        public ExtendedDescriptionAttribute(string description, string shortDesctiption) : base(description)
        {
            ShortDesctiption = shortDesctiption;
        }
    }
}
