using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Text;
using EFCore.Models;
using System.Diagnostics.CodeAnalysis;

namespace EFCore
{
    public static class ProgramOperations
    {
        public static Type SelectedEntity { get; private set; }

        public static string SelectedExercise { get; private set; }
        public static Dictionary<int, Type> EntitiesDict { get; private set; }
        public static Dictionary<int, Delegate> OperationsDict { get; private set; }

        public static Dictionary<int, string> ExercisesDict { get; private set; }

        public delegate void OperationToExecute();

        

        static ProgramOperations()
        {
            

            OperationsDict = new Dictionary<int, Delegate>();
            EntitiesDict = new Dictionary<int, Type>();
            ExercisesDict = new Dictionary<int, string>();

            
            OperationsDict.Add(1, new OperationToExecute(Operations.CreateIten));
            OperationsDict.Add(2, new OperationToExecute(Operations.ListAll));
            OperationsDict.Add(3, new OperationToExecute(Operations.RemoveIten));
            OperationsDict.Add(4, new OperationToExecute(Operations.Update));
            OperationsDict.Add(5, new OperationToExecute(Operations.BestOfTheBest));
            OperationsDict.Add(6, new OperationToExecute(Operations.SQLexercises));
            OperationsDict.Add(7, new OperationToExecute(Operations.Close));

            
            EntitiesDict.Add(1, typeof(Customer));
            EntitiesDict.Add(2, typeof(Employee));
            EntitiesDict.Add(3, typeof(Product));
            EntitiesDict.Add(4, typeof(Shipper));
            EntitiesDict.Add(5, typeof(Supplier));


            ExercisesDict.Add(1, "exercício 1");
            ExercisesDict.Add(2, "exercício 3");
            ExercisesDict.Add(3, "exercício 4");
            ExercisesDict.Add(4, "exercício 6");
            ExercisesDict.Add(5, "exercício 7");
            ExercisesDict.Add(6, "exercício 10");
            ExercisesDict.Add(7, "exercício 12");
            ExercisesDict.Add(8, "exercício 14");
            ExercisesDict.Add(9, "exercício 16");
            ExercisesDict.Add(10, "exercício 17");
            ExercisesDict.Add(11, "exercício 21");
            ExercisesDict.Add(12, "exercício 22");
            ExercisesDict.Add(13, "exercício 23");
        }

        public static void ResetSelectedEntity() => SelectedEntity = null;
        public static void ResetSelectedExercise() => SelectedExercise = null;

        public static void ChooseOperation() 
        {
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
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
            ResetSelectedExercise();
        }
        public static void SelectEntities()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            while (SelectedEntity == null)
            {
                Console.WriteLine("Please select an entity:");
                foreach (KeyValuePair<int, Type> ele1 in EntitiesDict)
                {
                    Console.WriteLine("{0}) {1}", ele1.Key, ele1.Value.Name);
                }
                Console.Write(".: ");
                Int32.TryParse(Console.ReadLine(), out int option);
                SelectedEntity = EntitiesDict.GetValueOrDefault(option);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SelectExercicio()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            while (SelectedExercise == null)
            {
                Console.WriteLine("Please select an exercise:");
                foreach (KeyValuePair<int, string> ele1 in ExercisesDict)
                {
                    Console.WriteLine("{0}) {1}", ele1.Key, ele1.Value);
                }
                Console.Write(".: ");
                SelectedExercise = (Console.ReadLine());
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
