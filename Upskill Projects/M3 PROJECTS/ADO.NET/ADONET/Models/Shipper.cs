using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        
    }
}
