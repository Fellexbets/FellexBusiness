using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    public class Customer
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("companyName")]
        public string companyName { get; set; }

        [JsonPropertyName("contactTitle")]
        public string contactTitle { get; set; }

        [JsonPropertyName("address")]
        public string address { get; set; }

        [JsonPropertyName("city")]
        public string city { get; set; }

        [JsonPropertyName("region")]
        public string region { get; set; }

        [JsonPropertyName("postalCode")]
        public string postalCode { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("phone")]
        public string phone { get; set; }

        [JsonPropertyName("fax")]
        public string fax { get; set; }
    }
}