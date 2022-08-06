using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExample
{
    class Cube : Solid
    {
        double side;

        public Cube(double side)
        {
            this.side = side;
        }

        public override double SurfaceArea(){
            return side * side;
        }

        public override double Volume(){
            return SurfaceArea() * side;
        }
    }
}
