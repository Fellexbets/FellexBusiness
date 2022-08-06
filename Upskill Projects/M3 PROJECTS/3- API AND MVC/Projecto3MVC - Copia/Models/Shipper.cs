using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Projecto3MVC.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            
        }
        [Required]
        public int ShipperId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phone { get; set; }

        
    }
}
