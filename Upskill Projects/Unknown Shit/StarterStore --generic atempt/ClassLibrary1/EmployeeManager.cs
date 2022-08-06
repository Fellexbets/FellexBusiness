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
    public class EmployeeManager : IEnumerable<Employee>
    {
        private List<Employee> employees;

        public EmployeeManager() => CarregarListaEmployee();

        private void CarregarListaEmployee()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(Employee), typeof(PathAttribute)) as PathAttribute;
            if (!File.Exists(pathAttribute.Path))
            {
                employees = new List<Employee>();
                return;
            }
            string jsonString = File.ReadAllText(pathAttribute.Path);
            employees = JsonSerializer.Deserialize<List<Employee>>(jsonString);

        }

        public void AdicionarEmpregados(Employee empregado)
        {
            employees.Add(empregado);
        }

        public void RemoverEmpregado(Employee empregado)
        {
            employees.Remove(empregado);
        }

        public void SaveToFile()
        {

            string path = @"Employees.json";
            string customer = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(path, customer);

        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return employees.GetEnumerator();
        }
        public IEnumerator<Employee> GetEnumerator()
        {
            return employees.GetEnumerator();
        }




    }
}
