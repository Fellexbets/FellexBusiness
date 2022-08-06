using System;
using System.Collections.Generic;
using System.Reflection;

namespace RoadToDB
{
    public class RoadToDBConsole
    {
        public static Entity CreateNewEntityFromConsoleInput(ICrud<Entity> selected)
        {   //this method is used to create a new entity based on what the user input. This can be used either to add a new object or to update one. This method is prepared to covert the input string to an integer if the property is an integer.
            // Reflection: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection
            // the section bellow creates a new generic instance T of the selected entity
            Entity o = selected.CreateNewOfT();
            // PropertyInfo is a predefined class of the System used to get the attributes of a property;
            // The properties are gathered from the selected entity through the gettype() and getproperties() methods and are stored in the propertyInfos array;
            PropertyInfo[] propertyInfos = o.GetType().GetProperties();
            // the foreach bellow asks the user input for each porperty name of the propertyinfoarray;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Console.WriteLine(propertyInfo.Name + "?");
                // only two types are accepted and cared for here: strings and integers. If I would like to have object properties, i would have to add a new "if" covering it.
                // if the propertyinfo type is a string, the Console.ReadLine is atributed to the variable "propvalue" and the new value is setted through the SetValue method.
                // if the propertyinfo is an integer, the string typed by the user is converted to an integer through the Int32.TryParse method.
                Type t = propertyInfo.PropertyType;
                if (t == typeof(string))
                {
                    string propValue = Console.ReadLine();
                    propertyInfo.SetValue(o, propValue, null);
                }
                if (t == typeof(int))
                {
                    Int32.TryParse(Console.ReadLine(), out int propValue);
                    propertyInfo.SetValue(o, propValue, null);
                }
                if (t == typeof(Adress))
                {
                    Object address = Activator.CreateInstance(typeof(Adress));
                    PropertyInfo[] adressInfos = t.GetProperties();

                    foreach(PropertyInfo addrInfo in adressInfos)
                    {
                        Console.WriteLine(addrInfo.Name + "?");
                        string propValue = Console.ReadLine();

                        addrInfo.SetValue(address, propValue, null);
                    }
                }
            }
            return o;
        }

        public static int GetAllOperations()
            // this method is used to get all operations available in the "operation" section and present it to the user
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==========================");
            Console.WriteLine("==== Select Operation ====");
            Console.WriteLine("==========================");

            // Enumeration types: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
            // the operations are enumerated in a classy way, the first one is defined by = 1, the others are automatically defined by their position in the string.(2,3,4,5,etc)
            foreach (string operation in Enum.GetNames(typeof(Operation)))
            {
                Console.WriteLine("{0} for {1}", (int)Enum.Parse(typeof(Operation), operation), operation);
            }
            Int32.TryParse(Console.ReadLine(), out int selectedOperation);

            Console.WriteLine("==========================");
            Console.ForegroundColor = ConsoleColor.White;
            return selectedOperation;
        }

        public static ICrud<Entity> GetAllEntities()
            // this method is designed to get all the entitys available to work with
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==========================");
            Console.WriteLine("==== Select an Entity ====");
            Console.WriteLine("==========================");

            // Dictionary<TKey,TValue> Class: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-6.0
            // a dictionary is created, the first input <Tkey> represents the type of the keys in the dictionary and second input represents the type of the value. In our case the type of keys is integers and the type of values are Entity Icrud instances.
            Dictionary<int, ICrud<Entity>> Options = new Dictionary<int, ICrud<Entity>>();
            Options.Add(1, Manager<Customer>.Instance);
            Options.Add(2, Manager<Category>.Instance);
            Options.Add(3, Manager<Employee>.Instance);
            Options.Add(4, Manager<Order>.Instance);
            Options.Add(5, Manager<Product>.Instance);
            Options.Add(6, Manager<Region>.Instance);
            Options.Add(7, Manager<Shipper>.Instance);
            Options.Add(8, Manager<Supplier>.Instance);
            // we had 2 entrys on the dictionary, i added the employee and will add more Models once they are ready (products, orders, etc)
            foreach (KeyValuePair<int, ICrud<Entity>> ele1 in Options)
            {
                Console.WriteLine("{0} for {1}", ele1.Key, ele1.Value.TypeOfT());
            }
            // this statements bellow convert the user input from string to integer so they can be read by the dictionary "options", he takes integers as values. 
            Int32.TryParse(Console.ReadLine(), out int option);
            ICrud<Entity> selected = Options[option];

            Console.WriteLine("==========================");
            return selected;
        }
    }
}
