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

        [JsonPropertyName("address")]
        public Address? AddressProp { get; set; }

        public override string GetPrimaryKey() => SupplierId.ToString();

        public override string ToString() => string.Format("{0, 20} | {1, 40} | {2, 30} | {3, 10}", SupplierId, CompanyName, ContactName, AddressProp?.Country);

        public override string Header() => base.Header() + string.Format("{0, 20} | {1, 40} | {2, 30} | {3, 10}", "Supplier Id", "Company Name", "Contact Name", "Country");
    }
}
