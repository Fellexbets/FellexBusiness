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

        private static string SelectedOperation;

        private delegate void OperationToExecute();

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
            OperationsDict.Add(90, new OperationToExecute(SoundOperations.PlaySound)); 
            OperationsDict.Add(91, new OperationToExecute(SoundOperations.STFU));

            // Initialize Entities
            EntitiesDict.Add(1, Manager<Customer>.Instance);
            EntitiesDict.Add(2, Manager<Category>.Instance);
            EntitiesDict.Add(3, Manager<Employee>.Instance);
            EntitiesDict.Add(4, Manager<Order>.Instance);
            EntitiesDict.Add(5, Manager<Product>.Instance);
            EntitiesDict.Add(6, Manager<Shipper>.Instance);
            EntitiesDict.Add(7, Manager<Supplier>.Instance);
        }

        private static void ResetSelectedEntity() => SelectedEntity = null;

        public static void ChooseOperation() 
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please select an operation:");
            foreach (KeyValuePair<int, Delegate> ele1 in OperationsDict)
            {
                Console.WriteLine("{0}) {1}", ele1.Key, ele1.Value.Method.Name);
            }
            Console.Write(".: ");
            Int32.TryParse(Console.ReadLine(), out int operation);
            Console.WriteLine();
            SelectedOperation = OperationsDict.GetValueOrDefault(operation)?.Method.Name;
            OperationsDict.GetValueOrDefault(operation)?.DynamicInvoke();
            ResetSelectedEntity();
        }

        public static void SelectEntities()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            while (SelectedEntity == null)
            {
                Console.WriteLine($"Which entity should I {SelectedOperation}?");
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
                string optional = Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null ? "Optional" : "Mandatory";
                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                try
                {
                    Console.WriteLine($"Value for: {propertyInfo.Name}? (hint: {optional})");
                    string propValue = Console.ReadLine();
                    object safeValue = string.IsNullOrEmpty(propValue) ? null : Convert.ChangeType(propValue, t);
                    propertyInfo.SetValue(o, safeValue, null);
                }
                catch (InvalidCastException ice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: This conversion is not supported - {ice.Message}");
                }
                catch (FormatException fe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Value is not in a format recognized by conversionType - {fe.Message}");
                }
                catch (OverflowException oe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Value represents a number that is out of the range of conversionType - {oe.Message}");
                }
                catch (ArgumentNullException ane)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: ConversionType is null - {ane.Message}");
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return o;
        }
    }
}
