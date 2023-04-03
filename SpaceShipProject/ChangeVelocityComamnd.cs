using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Contracts.Common;
using SpaceShipProject.Exceptions;
using System;

namespace SpaceShipProject
{
    public class ChangeVelocityComamnd : ICommand
    {
        private readonly IMovable m_moveObject;
        private readonly IRotable m_routeObject;

        public ChangeVelocityComamnd(IMovable moveObject, IRotable routeObject)
        {
            m_moveObject = moveObject;
            m_routeObject = routeObject;
        }

        public void Execute()
        {
            if (m_moveObject.Velocity == null)
                throw new NotDefinePropertyException("Velocity must be set");

            if (m_routeObject.DirectionsNumber <= 0)
                throw new NotDefinePropertyException("DirectionNumber cannot be less or equal Zero.");

            if (m_routeObject.Direction < 0 || m_routeObject.Direction >= m_routeObject.DirectionsNumber)
                throw new NotDefinePropertyException("Direction cannot be less Zero or greater equal DirectionNumber.");

            var devider = 360 * m_routeObject.Direction / m_routeObject.DirectionsNumber;

            int x = (int)(m_moveObject.Velocity.X * Math.Cos(devider));
            int y = (int)(m_moveObject.Velocity.Y * Math.Sin(devider));

            m_moveObject.Velocity = new Vector(x, y);
        }
    }
}
