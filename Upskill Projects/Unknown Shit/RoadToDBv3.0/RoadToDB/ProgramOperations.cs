using System;
using System.Collections.Generic;
using System.Reflection;

namespace RoadToDB
{
    public class ProgramOperations
    {
        public static ICrud<Entity> SelectedEntity { get; private set; }
        public static Dictionary<int, ICrud<Entity>> EntitiesDict { get; private set; }
        public static Dictionary<int, Delegate> OperationsDict { get; private set; }

        public delegate void OperationToExecute();

        static ProgramOperations()
        {
            OperationsDict = new Dictionary<int, Delegate>();
            EntitiesDict = new Dictionary<int, ICrud<Entity>>();

            // Initialize Operations
            OperationsDict.Add(1, new OperationToExecute(Operations.Create));
            OperationsDict.Add(2, new OperationToExecute(Operations.Read));
            OperationsDict.Add(3, new OperationToExecute(Operations.Update));
            OperationsDict.Add(4, new OperationToExecute(Operations.Delete));
            OperationsDict.Add(5, new OperationToExecute(Operations.Find));
            OperationsDict.Add(6, new OperationToExecute(Operations.Save));
            OperationsDict.Add(7, new OperationToExecute(Operations.SaveAll));
            OperationsDict.Add(8, new OperationToExecute(Operations.Close));

            // Initialize Entities
            EntitiesDict.Add(1, Manager<Customer>.Instance);
            EntitiesDict.Add(2, Manager<Category>.Instance);
            EntitiesDict.Add(3, Manager<Employee>.Instance);
            EntitiesDict.Add(4, Manager<Order>.Instance);
            EntitiesDict.Add(5, Manager<Product>.Instance);
            EntitiesDict.Add(6, Manager<Shipper>.Instance);
            EntitiesDict.Add(7, Manager<Supplier>.Instance);
        }

        public static void ResetSelectedEntity() => SelectedEntity = null;

        public static void ChooseOperation() 
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Please select an operation:");
            foreach (KeyValuePair<int, Delegate> ele1 in OperationsDict)
            {
                Console.WriteLine("{0}) {1}", ele1.Key, ele1.Value.Method.Name);
            }
            Console.Write(".: ");
            Int32.TryParse(Console.ReadLine(), out int operation);
            Console.WriteLine();
            OperationsDict.GetValueOrDefault(operation)?.DynamicInvoke();
            ResetSelectedEntity();
        }

        public static void SelectEntities()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            while (SelectedEntity == null)
            {
                Console.WriteLine("Please select an entity:");
                foreach (KeyValuePair<int, ICrud<Entity>> ele1 in EntitiesDict)
                {
                    Console.WriteLine("{0}) {1}", ele1.Key, ele1.Value.TypeOfT());
                }
                Console.Write(".: ");
                Int32.TryParse(Console.ReadLine(), out int option);
                SelectedEntity = EntitiesDict.GetValueOrDefault(option);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static Entity CreateNewEntityFromConsoleInput(ICrud<Entity> selected)
        {
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
    }
}
