using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "customers.json";
            if (File.Exists(path))
            {

            
                string jsonStringArray = File.ReadAllText(path);
                string jsonString = string.Join("", jsonStringArray);
                List <Customer> Customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);
                foreach (Customer customer in Customers)
                {
                    Console.WriteLine(customer.companyName);
                }
                Customer c = Customers[0];
                string customerString = JsonSerializer.Serialize(c);
                    Console.WriteLine(customerString);
            }
            else
            {
                Console.WriteLine("o FICHEIRO NÃO EXISTE AMIGÃO");
            }
        }
    }
}
