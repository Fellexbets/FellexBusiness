using System;
using System.Text.Json.Serialization;

namespace ClassLibrary1
{
    [PathAttribute("DB/Customers.json")]
    public class Customer : IHasPrimaryKey
    {
        #region Properties
        [JsonPropertyName("customerID")]
        public string customerID { get; set; }

        [JsonPropertyName("contactName")]
        public string contactName { get; set; }

        [JsonPropertyName("contactTitle")]
        public string contactTitle { get; set; }

        [JsonPropertyName("address")]
        public string address { get; set; }

        [JsonPropertyName("city")]
        public string city { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("phone")]
        public string phone { get; set; }

        [JsonPropertyName("fax")]
        public string fax { get; set; }
        #endregion


        public override string ToString() => contactName + ", from " + country;

        public string GetPrimaryKey() => customerID;


    }
}
