using Project3API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Project3API.Models
{
    public class Shipper : Entity
    {
        
        
        [Required]
        public string CompanyName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phone { get; set; }

        
    }
}
