using System;
using System.Runtime.Serialization;

namespace GiaImport
{
    [Serializable]
    internal class PreparationStageException : Exception
    {
        private Exception ex;

        public PreparationStageException()
        {
        }

        public PreparationStageException(string message) : base(message)
        {
        }

        public PreparationStageException(Exception ex)
        {
            this.ex = ex;
        }

        public PreparationStageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PreparationStageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}