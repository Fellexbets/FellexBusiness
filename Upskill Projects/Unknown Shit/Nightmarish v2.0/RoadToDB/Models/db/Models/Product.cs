using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("products.json")]
    public class Product : Entity
    {
        #region Properties

        [JsonPropertyName("productID")]
        public int ProductID { get; set; }
        [JsonPropertyName("supplierID")]
        public int SupplierID { get; set; }
        [JsonPropertyName("categoryID")]
        public int CategoryID { get; set; }
        [JsonPropertyName("quantityPerUnit")]
        public string QuantityPerUnit { get; set; }
        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
        [JsonPropertyName("unitsInStock")] 
        public int UnitsInStock { get; set; }
        [JsonPropertyName("unitsOnOrder")]
        public int UnitsOnOrder { get; set; }
        [JsonPropertyName("reorderLevel")]
        public int ReorderLevel { get; set; }
        [JsonIgnore]
        [JsonPropertyName("discontinued")]
        public bool Discontinued { get; set; }
        
        [JsonPropertyName("name")]
        public object Name { get; set; }
        
        
        
        


        #endregion
        public override string GetPrimaryKey() => ProductID.ToString();
        public override string ToString() => string.Format("{0, 10} | {1,40} | {2,40}", ProductID, Name, UnitsInStock);

        public override string Header() => string.Format("{0, 10} | {1,40} | {2,40}\n------------------------------------------------------------------------------------------------", "ID", "Name", "Units in Stock");

        
    }
}