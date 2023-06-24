namespace PattermStateProject.States
{
    public class DefaultCommandState : CommandStateBase
    {
        public DefaultCommandState()
            : base(0)
        {

        }

        public override ICommandState Next(ContextState context)
        {
            var command = context.GetCommandFromQueue();

            if (command != null)
            {
                command.Execute();
                return context.ChangeState(command, this);
            }

            return this;
        }
    }
}
