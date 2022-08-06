using System;

namespace _5Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 11;
            int y = 3;
            Console.WriteLine($"x is {x}, y is {y}");
            Console.WriteLine($"x + y = {x + y}");
            Console.WriteLine($"x - y = {x - y}");
            Console.WriteLine($"x * y = {x * y}");
            Console.WriteLine($"x / y = {x / y}");
            Console.WriteLine($"x % y = {x % y}");

            int a = 10;
            double b = a; // an int can be safely cast into a double
            Console.WriteLine(b);

            double c = 9.8;
            int d = (int)c;
            Console.WriteLine(d); // d is 9 losing the .8 part

            long e = 10;
            int f = (int)e;
            Console.WriteLine($"e is {e:N0} and f is {f:N0}");

            e = 5_000_000_000;
            f = (int)e;
            Console.WriteLine($"e is {e:N0} and f is {f:N0}");

            double g = 9.8;
            int h = Convert.ToInt32(g);
            Console.WriteLine($"g is {g} and h is {h}");

            // Understanding the default rounding rules

            double[] doubles = new[]
              { 9.49, 9.5, 9.51, 10.49, 10.5, 10.51 };

            foreach (double n in doubles)
            {
                Console.WriteLine($"ToInt({n}) is {Convert.ToInt32(n)}");
            }

            // Taking control of rounding rules

            foreach (double n in doubles)
            {
                Console.WriteLine(format:
                  "Math.Round(value: {0}, digits: 0, mode: MidpointRounding.AwayFromZero) is {1}",
                  arg0: n,
                  arg1: Math.Round(value: n,
                    digits: 0,
                    mode: MidpointRounding.AwayFromZero));
            }
        }
    }
}
