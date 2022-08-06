using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace ClassLibrary1
    
{
    [PathAttribute("DB/Employees.json")]
    public class Employee : IHasPrimaryKey

    {
        #region Properties
    [JsonPropertyName("employeeID")]
    public string employeeID { get; set; }

    [JsonPropertyName("contactName")]
    public string contactName { get; set; }

    [JsonPropertyName("contactTitle")]
    public string contactTitle { get; set; }


    [JsonPropertyName("titleOfCourtesy")]
     public string titleOfCourtesy { get; set; }

    [JsonPropertyName("address")]
    public string address { get; set; }

    [JsonPropertyName("city")]
    public string city { get; set; }

    [JsonPropertyName("country")]
    public string country { get; set; }

    [JsonPropertyName("phone")]
    public string phone { get; set; }

    [JsonPropertyName("birthday")]
    public string birthday { get; set; }

    [JsonPropertyName("photo")]
    public string photo { get; set; }
        #endregion

        public override string ToString() => contactName + ", from " + country;
        public string GetPrimaryKey() => employeeID;
    }

}

