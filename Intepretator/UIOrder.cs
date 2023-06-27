namespace Intepretator
{
    public class UIOrder : UIObject
    {
        public int ID
        {
            get
            {
                return (int)GetPropertyValue(nameof(ID));
            }
            set
            {
                SetPropertyValue(nameof(ID), value);
            }
        }

        public string Action
        {
            get
            {
                return (string)GetPropertyValue(nameof(Action));
            }
            set
            {
                SetPropertyValue(nameof(Action), value);
            }
        }

        public int Velocity
        {
            get
            {
                return (int)GetPropertyValue(nameof(Velocity));
            }
            set
            {
                SetPropertyValue(nameof(Velocity), value);
            }
        }

        public bool Shoot
        {
            get
            {
                return (bool)GetPropertyValue(nameof(Shoot));
            }
            set
            {
                SetPropertyValue(nameof(Shoot), value);
            }
        }
    }
}
