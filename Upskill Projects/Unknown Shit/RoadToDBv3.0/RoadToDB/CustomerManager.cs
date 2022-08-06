using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Collections;

namespace RoadToDB
{
    // Representa um conjunto de clientes
    public class CustomerManager : IEnumerable<Customer>
    {

        private List<Customer> customers;

        public CustomerManager() => CarregarListaClientes();

      

        private void CarregarListaClientes()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(Customer), typeof(PathAttribute)) as PathAttribute;
            if (!File.Exists(pathAttribute.Path))
            {
                customers = new List<Customer>();
                return;
            }
            string jsonString = File.ReadAllText(pathAttribute.Path);
            customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return customers.GetEnumerator();
        }
        public IEnumerator<Customer> GetEnumerator()
        {
            return customers.GetEnumerator();
        }
    }
}
