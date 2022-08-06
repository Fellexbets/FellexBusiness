using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Order : Entity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Shipper ShipViaNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public override string ToString() => $"{OrderId} {ShipName} {ShippedDate}";

        public override string GetPrimaryKey() => OrderId.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 40}\n-----------------------------------------------------------------------------------------------------------------", "Order ID", "ShipName", "Shipped Date");
    }
}
