using Project3API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Project3API.Models
{
    public class Product : Entity
    {
                
        [Required]
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
       
        public string QuantityPerUnit { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitPrice { get; set; }
        [Required]
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        
        public virtual Supplier Supplier { get; set; }
        
    }
}
