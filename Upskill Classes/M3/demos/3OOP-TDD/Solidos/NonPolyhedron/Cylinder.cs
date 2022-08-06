using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDSolidos
{
    public class Cylinder : NonPolyhedron
    {
        double height;

        public Cylinder(double radius, double height) : base(radius)
        {
            this.height = height;
        }

        public override double Volume(){
            return SurfaceArea() * height;
        }
    }
}
