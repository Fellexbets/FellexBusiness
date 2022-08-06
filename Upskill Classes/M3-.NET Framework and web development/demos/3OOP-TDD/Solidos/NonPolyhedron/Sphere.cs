using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDSolidos
{
    public class Sphere : NonPolyhedron
    {
        public Sphere(double radius) : base(radius)
        {

        }

        public override double Volume(){
            // SurfaceArea: Math.PI * radius * radius
            return  4.0 / 3 * SurfaceArea() * radius;
        }
    }
}
