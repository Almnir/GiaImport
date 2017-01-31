using System;
using System.Runtime.Serialization;

namespace GiaImport
{
    [Serializable]
    internal class ShrinkFilesException : Exception
    {
        private Exception ex;

        public ShrinkFilesException()
        {
        }

        public ShrinkFilesException(string message) : base(message)
        {
        }

        public ShrinkFilesException(Exception ex)
        {
            this.ex = ex;
        }

        public ShrinkFilesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShrinkFilesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}