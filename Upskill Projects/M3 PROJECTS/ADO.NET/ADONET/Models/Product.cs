using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Models
{
    public partial class Product
    {
        public Product()
        {
            
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        
        public virtual Supplier Supplier { get; set; }


        public override string ToString() => ($"{ProductId}: {ProductName}, its priced at {UnitPrice} $. We have {UnitsInStock} of those in stock. ");
    }
}
