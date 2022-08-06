using System;
using System.Text.Json.Serialization;

#nullable enable

namespace RoadToDB
{
    [PathAttribute("orders.json")]
    public class Order : Entity
    {
        [JsonPropertyName("orderID")]
        public int OrderId { get; set; }
        
        [JsonPropertyName("customerID")]
        public string CustomerId { get; set; } = null!;
        
        [JsonPropertyName("employeeID")]
        public int? EmployeeId { get; set; }
        
        [JsonPropertyName("orderDate")]
        public DateTime? OrderDate { get; set; }

        [JsonIgnore]
        [JsonPropertyName("requiredDate")]
        public DateTime? RequiredDate { get; set; }
        
        [JsonIgnore]
        [JsonPropertyName("shippedDate")]
        public DateTime? ShippedDate { get; set; }
        
        [JsonPropertyName("shipVia")]
        public int? ShipVia { get; set; }
        
        [JsonPropertyName("freight")]
        public decimal? Freight { get; set; }
        
        [JsonPropertyName("shipName")]
        public string? ShipName { get; set; }

        public override string GetPrimaryKey() => OrderId.ToString();

        public override string ToString() => string.Format("{0, 20} | {1, 20} | {2, 30}", OrderId, OrderDate, ShipName);

        public override string Header() => string.Format("{0, 20} | {1, 20} | {2, 30}\n----------------------------------------------------------------------------", "OrderId", "Order Date", "Ship Name");
    }
}
