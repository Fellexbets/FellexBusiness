using System;
using ClassLibrary1;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            
            Console.WriteLine("Select the group you want to work with:");
            Console.WriteLine("a) Customers");
            Console.WriteLine("b) Employees");
            Console.WriteLine("c) Products");
            Console.WriteLine("d) Suppliers");
            string opcao1 = Console.ReadLine();
            Console.WriteLine("Select the operation you want to perform:");
            Console.WriteLine("a) add");
            Console.WriteLine("b) remove");
            Console.WriteLine("c) showall");
            Console.WriteLine("d) find");
            string opcao2 = Console.ReadLine();

            if (opcao1 == "a")
            {
                if (opcao2 == "a")
                {
                    
                    Customer clienteNovo = new Customer();
                    Console.WriteLine("Write the customers name");
                    clienteNovo.contactName = Console.ReadLine();
                    Console.WriteLine("write the customers title");
                    clienteNovo.contactTitle = Console.ReadLine();
                    Console.WriteLine("write the customers address");
                    clienteNovo.address = Console.ReadLine();
                    Console.WriteLine("write the customers city");
                    clienteNovo.address = Console.ReadLine();
                    Console.WriteLine("write the customers country");
                    clienteNovo.address = Console.ReadLine();
                    Console.WriteLine("write the customers phone");
                    clienteNovo.address = Console.ReadLine();
                    Console.WriteLine("write the customers fax");
                    clienteNovo.address = Console.ReadLine();
                    Manager<Customer>.Instance.AdicionarIten(clienteNovo);
                    Manager<Customer>.Instance.SaveChanges();
                }
                if (opcao2 == "b")
                {
                    Console.WriteLine("Write the customer ID of the Customer you want to remove");
                    string idToRemove = Console.ReadLine();
                    Manager<Customer>.Instance.RemoverTudo(idToRemove);
                    Manager<Customer>.Instance.SaveChanges();

                };
                if (opcao2 == "c")
                {
                    foreach (Customer c in Manager<Customer>.Instance)
                    {
                        Console.WriteLine(c);
                    }
                };
                if (opcao2 == "d")
                {
                    Console.WriteLine("Write the ID of the customer you want to find");
                    string customerToFind = Console.ReadLine();
                    Console.WriteLine(Manager<Customer>.Instance.Find(customerToFind));
                        
                }

            }
            if (opcao1 == "b")
            {
                if (opcao2 == "a")
                {
                    Employee employeeNew = new Employee();
                    Console.WriteLine("Write the employee's name");
                    employeeNew.contactName = Console.ReadLine();
                    Console.WriteLine("write the employee's title");
                    employeeNew.contactTitle = Console.ReadLine();
                    Console.WriteLine("write the employee's title of courtesy");
                    employeeNew.titleOfCourtesy = Console.ReadLine();
                    Console.WriteLine("write the employee's address");
                    employeeNew.address = Console.ReadLine();
                    Console.WriteLine("write the employee's city");
                    employeeNew.city = Console.ReadLine();
                    Console.WriteLine("write the employee's country");
                    employeeNew.country = Console.ReadLine();
                    Console.WriteLine("write the employee's phone");
                    employeeNew.phone = Console.ReadLine();
                    Console.WriteLine("write the employee's birthday");
                    employeeNew.birthday = Console.ReadLine();
                    Console.WriteLine("does the employee have a photo?");
                    employeeNew.photo = Console.ReadLine();
                    Manager<Employee>.Instance.AdicionarIten(employeeNew);
                    Manager<Employee>.Instance.SaveChanges();
                }    
                if (opcao2 == "b")
                {
                    Console.WriteLine("Write the employeeID of the Employee you want to remove");
                    string idToRemove = Console.ReadLine();
                    Manager<Employee>.Instance.RemoverTudo(idToRemove);
                    Manager<Employee>.Instance.SaveChanges();
                }    
                if (opcao2 == "c")
                {
                    foreach (Employee c in Manager<Employee>.Instance)
                    {
                        Console.WriteLine(c);
                    }
                }
                if (opcao2 == "d")
                {
                    Console.WriteLine("Write the ID of the employee you want to find");
                    string employeeToFind = Console.ReadLine();
                    Console.WriteLine(Manager<Employee>.Instance.Find(employeeToFind));

                }
            }
            if (opcao1 == "c")
            {
                if (opcao2 == "a")
                {
                    Product productsNew = new Product();
                    Console.WriteLine("Write the product's name");
                    productsNew.productName = Console.ReadLine();
                    Console.WriteLine("write the product's title");
                    productsNew.productPrice = Console.ReadLine();
                    Manager<Product>.Instance.AdicionarIten(productsNew);
                    Manager<Product>.Instance.SaveChanges();


                }
                if (opcao2 == "b")
                {
                    Console.WriteLine("Write the productID of the Product you want to remove");
                    string idToRemove = Console.ReadLine();
                    Manager<Product>.Instance.RemoverTudo(idToRemove);
                    Manager<Product>.Instance.SaveChanges();
                }
                if (opcao2 == "c")
                {
                    foreach (Product c in Manager<Product>.Instance)
                    {
                        Console.WriteLine(c);
                    }
                }
                if (opcao2 == "d")
                {
                    Console.WriteLine("Write the ID of the product you want to find");
                    string productToFind = Console.ReadLine();
                    Console.WriteLine(Manager<Product>.Instance.Find(productToFind));

                }
            }
            if (opcao1 == "d")
            {
                if (opcao2 == "a")
                {
                    Supplier supplierNew = new Supplier();
                    Console.WriteLine("Write the supplier's name");
                    supplierNew.supplierName = Console.ReadLine();
                    Console.WriteLine("write the supplier's area of work");
                    supplierNew.areaOfWork = Console.ReadLine();
                    Console.WriteLine("write the supplier's country");
                    supplierNew.country = Console.ReadLine();
                    Manager<Supplier>.Instance.AdicionarIten(supplierNew);
                    Manager<Supplier>.Instance.SaveChanges();
                    
                }
                if (opcao2 == "b")
                {
                    Console.WriteLine("Write the supplier's ID you want to remove");
                    string idToRemove = Console.ReadLine();
                    Manager<Supplier>.Instance.RemoverTudo(idToRemove);
                    Manager<Supplier>.Instance.SaveChanges();

                }
                if (opcao2 == "c")
                {
                    foreach (Supplier c in Manager<Supplier>.Instance)
                    {
                        Console.WriteLine(c);
                    }
                }
                if (opcao2 == "d")
                {
                    Console.WriteLine("Write the ID of the supplier you want to find");
                    string supplierToFind = Console.ReadLine();
                    Console.WriteLine(Manager<Supplier>.Instance.Find(supplierToFind));

                }
            }


            
            

          


           
       }
    }
}
