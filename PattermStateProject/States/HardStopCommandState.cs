namespace PattermStateProject.States
{
    public class HardStopCommandState : CommandStateBase
    {
        public HardStopCommandState()
            : base(1)
        {

        }

        public override ICommandState Next(ContextState context)
        {
            return null;
        }
    }
}
