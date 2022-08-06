using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace ClassLibrary1
{
    public class Manager<T> : IEnumerable<T>
    {
        private List<T> contents;

        public static Manager<T> Instance { get; private set; }

        static Manager() => Instance = new Manager<T>();

        private Manager() => CarregarLista();

        private void CarregarLista()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            if (!File.Exists(pathAttribute.Path))
            {
                contents = new List<T>();
                return;
            }
            string jsonString = File.ReadAllText(pathAttribute.Path);
            contents = JsonSerializer.Deserialize<List<T>>(jsonString);

        }

        public void AdicionarClientes(T cliente)
        {
            contents.Add(cliente);
        }

        public void RemoverClientes(T entrada)
        {
            contents.Remove(entrada);
        }

        public void SaveCustomersToFile()
        {

            string path = @"Customers.json";
            string customer = JsonSerializer.Serialize (contents, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(path, customer);

        }
        public void SaveEmployeesToFile()
        {

            string path = @"Employees.json";
            string customer = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(path, customer);

        }
        public void SaveProductsToFile()
        {

            string path = @"Products.json";
            string customer = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(path, customer);

        }
        public void SaveSuppliersToFile()
        {

            string path = @"Suppliers.json";
            string customer = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(path, customer);

        }


        public void ShowAllCustomers()
        {
            foreach (Customer customer in Manager<Customer>.Instance)
            {
                Console.WriteLine(customer);
            }
        }
        private static void ShowAllEmployees()
        {
            foreach (Employee employee in Manager<Employee>.Instance)
            {
                Console.WriteLine(employee);
            }
        }
        private static void ShowAllProducts()
        {
            foreach (Product product in Manager<Product>.Instance)
            {
                Console.WriteLine(product);
            }
        }
        private static void ShowAllSuppliers()
        {
            foreach (Supplier supplier in Manager<Supplier>.Instance)
            {
                Console.WriteLine(supplier);
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return contents.GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return contents.GetEnumerator();
        }




    }
}
