namespace Intepretator.Commands
{
    public class ShootCommand : ICommand
    {
        private readonly UIUserObject _uIObject;
        private readonly bool _shoot;

        public ShootCommand(UIUserObject uIObject, bool shoot)
        {
            _uIObject = uIObject;
            _shoot = shoot;
        }

        public void Execute()
        {
            _uIObject.Shoot = _shoot;
        }
    }
}
