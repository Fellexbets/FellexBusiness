using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("orders.json")]
    public class Order : Entity
    {
        #region Properties

        [JsonPropertyName("orderID")]
        public int OrderID { get; set; }
        [JsonPropertyName("customerID")]
        public string CustomerID { get; set; }
        [JsonPropertyName("employeeID")]
        public int EmployeeID { get; set; }
        [JsonPropertyName("orderDate")]
        public string OrderDate { get; set; }
        [JsonPropertyName("requiredDate")]
        public string RequiredDate { get; set; }
        [JsonPropertyName("shippedDate")] 
        public string ShippedDate { get; set; }
        [JsonPropertyName("shipVia")]
        public int ShipVia { get; set; }
        [JsonPropertyName("freight")]
        public decimal freight { get; set; }
        [JsonPropertyName("shipName")]
        public string ShipName { get; set; }
        [JsonIgnore]
        [JsonPropertyName("shipAdress")]
        public object shipAdress { get; set; }
        [JsonIgnore]
        [JsonPropertyName("details")]
        
        public object details { get; set; }
        
        
        


        #endregion
        public override string GetPrimaryKey() => OrderID.ToString();
        public override string ToString() => string.Format("{0, 10} | {1,40} | {2,40}", OrderID, ShipName, ShippedDate);

        public override string Header() => string.Format("{0, 10} | {1,40} | {2,40}\n------------------------------------------------------------------------------------------------", "ID", "Ship Name", "Ship date");

        
    }
}