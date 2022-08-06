using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    public class Product : Entity
    { 
        public int ProductID { get; set; } 
        public string ProductName { get; set; }

        [Column("UnitPrice")]
        public decimal? Cost { get; set; }
        
        [Column("UnitsInStock")]
        public short? Stock { get; set; }

        public bool Discontinued { get; set; }

        public override string ToString() => $"{ProductName} {Cost} {Stock}";

        public override string GetPrimaryKey() => ProductID.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 40}\n-----------------------------------------------------------------------------------------------------------------", "Product nAME", "Product Cost", "Stock Available");


    }
}