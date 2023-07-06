namespace EndPointProject
{
    public class GameObjectRequest : PropertyValueObject
    {
        public int GameID
        {
            get
            {
                return (int)GetPropertyValue("game");
            }
            set
            {
                SetPropertyValue("game", value);
            }
        }

        public int UObjectID
        {
            get
            {
                return (int)GetPropertyValue("uobject");
            }
            set
            {
                SetPropertyValue("uobject", value);
            }
        }

        public string ActionID
        {
            get
            {
                return (string)GetPropertyValue("icommand");
            }
            set
            {
                SetPropertyValue("icommand", value);
            }
        }

        public object Params
        {
            get
            {
                return (string)GetPropertyValue("param");
            }
            set
            {
                SetPropertyValue("param", value);
            }
        }
    }
}
