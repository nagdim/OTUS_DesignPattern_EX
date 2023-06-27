using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intepretator
{
    public class UIObject
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public void SetPropertyValue(string propName, object propValue)
        {
            _values[propName] = propValue;
        }

        public object GetPropertyValue(string propName)
        {
            return _values[propName];
        }
    }

    public class UIUserObject : UIObject
    {

        public int ID
        {
            get
            {
                return (int)GetPropertyValue(nameof(UIUserObject.ID));
            }
            set
            {
                SetPropertyValue(nameof(UIUserObject.ID), value);
            }
        }

        public bool Shoot
        {
            get
            {
                return (bool)GetPropertyValue(nameof(UIUserObject.Shoot));
            }
            set
            {
                SetPropertyValue(nameof(UIUserObject.Shoot), value);
            }
        }
        public int Velocity
        {
            get
            {
                return (int)GetPropertyValue(nameof(UIUserObject.Velocity));
            }
            set
            {
                SetPropertyValue(nameof(UIUserObject.Velocity), value);
            }
        }
    }

}
