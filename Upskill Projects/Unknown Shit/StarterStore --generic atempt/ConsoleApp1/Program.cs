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
            EmployeeManager employees = new EmployeeManager();
            ProductsManager products = new ProductsManager();
            SupplierManager suppliers = new SupplierManager();
            
            Console.WriteLine("Select what you want to work with: a) Customers, b)Employees, c)Products, d)Suppliers");
            string opcao1 = Console.ReadLine();
            Console.WriteLine("Select the operation you want to perform: a) add, b) remove, c) showall");
            string opcao2 = Console.ReadLine();

            if (opcao1 == "a")
            {
                if (opcao2 == "a")
                {
                    Customer customer1 = new Customer();
                    Console.WriteLine("Write the customers name");
                    customer1.contactName = Console.ReadLine();
                    Console.WriteLine("write the customers title");
                    customer1.contactTitle = Console.ReadLine();
                    Console.WriteLine("write the customers address");
                    customer1.address = Console.ReadLine();
                    Console.WriteLine("write the customers city");
                    customer1.address = Console.ReadLine();
                    Console.WriteLine("write the customers country");
                    customer1.address = Console.ReadLine();
                    Console.WriteLine("write the customers phone");
                    customer1.address = Console.ReadLine();
                    Console.WriteLine("write the customers fax");
                    customer1.address = Console.ReadLine();
                    Manager<Customer>.Instance.AdicionarClientes(customer1);
                    Manager<Customer>.Instance.SaveToFile();
                }
                if (opcao2 == "b")
                {
                    

                };
                if (opcao2 == "c")
                {
                    Manager<T>.Instance.ShowAllCustomers;
                };

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
                    Console.WriteLine("write the employee's address");
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
                    employees.AdicionarEmpregados(employeeNew);
                    employees.SaveToFile();
                }    
                if (opcao2 == "b")
                {

                }    
                if (opcao2 == "c")
                {
                    foreach (Employee c in employees)
                    {
                        Console.WriteLine(c);
                    }
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
                    products.AdicionarProdutos(productsNew);
                    products.SaveToFile();
                    
                    
                }
                if (opcao2 == "b")
                {

                }
                if (opcao2 == "c")
                {
                    foreach (Product c in products)
                    {
                        Console.WriteLine(c);
                    }
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
                    suppliers.AdicionarSupplier(supplierNew);
                    suppliers.SaveToFile();
                    
                }
                if (opcao2 == "b")
                {
                    Console.WriteLine("Write the supplier's ID you want to remove");
                    Supplier newSup = new Supplier();
                    string ID = Console.ReadLine();
                    
                    

                }
                if (opcao2 == "c")
                {
                    foreach (Supplier c in suppliers)
                    {
                        Console.WriteLine(c);
                    }
                }
            }


            
            

          


           
       }
    }
}
