using Project3API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project3API.Models
{
    public class Customer : Entity
    {
                
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(40, MinimumLength = 3)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Contact Title")]
        public string ContactTitle { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        
        public string Region { get; set; }
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Fax { get; set; }

       
    }
}
