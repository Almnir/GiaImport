using System;
using System.Runtime.Serialization;

namespace GiaImport
{
    [Serializable]
    internal class TruncateException : Exception
    {
        public TruncateException()
        {
        }

        public TruncateException(string message) : base(message)
        {
        }

        public TruncateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TruncateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}