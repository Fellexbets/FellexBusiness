using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable enable

namespace RoadToDB
{
    [PathAttribute("employees.json")]
    public class Employee : Entity
    {
        [JsonPropertyName("employeeID")]
        public int EmployeeId { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = null!;
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("address")]
        public Address? AddressProp { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("titleOfCourtesy")]
        public string? TitleOfCourtesy { get; set; }
        [JsonPropertyName("birthDate")]
        public DateTime? BirthDate { get; set; }
        [JsonPropertyName("hireDate")]
        public DateTime? HireDate { get; set; }
                
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
        [JsonPropertyName("reportsTo")]
        public int? ReportsTo { get; set; }
        [JsonPropertyName("territoryIDs")]
        public List<int> TerritoryIDs { get; set; } = new List<int>();

        public override string GetPrimaryKey() => EmployeeId.ToString();

        public override string ToString() => string.Format("{0, 20} | {1, 20} | {2, 30} | {3, 20}", EmployeeId, FirstName, LastName, AddressProp);

        public override string Header() => base.Header() + string.Format("{0, 20} | {1, 20} | {2, 30} | {3, 20}", "Employee Id", "First Name", "Last Name", "Country");
    }
}
