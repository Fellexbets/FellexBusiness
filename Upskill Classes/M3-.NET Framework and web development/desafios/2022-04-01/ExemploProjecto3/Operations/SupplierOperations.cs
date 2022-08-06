using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploProjecto3.Operations
{
    public class SupplierOperations : IOperations
    {
        public static SupplierOperations Instance { get; set; }

        static SupplierOperations()
        {
            Instance = new SupplierOperations();
        }

        private SupplierOperations() { }

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
