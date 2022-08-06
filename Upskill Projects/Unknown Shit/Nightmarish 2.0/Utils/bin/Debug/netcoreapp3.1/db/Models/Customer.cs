using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Customer : Entity
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString() => $"{CustomerId} {ContactName} {Country}";

        public override string GetPrimaryKey() => CustomerId.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 40}\n-----------------------------------------------------------------------------------------------------------------", "Customer ID", "Contact Name", "Country");
    }
}
