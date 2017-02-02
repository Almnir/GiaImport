using System;
using System.Runtime.Serialization;

namespace GiaImport
{
    [Serializable]
    internal class SyncException : Exception
    {
        public SyncException()
        {
        }

        public SyncException(string message) : base(message)
        {
        }

        public SyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}