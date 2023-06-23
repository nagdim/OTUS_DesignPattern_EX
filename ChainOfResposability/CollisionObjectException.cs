using System;
using System.Runtime.Serialization;

namespace ChainOfResposability
{
    [Serializable]
    public class CollisionObjectException : Exception
    {
        public CollisionObjectException()
        {
        }

        public CollisionObjectException(string message) : base(message)
        {
        }

        public CollisionObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollisionObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}