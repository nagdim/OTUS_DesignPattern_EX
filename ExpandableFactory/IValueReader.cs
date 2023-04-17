using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandableFactory
{
    public interface IValueReader
    {
        int[] Read();
    }
}
