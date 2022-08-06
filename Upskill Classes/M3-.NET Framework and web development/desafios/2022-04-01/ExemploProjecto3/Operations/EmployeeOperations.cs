using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploProjecto3.Operations
{
    public class EmployeeOperations : IOperations
    {
        public static EmployeeOperations Instance { get; set; }

        static EmployeeOperations()
        {
            Instance = new EmployeeOperations();
        }

        private EmployeeOperations() { }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void ShowAll()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}

