using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorOpenBank.Data.Models.APImodels
{
    public class Movement
    {
        public int MovementId { get; set; }

        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public DateTime MovimentTime { get; set; }

        //public Account? Account { get; set; } 
    }
}
