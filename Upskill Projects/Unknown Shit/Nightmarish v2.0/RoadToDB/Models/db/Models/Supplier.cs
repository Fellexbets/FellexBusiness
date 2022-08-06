using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("suppliers.json")]
    public class Supplier : Entity
    {
        #region Properties

        [JsonPropertyName("supplierID")]
        public int SupplierID { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("contactName")]
        public string ContactName { get; set; }
        [JsonPropertyName("contactTitle")]
        public string ContactTitle { get; set; }
        
        [JsonPropertyName("address")]
        public Adress Adress { get; set; }
       







        #endregion
        public override string GetPrimaryKey() => SupplierID.ToString();
        public override string ToString() => string.Format("{0, 10} | {1,40}", SupplierID, CompanyName);

        public override string Header() => string.Format("{0, 10} | {1,40}\n------------------------------------------------------------------------------------------------", "ID", "Name");

        
    }
}