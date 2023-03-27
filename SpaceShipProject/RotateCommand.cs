

using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;
using System;

namespace SpaceShipProject
{
    public class RotateCommand : ICommand
    {
        private IRotable _rotable;

        public RotateCommand(IRotable rotable)
        {
            _rotable = rotable ?? throw new ArgumentNullException(nameof(rotable));
        }

        public void Execute()
        {
            if (_rotable.DirectionsNumber <= 0)
                throw new NotDefinePropertyException("DirectionsNumber cannot be less or equal zero.");

            if (_rotable.Direction < 0 || _rotable.Direction >= _rotable.DirectionsNumber)
                throw new NotDefinePropertyException($"{nameof(IRotable.Direction)} cannot be less 0 or greater {nameof(IRotable.DirectionsNumber)}.");

            if (_rotable.AngularVelocity < 0 || _rotable.AngularVelocity >= _rotable.DirectionsNumber)
                throw new NotDefinePropertyException($"{nameof(IRotable.AngularVelocity)} cannot be less 0 or greater {nameof(IRotable.DirectionsNumber)}.");

            _rotable.Direction = (_rotable.Direction + _rotable.AngularVelocity) % _rotable.DirectionsNumber;
        }
    }
}
