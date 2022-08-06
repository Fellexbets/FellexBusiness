using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("customers.json")]
    public class Customer : Entity
    {
        #region Properties

        [JsonPropertyName("id")]
        public string CustomerID { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("contactName")]
        public string ContactName { get; set; }
        [JsonPropertyName("contactTitle")]
        public string ContactTitle { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("city")] 
        public string City { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("fax")]
        public string Fax { get; set; }

        #endregion

        public override string ToString() => string.Format("{0, 10} | {1,40} | {2,40}", CustomerID, CompanyName, ContactName);

        public override string Header() => base.Header() + string.Format("{0, 10} | {1,40} | {2,40}", "Customer ID", "Company Name", "Contact Name");

        public override string GetPrimaryKey() => CustomerID;
    }
}