

using SpaceShipProject.Contracts.Commands;
using System;

namespace SpaceShipProject
{
    public class MoveCommand : ICommand
    {
        private readonly IMovable m_movable;

        public MoveCommand(IMovable movable)
        {
            m_movable = movable ?? throw new ArgumentNullException(nameof(movable));
        }

        public void Execute()
        {
            if (m_movable.Velocity == null)
                throw new ArgumentNullException(nameof(m_movable.Velocity));

            m_movable.Position += m_movable.Velocity;
        }
    }
}
