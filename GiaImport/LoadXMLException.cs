using System;
using System.Runtime.Serialization;

namespace GiaImport
{
    [Serializable]
    internal class LoadXMLException : Exception
    {
        public LoadXMLException()
        {
        }

        public LoadXMLException(string message) : base(message)
        {
        }

        public LoadXMLException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoadXMLException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}