using System;
using System.Runtime.Serialization;

namespace Reflect.Integration.API
{
    [Serializable]
    internal class InvalidStatementException : Exception
    {
        public InvalidStatementException()
        {
        }

        public InvalidStatementException(string message) : base(message)
        {
        }

        public InvalidStatementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidStatementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}