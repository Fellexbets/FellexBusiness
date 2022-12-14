using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Projecto3MVC.Models
{
    public partial class Employee
    {
        public Employee()
        {
           
           
        }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }
        
        public string Address { get; set; }
        public string City { get; set; }
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        public byte[] Photo { get; set; }
        

       
        
    }
}
