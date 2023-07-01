using System;
using System.Runtime.Serialization;

namespace AuthTokenProject
{
    [Serializable]
    internal class GameNotFound : Exception
    {
        public GameNotFound()
        {
        }

        public GameNotFound(string message) : base(message)
        {
        }

        public GameNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}