using System;

namespace Intepretator.Exceptions
{
    public class ResolveContainerException : Exception
    {
        public ResolveContainerException(string message) : base(message)
        {
        }

        public ResolveContainerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
