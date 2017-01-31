using System.Runtime.Serialization;

namespace RBD.Common.Interfaces
{
	public interface ISerializationSurrogate
	{
		void GetObjectData(object obj,
		  SerializationInfo info, StreamingContext context);

		object SetObjectData(object obj,
		  SerializationInfo info, StreamingContext context,
		  ISurrogateSelector selector);
	}
}
