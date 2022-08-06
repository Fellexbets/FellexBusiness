﻿using System.Text.Json.Serialization;

namespace DemoJSONApp
{
    public class Customer
    {
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
    }
}