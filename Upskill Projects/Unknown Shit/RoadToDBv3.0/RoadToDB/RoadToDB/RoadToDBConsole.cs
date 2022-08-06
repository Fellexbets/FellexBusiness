using System;
using System.Collections.Generic;
using System.Reflection;

namespace RoadToDB
{
    public class RoadToDBConsole
    {
        public static Entity CreateNewEntityFromConsoleInput(ICrud<Entity> selected)
        {
            // Reflection: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection
            Entity o = selected.CreateNewOfT();
            PropertyInfo[] propertyInfos = o.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Console.WriteLine(propertyInfo.Name + "?");
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
            }
            return o;
        }

        public static int GetAllOperations()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==========================");
            Console.WriteLine("==== Select Operation ====");
            Console.WriteLine("==========================");

            // Enumeration types: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
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
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==========================");
            Console.WriteLine("==== Select an Entity ====");
            Console.WriteLine("==========================");

            // Dictionary<TKey,TValue> Class: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-6.0
            Dictionary<int, ICrud<Entity>> Options = new Dictionary<int, ICrud<Entity>>();
            Options.Add(1, Manager<Customer>.Instance);
            Options.Add(2, Manager<Category>.Instance);
            foreach (KeyValuePair<int, ICrud<Entity>> ele1 in Options)
            {
                Console.WriteLine("{0} for {1}", ele1.Key, ele1.Value.TypeOfT());
            }
            Int32.TryParse(Console.ReadLine(), out int option);
            ICrud<Entity> selected = Options[option];

            Console.WriteLine("==========================");
            return selected;
        }
    }
}
