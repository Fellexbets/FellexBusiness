using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("regions.json")]
    public class Region : Entity
    {
        #region Properties

        [JsonPropertyName("territoryID")]
        public int TerritoryID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        
        
        
        


        #endregion
        public override string GetPrimaryKey() => TerritoryID.ToString();
        public override string ToString() => string.Format("{0, 10} | {1,40}", TerritoryID, Name);

        public override string Header() => string.Format("{0, 10} | {1,40}\n------------------------------------------------------------------------------------------------", "ID", "Name");

        
    }
}