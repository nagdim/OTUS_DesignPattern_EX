using System;

namespace SpaceShipProject.Exceptions
{
    public class CommandException : Exception
    {
        public CommandException(string message) : base(message)
        {

        }
    }
}
