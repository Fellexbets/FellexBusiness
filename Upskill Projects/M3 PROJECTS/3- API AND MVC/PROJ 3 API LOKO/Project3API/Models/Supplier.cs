using Project3API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project3API.Models
{
    public partial class Supplier : Entity
    {
        
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        
        public string City { get; set; }
        [Required]

        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phone { get; set; }
       

        public virtual ICollection<Product> Products { get; set; }
    }
}
