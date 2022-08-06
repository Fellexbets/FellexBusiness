using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("shippers.json")]
    public class Shipper : Entity
    {
        #region Properties

        [JsonPropertyName("shipperID")]
        public int ShipperID { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("phone")]
        public string phone { get; set; }







        #endregion
        public override string GetPrimaryKey() => ShipperID.ToString();
        public override string ToString() => string.Format("{0, 10} | {1,40}", ShipperID, CompanyName);

        public override string Header() => string.Format("{0, 10} | {1,40}\n------------------------------------------------------------------------------------------------", "ID", "Name");

        
    }
}