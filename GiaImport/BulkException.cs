using System;
using System.Runtime.Serialization;

namespace GiaImport
{
    [Serializable]
    internal class BulkException : Exception
    {
        private Exception ex;

        public BulkException()
        {
        }

        public BulkException(string message) : base(message)
        {
        }

        public BulkException(Exception ex)
        {
            this.ex = ex;
        }

        public BulkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BulkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}