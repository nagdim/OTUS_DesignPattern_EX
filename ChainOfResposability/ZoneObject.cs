using ChainOfResposability.Interface;
using SpaceShipProject.Contracts.Common;
using System;
using System.CodeDom;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ChainOfResposability
{

    public class ZoneObject : IZoneObject
    {
        public Vector Coordinate { get; private set; }
        public ISectorOfZone Sector { get; set; }

        public double Size { get; set; }

        public ZoneObject(Vector point, double size)
        {
            Coordinate = point;
            Size = size;
        }

        public bool Verify(Vector point)
        {
            var d = Math.Sqrt(Math.Pow(Coordinate.X - point.X, 2) + Math.Pow(Coordinate.Y - point.Y, 2));

            if (d > Size)
                return false;

            return true;
        }
    }
}
