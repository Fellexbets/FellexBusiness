using System;

namespace OOPExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Solid[] solidArray = new Solid[4];
            solidArray[0] = new Cube(3);
            solidArray[1] = new Parallelepiped(2, 3, 4);
            solidArray[2] = new Cylinder(5, 20);
            solidArray[3] = new Sphere(5);

            foreach (Solid s in solidArray)
            {
                Console.WriteLine($"Solid: {s.GetType().Name}, Surface Area: {Math.Round(s.SurfaceArea(), 2)}, Volume: {Math.Round(s.Volume(), 2)}");
            }
        }
    }
}
