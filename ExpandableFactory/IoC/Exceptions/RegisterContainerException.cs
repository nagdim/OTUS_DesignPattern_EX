﻿using System;

namespace ExpandableFactory.IoC.Exceptions
{
    public class RegisterContainerException : Exception
    {
        public RegisterContainerException(string message) : base(message)
        {
        }

        public RegisterContainerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
