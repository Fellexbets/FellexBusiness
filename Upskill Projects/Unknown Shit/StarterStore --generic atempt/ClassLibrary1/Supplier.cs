using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [PathAttribute("Suppliers.json")]
    public class Supplier
    {
        [JsonPropertyName("supplierID")]
        public string supplierID { get; set; }

        [JsonPropertyName("supplierName")]
        public string supplierName { get; set; }
        [JsonPropertyName("areaOfWork")]
        public string areaOfWork { get; set; }
        [JsonPropertyName("country")]
        public string country { get; set; }

        public override string ToString() => supplierName + ", especialized in" + areaOfWork + ", from " + country;
    }
}
