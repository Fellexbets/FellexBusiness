using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExample
{
    class Sphere : NonPolyhedron
    {
        public Sphere(double radius) : base(radius)
        {

        }

        public override double Volume(){
            // SurfaceArea: Math.PI * radius * radius
            return  4 / 3 * SurfaceArea() * radius;
        }
    }
}
