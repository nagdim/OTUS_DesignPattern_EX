using System;

namespace SpaceShipProject.Exceptions
{
    public class FuelEmptyException : Exception
    {
        public FuelEmptyException(string message) : base(message)
        {

        }
    }
}
