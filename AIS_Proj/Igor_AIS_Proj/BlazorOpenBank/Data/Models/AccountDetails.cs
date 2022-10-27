
using System.Text.Json.Serialization;

namespace BlazorOpenBank.Data.Models
{

    public class AccountDetails
    {
      
        public Account Account { get; set; } = new Account();

        public IEnumerable<Movim>? Movs { get; set; } = new List<Movim>();
    }
}
