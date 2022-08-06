using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class OrderDetail : Entity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public override string ToString() => $"{OrderId} {ProductId} {UnitPrice}";

        public override string GetPrimaryKey() => OrderId.ToString() + ProductId.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 40}\n-----------------------------------------------------------------------------------------------------------------", "Order ID", "Product ID", "Unit Price");
    }
}
