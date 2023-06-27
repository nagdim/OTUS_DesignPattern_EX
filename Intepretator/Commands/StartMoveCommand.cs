namespace Intepretator.Commands
{
    public class StartMoveCommand : ICommand
    {
        private readonly UIUserObject _uIObject;
        private readonly int _velocity;

        public StartMoveCommand(UIUserObject uIObject, int velocity)
        {
            _uIObject = uIObject;
            _velocity = velocity;
        }

        public void Execute()
        {
            _uIObject.Velocity = _velocity;
        }
    }
}
