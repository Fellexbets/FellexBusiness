using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDSolidos
{
    public abstract class NonPolyhedron : Solid
    {
        protected double radius;

        public NonPolyhedron(double radius)
        {
            this.radius = radius;
        }

        public override double SurfaceArea(){
            return Math.PI * radius * radius;
        }
    }
}
