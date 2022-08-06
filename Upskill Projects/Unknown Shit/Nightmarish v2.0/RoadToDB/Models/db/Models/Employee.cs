using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("employees.json")]
    public class Employee : Entity
    {
        #region Properties

        [JsonPropertyName("id")]
        public int EmployeeID { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("titleOfCourtesy")]
        public string TitleOfCourtesy { get; set; }
        [JsonPropertyName("birthdate")] 
        public string Birthdate { get; set; }
        [JsonPropertyName("hiredate")]
        public string HireDate { get; set; }
        [JsonPropertyName("address")]
        public Adress Adress { get; set; }
        
        [JsonPropertyName("notes")]
        public string Notes { get; set; }
        


        #endregion
        public override string GetPrimaryKey() => EmployeeID.ToString();
        public override string ToString() => string.Format("{0, 10} | {1,40} | {2,40}", EmployeeID, FirstName, LastName);

        public override string Header() => string.Format("{0, 10} | {1,40} | {2,40}\n------------------------------------------------------------------------------------------------", "ID", "First Name", "Last Name");

        
    }
}