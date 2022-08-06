using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    public class Product 
    { 
        public int ProductID { get; set; } 
        public string ProductName { get; set; }

        [Column("UnitPrice")]
        public decimal? Cost { get; set; }
        
        [Column("UnitsInStock")]
        public short? Stock { get; set; }

        public bool Discontinued { get; set; }

        public override string ToString() => $"{ProductName} {Cost} {Stock}";
    }
}