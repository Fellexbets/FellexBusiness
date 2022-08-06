using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExample
{
    class Parallelepiped : Solid
    {
        double width, length, height;

        public Parallelepiped(double width, double length, double height)
        {
            this.width = width;
            this.length = length;
            this.height = height;
        }

        public override double SurfaceArea(){
            return width * length;
        }

        public override double Volume(){
            return SurfaceArea() * height;
        }
    }
}
