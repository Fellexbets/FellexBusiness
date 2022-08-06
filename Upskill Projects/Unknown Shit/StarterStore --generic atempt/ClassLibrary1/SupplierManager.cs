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
    public class SupplierManager : IEnumerable<Supplier>
    {
        private List<Supplier> suppliers;

        public SupplierManager() => GenerateList();


        private void GenerateList()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(Supplier), typeof(PathAttribute)) as PathAttribute;
            if (!File.Exists(pathAttribute.Path))
            {
                suppliers = new List<Supplier>();
                return;
            }
            string jsonString = File.ReadAllText(pathAttribute.Path);
            suppliers = JsonSerializer.Deserialize<List<Supplier>>(jsonString);
        }

        public void AdicionarSupplier(T supply)
        {
            suppliers.Add(supply);
        }

        public void RemoverSupplier(T supply)
        {
            suppliers.Remove(supply);
        }

        public void SaveToFile()
        {
            string path = @"Suppliers.json";
            string supply = JsonSerializer.Serialize(suppliers, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(path, supply);

        }

        public void RemoverTudo(string ID)
        {
            suppliers.RemoveAll(qqcoisa => qqcoisa.supplierID == ID);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return suppliers.GetEnumerator();
        }
        public IEnumerator<Supplier> GetEnumerator()
        {
            return suppliers.GetEnumerator();
        }










    }
}
