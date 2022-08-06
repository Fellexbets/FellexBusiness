using System;
using System.Text.Json.Serialization;

#nullable enable

namespace RoadToDB
{
    [PathAttribute("shippers.json")]
    public class Shipper : Entity
    {
        [JsonPropertyName("shipperID")]
        public int ShipperId { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = null!;
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        public override string GetPrimaryKey() => ShipperId.ToString();

        public override string ToString() => string.Format("{0, 20} | {1, 20} | {2, 30}", ShipperId, CompanyName, Phone);

        public override string Header() => string.Format("{0, 20} | {1, 20} | {2, 30}\n----------------------------------------------------------------------------", "Shipper Id", "Company Name", "Phone");
    }
}
