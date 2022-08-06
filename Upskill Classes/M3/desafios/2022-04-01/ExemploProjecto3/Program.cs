
﻿using System;
using System.Collections.Generic;
using ExemploProjecto3.Operations;

namespace ExemploProjecto3
{
    public class Program
    {
        public delegate void Operation();

        static void Main(string[] args)
        {
            Dictionary<string, Operation> options = new Dictionary<string, Operation>();
            options.Add("1", new Operation(CustomerOperations.Instance.Add));
            options.Add("2", new Operation(CustomerOperations.Instance.Remove));
            options.Add("3", new Operation(CustomerOperations.Instance.Update));
            options.Add("4", new Operation(CustomerOperations.Instance.ShowAll));

            options.Add("5", new Operation(EmployeeOperations.Instance.Add));
            options.Add("6", new Operation(EmployeeOperations.Instance.Remove));
            options.Add("7", new Operation(EmployeeOperations.Instance.Update));
            options.Add("8", new Operation(EmployeeOperations.Instance.ShowAll));

            options.Add("9", new Operation(ProductOperations.Instance.Add));
            options.Add("10", new Operation(ProductOperations.Instance.Remove));
            options.Add("11", new Operation(ProductOperations.Instance.Update));
            options.Add("12", new Operation(ProductOperations.Instance.ShowAll));

            options.Add("13", new Operation(SupplierOperations.Instance.Add));
            options.Add("14", new Operation(SupplierOperations.Instance.Remove));
            options.Add("15", new Operation(SupplierOperations.Instance.Update));
            options.Add("16", new Operation(SupplierOperations.Instance.ShowAll));

            foreach (KeyValuePair<string, Operation> kvp in options)
            {
                Console.WriteLine($"{kvp.Key}) {kvp.Value.Target} {kvp.Value.Method.Name}");
            }
            Console.Write(".: ");
            string option = Console.ReadLine();
            options.GetValueOrDefault(option)?.DynamicInvoke();
        }
    }
}