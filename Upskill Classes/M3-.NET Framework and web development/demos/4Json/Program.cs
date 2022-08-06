using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace _4Json
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializeCustomer();
            DeserializeCustomerFromFile();
        }

        static void SerializeCustomer()
        {
            List<Customer> customers = new List<Customer>();
            Customer customerToSerialize = new Customer {
                CustomerID = "ALFKI",
                CompanyName = "Alfreds Futterkiste",
                ContactName = "Maria Anders",
                ContactTitle = "Sales Representative",
                Address = "Obere Str. 57",
                City = "Berlin",
                PostalCode = "12209",
                Country = "Germany",
                Phone = "030-0074321",
                Fax = "030-0076545"
            };
            customers.Add(customerToSerialize);
            string jsonCustomer = JsonConvert.SerializeObject(customers, Formatting.Indented);
            Console.WriteLine(jsonCustomer);
        }
        
        static void DeserializeCustomerFromFile()
        {
            string path = @"db\customers.json";
            if (System.IO.File.Exists(path))
            {
                string jsonstring = System.IO.File.ReadAllText(path);
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(jsonstring);
                foreach(Customer c in customers)
                {
                    Console.WriteLine(c.ContactName);
                }
            }
        }
    }
}
