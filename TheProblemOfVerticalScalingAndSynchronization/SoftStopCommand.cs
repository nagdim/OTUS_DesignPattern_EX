using SpaceShipProject.Contracts.Commands;

namespace TheProblemOfVerticalScalingAndSynchronization
{
    public class SoftStopCommand : ICommand
    {
        private readonly EventLoop m_eventLoop;

        public SoftStopCommand(EventLoop eventLoop)
        {
            m_eventLoop = eventLoop;
        }

        public void Execute()
        {
            m_eventLoop.SetFuncToStop(
                () =>
                {
                    return !m_eventLoop.QueueIsEmpty;
                });
        }
    }
}
