using SpaceShipProject.Contracts.Commands;
using System.Collections.Generic;

namespace PattermStateProject.States
{
    public class MoveToCommandState : CommandStateBase
    {
        private readonly Queue<ICommand> _commands;

        public MoveToCommandState(Queue<ICommand> commands)
            : base(3)
        {
            _commands = commands;
        }

        public override ICommandState Next(ContextState context)
        {
            var command = context.GetCommandFromQueue();

            if (command != null)
            {
                _commands.Enqueue(command);
                return context.ChangeState(command, this);
            }

            return this;
        }
    }
}
