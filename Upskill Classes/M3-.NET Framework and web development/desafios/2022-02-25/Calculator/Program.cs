using System;
using CalculatorLib;

namespace CalculatorProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insira dois operandos separados por ENTER");
            string strA = Console.ReadLine();
            string strB = Console.ReadLine(); 
            int.TryParse(strA, out int a);
            int.TryParse(strB, out int b);

            Calculator calculator = new Calculator();
            double res = calculator.Add(a, b);
            Console.WriteLine($"{a} + {b} = {res}");
        }
    }
}
