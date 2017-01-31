namespace RBD.Common.Enums
{
    public class EnumDescription
    {
        public EnumDescription(int value, string description)
        {
            Value = value;
            Description = description;
        }

        public int Value { get; private set; }
        public string Description { get; private set; }

        public override string ToString()
        {
            return Description;
        }
    }
}