using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Employee : Entity
    {
        public Employee()
        {
            InverseReportsToNavigation = new HashSet<Employee>();
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string PhotoPath { get; set; }

        public virtual Employee ReportsToNavigation { get; set; }
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString() => $"{LastName} {Title} {Country}";

        public override string GetPrimaryKey() => EmployeeId.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 40}\n-----------------------------------------------------------------------------------------------------------------", "Last Name", "Title", "Country");
    }
}
