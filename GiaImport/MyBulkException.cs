using System;
using System.Runtime.Serialization;

namespace GiaImport
{
    [Serializable]
    internal class MyBulkException : Exception
    {
        public MyBulkException()
        {
        }

        public MyBulkException(string message) : base(message)
        {
        }

        public MyBulkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyBulkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}