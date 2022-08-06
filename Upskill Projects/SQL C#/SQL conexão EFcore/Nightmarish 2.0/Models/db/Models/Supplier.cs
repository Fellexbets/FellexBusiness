using System;
using System.Text.Json.Serialization;

#nullable enable

namespace RoadToDB
{
    [PathAttribute("suppliers.json")]
    public class Supplier : Entity
    {
        [JsonPropertyName("supplierID")]
        public int SupplierId { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = null!;
        [JsonPropertyName("contactName")]
        public string? ContactName { get; set; }
        [JsonPropertyName("contactTitle")]
        public string? ContactTitle { get; set; }
                

        public override string GetPrimaryKey() => SupplierId.ToString();

        public override string ToString() => string.Format("{0, 20} | {1, 40} | {2, 40}", SupplierId, CompanyName, ContactName);

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 40}\n-----------------------------------------------------------------------------------------------------------------", "Supplier Id", "Company Name", "Contact Name");
    }
}
