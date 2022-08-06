using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Supplier : Entity, IHasPrimaryKey
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
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
        public string HomePage { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString() => $"{CompanyName} {Country} {Phone}";

        public override string GetPrimaryKey() => SupplierId.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 40}\n-----------------------------------------------------------------------------------------------------------------", "Company Name", "Country", "Phone");
    }
}
