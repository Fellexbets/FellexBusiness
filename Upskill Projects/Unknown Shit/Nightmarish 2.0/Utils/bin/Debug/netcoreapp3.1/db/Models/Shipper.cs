using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Shipper : Entity
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString() => $"{CompanyName} {Phone}";

        public override string GetPrimaryKey() => ShipperId.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40}\n-----------------------------------------------------------------------------------------------------------------", "Company Name", "Phone");
    }
}
