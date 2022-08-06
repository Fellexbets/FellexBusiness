using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ClassLibrary1
{
    [PathAttribute("StarterProducts.json")]
    public class Product
    {
        #region Properties
        [JsonPropertyName("productID")]
        public string productID { get; set; }

        [JsonPropertyName("productName")]
        public string productName { get; set; }

        [JsonPropertyName("productPrice")]
        public string productPrice { get; set; }
        #endregion

        public override string ToString() => productName + ", " + productPrice + " dollars";

    }
}
