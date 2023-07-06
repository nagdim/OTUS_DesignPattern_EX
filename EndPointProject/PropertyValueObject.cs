using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointProject
{
    public class PropertyValueObject
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
}
