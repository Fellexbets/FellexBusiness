﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace RoadToDB
{
    public class ProgramOperations
    {
        public static ICrud<Entity> SelectedEntity { get; private set; }

        // both dictionaries are created here, with the parameters specified beforehand
        public static Dictionary<int, ICrud<Entity>> EntitiesDict { get; private set; }
        public static Dictionary<int, Delegate> OperationsDict { get; private set; }

        public delegate void OperationToExecute();

        static ProgramOperations()
        {
            OperationsDict = new Dictionary<int, Delegate>();
            EntitiesDict = new Dictionary<int, ICrud<Entity>>();

            // by using two dictionaries we can easly add a new operation or entity here
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
        // the Method ChooseOperation() is where the program starts: differently from the previous version, now we can choose first the operation and then the entity to work with. This makes more sense when we think about closing or saving all. 
        public static void ChooseOperation() 
        {
            Console.WriteLine();
            // this is a nice way to set the color of the program text
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Please select an operation:");
            // a dictionary pairing integers with delegate type values
            foreach (KeyValuePair<int, Delegate> ele1 in OperationsDict)
            {
                Console.WriteLine("{0}) {1}", ele1.Key, ele1.Value.Method.Name);
            }
            Console.Write(".: ");
            Int32.TryParse(Console.ReadLine(), out int operation);
            Console.WriteLine();
            // this new part bellow is hard, i search about dynamicinvoke
            // HEY i am back, turns out the dynamicInvoke method is part of the delegate function, invoking the current method represented by the current delegate, maybe this is another reason to use delegates?
            OperationsDict.GetValueOrDefault(operation)?.DynamicInvoke();
            // makes "SelectedEntity" NULL again
            ResetSelectedEntity();
        }

        public static void SelectEntities()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            // this is the next setp: after choosing an operation we can choose an entity to work with
            // while nothing is selected, the program shows all entities available 
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
        // old method
        // this method is used to create a new entity from the users input, it first
        // through reflection, GetType().GetProperties() - it creates an array with all the properties with the right types
        // and after that, it asks the user for each property, storing the answers deppeding on them beeing integers or strings.



        // new method testing if null through new reflection methods
        // each property in the foreach is assigned to the variable t
        // the GetUnderlyingType method is used to get the underlying type of variables that can be null
        // if the string can be null and is empty, we add a NULL value to it
        // if the underlyingtype is NULL by itself we convert it to null?
        public static Entity CreateNewEntityFromConsoleInput(ICrud<Entity> selected)
        {
            Entity o = selected.CreateNewOfT();
            PropertyInfo[] propertyInfos = o.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Type t = propertyInfo.PropertyType;
                string propValue = null;
                if (Nullable.GetUnderlyingType(t) != null)
                {
                    Console.WriteLine($"The property {propertyInfo.Name} is optional.");
                    t = Nullable.GetUnderlyingType(t);
                    propValue = Console.ReadLine();
                    if (String.IsNullOrEmpty(propValue))
                    {
                        propertyInfo.SetValue(o, null, null);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine(propertyInfo.Name + "?");
                    propValue = Console.ReadLine();
                }
                propertyInfo.SetValue(o, Convert.ChangeType(propValue, t), null);
            }
            return o;
        }
    }
}
