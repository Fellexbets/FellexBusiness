using System;
using System.Text.Json.Serialization;

#nullable enable

namespace RoadToDB
{
    [PathAttribute("products.json")]
    public class Product : Entity
    {
        [JsonPropertyName("productID")]
        public int ProductId { get; set; }
        [JsonPropertyName("name")]
        public string ProductName { get; set; } = null!;
        [JsonPropertyName("supplierID")]
        public int? SupplierId { get; set; }
        [JsonPropertyName("categoryID")]
        public int? CategoryId { get; set; }
        [JsonPropertyName("quantityPerUnit")]
        public string? QuantityPerUnit { get; set; }
        [JsonPropertyName("unitPrice")]
        public decimal? UnitPrice { get; set; }
        [JsonPropertyName("unitsInStock")]
        public short? UnitsInStock { get; set; }
        [JsonPropertyName("unitsOnOrder")]
        public short? UnitsOnOrder { get; set; }
        [JsonPropertyName("reorderLevel")]
        public short? ReorderLevel { get; set; }
        [JsonPropertyName("discontinued")]
        public bool Discontinued { get; set; }

        public override string GetPrimaryKey() => ProductId.ToString();

        public override string ToString() => string.Format("{0, 20} | {1, 40} | {2, 40}", ProductId, ProductName, UnitPrice);

        public override string Header() => base.Header() + string.Format("{0, 20} | {1, 40} | {2, 40}", "Product Id", "Product Name", "Unit Price");
    }
}
