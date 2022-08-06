using System;

namespace _2DataTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}.");
            Console.WriteLine($"double uses {sizeof(double)} bytes and can store numbers in the range {double.MinValue:N0} to {double.MaxValue:N0}.");
            Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in the range {decimal.MinValue:N0} to {decimal.MaxValue:N0}.");

            uint naturalNumber = 23; // unsigned integer means positive whole number including 0
            int population = 66_000_000; // 66 million in UK 
            double weight = 1.88; // in kilograms
            decimal price = 4.99M; // in pounds sterling
            float realNumber = 2.3F; // float means single-precision floating point
            string fruit = "Apples"; // strings use double-quotes 
            char letter = 'Z'; // chars use single-quotes
            bool happy = true; // Booleans have value of true or false

            Console.WriteLine($"default(int) = {default(int)}");
            Console.WriteLine($"default(bool) = {default(bool)}");
            Console.WriteLine($"default(DateTime) = {default(DateTime)}");
            Console.WriteLine($"default(string) = {default(string)}");
        }
    }
}
