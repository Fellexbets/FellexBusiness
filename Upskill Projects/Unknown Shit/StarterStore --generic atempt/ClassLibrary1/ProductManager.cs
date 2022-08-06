using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Text.Encodings.Web;

namespace ClassLibrary1
{
    public class ProductsManager : IEnumerable<Product>
    {
        private List<Product> products;

        public ProductsManager() => GenerateProductsList();


        private void GenerateProductsList()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(Product), typeof(PathAttribute)) as PathAttribute;
            if (!File.Exists(pathAttribute.Path))
            {
                products = new List<Product>();
                return;
            }
            string jsonString = File.ReadAllText(pathAttribute.Path);
            products = JsonSerializer.Deserialize<List<Product>>(jsonString);
        }

        public void AdicionarProdutos(Product produto)
        {
            products.Add(produto);
        }

        public void RemoverProdutos(Product produto)
        {
            products.Remove(produto);
        }


        public void SaveToFile()
        {
            string path = @"StarterProducts.json";
            string product = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(path, product);

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return products.GetEnumerator();
        }
        public IEnumerator<Product> GetEnumerator()
        {
            return products.GetEnumerator();
        }










    }
}
