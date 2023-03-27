namespace SpaceShipProject.Contracts.Commands
{
    public interface IRotable
    {
        /// <summary>
        /// Направление движения (угол) 0..7
        /// </summary>
        int Direction { get; set; }

        /// <summary>
        /// Угловая скорость (изменения направления) 0..7
        /// </summary>
        int AngularVelocity { get; }

        /// <summary>
        /// Кол-во возможных направлений (8)
        /// </summary>
        int DirectionsNumber { get; }
    }
}
