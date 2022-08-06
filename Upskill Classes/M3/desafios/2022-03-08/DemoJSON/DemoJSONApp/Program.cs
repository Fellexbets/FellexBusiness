using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace DemoJSONApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "customers.json";
            if (File.Exists(path))
            {

                // Desserialização
                string jsonString = File.ReadAllText(path);
                List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.ContactName);
                }

                // Serialização
                List<Customer> customers2 = new List<Customer>();
                customers2.Add(customers[0]);
                customers2.Add(customers[1]);
                customers2.Add(customers[2]);
                string customerString = JsonSerializer.Serialize(customers2, new JsonSerializerOptions { WriteIndented = true }) ;
                File.WriteAllText("customer.json", customerString);    
            }
            else 
            {
                Console.WriteLine("Ficheiro inexistente...");
            }

            
        }
    }
}
