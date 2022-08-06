using System.Text.Json.Serialization;

namespace RoadToDB
{
    
    public class ShipAdress : Entity
    {
        #region Properties

        
        public string street { get; set; }
        
        public string city { get; set; }
       
        public string region { get; set; }
        public int postalCode { get; set; }
        public string country { get; set; }




        #endregion
        public override string Header() => string.Format("{0, 20}\n------------------------------------------------------------------------------------------------", "PostalCode");

        public override string GetPrimaryKey() => postalCode.ToString(); 

    }
}