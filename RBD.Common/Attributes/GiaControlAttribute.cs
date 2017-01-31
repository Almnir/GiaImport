using System;

namespace RBD.Common.Attributes
{
	public class GiaControlAttribute : Attribute
	{
		public Type Type;
		public GiaControlAttribute(Type type)
		{
			Type = type;
		}
    }
}
