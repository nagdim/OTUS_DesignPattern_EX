using System;
using System.Runtime.Serialization;

namespace AuthTokenProject
{
    [Serializable]
    public class VerifyTokenException : Exception
    {
        public VerifyTokenException()
        {
        }

        public VerifyTokenException(string message) : base(message)
        {
        }

        public VerifyTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VerifyTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}