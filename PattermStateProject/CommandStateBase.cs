namespace PattermStateProject
{
    public abstract class CommandStateBase : ICommandState
    {
        private readonly int _key;

        protected CommandStateBase(int key)
        {
            _key = key;
        }

        public abstract ICommandState Next(ContextState context);

        public override bool Equals(object obj)
        {
            if (obj == null) 
                return false;

            if (obj is CommandStateBase)
                return ((CommandStateBase)obj)._key == _key;

            return false;
        }

        public override int GetHashCode()
        {
            return _key;
        }
    }
}
