using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploProjecto3.Operations
{
    public class ProductOperations : IOperations
    {
        public static ProductOperations Instance { get; set; }

        static ProductOperations()
        {
            Instance = new ProductOperations();
        }

        private ProductOperations() { }

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
