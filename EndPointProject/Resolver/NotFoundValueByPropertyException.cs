using System;
using System.Runtime.Serialization;

namespace EndPointProject
{
    [Serializable]
    public class NotFoundValueByPropertyException : Exception
    {
        public NotFoundValueByPropertyException()
        {
        }

        public NotFoundValueByPropertyException(string message) : base(message)
        {
        }

        public NotFoundValueByPropertyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundValueByPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}