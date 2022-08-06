using System.Text.Json.Serialization;

namespace RoadToDB
{
    
    public class Adress : Entity
    {
        #region Properties

        
        public string street { get; set; }
        
        public string city { get; set; }
       
        public string region { get; set; }
        [JsonIgnore]
        public int postalCode { get; set; }
        public string country { get; set; }

        public string phone { get; set; }


        #endregion



        public override string Header() => string.Format("{0, 20}\n------------------------------------------------------------------------------------------------", "Phone");

        public override string GetPrimaryKey() => phone;
    }
}