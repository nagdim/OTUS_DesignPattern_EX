using SpaceShipProject.Contracts.Common;

namespace SpaceShipProject.Contracts.Commands
{
    public interface IMovable
    {
        /// <summary>
        /// Текущая позиция объекта
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// Мгновенная скорость объекта
        /// </summary>
        Vector Velocity { get; set; }
    }
}
