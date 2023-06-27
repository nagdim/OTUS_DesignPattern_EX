namespace Intepretator.Commands
{
    public class StopMoveCommand : ICommand
    {
        private readonly UIUserObject _uIObject;
        private readonly int _velocity;

        public StopMoveCommand(UIUserObject uIObject)
        {
            _uIObject = uIObject;
            _velocity = 0;
        }

        public void Execute()
        {
            _uIObject.Velocity = _velocity;
        }
    }
}
