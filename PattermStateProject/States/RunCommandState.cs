namespace PattermStateProject.States
{
    public class RunCommandState : CommandStateBase
    {
        public RunCommandState()
            : base(2)
        {

        }

        public override ICommandState Next(ContextState context)
        {
            return new DefaultCommandState();
        }
    }
}
