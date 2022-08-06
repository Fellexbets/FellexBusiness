using System.Text.Json.Serialization;

namespace RoadToDB
{
    public class Address
	{
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonIgnore]
        [JsonPropertyName("postalCode")]
        public int PostalCode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonIgnore]
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        public override string ToString() => Country;
    }
}