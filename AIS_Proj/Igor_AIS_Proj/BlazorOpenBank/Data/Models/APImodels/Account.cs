using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorOpenBank.Data.Models.APImodels
{
    public class Account
    {
        public Account()
        {

        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public List<Movement>? Movements { get; set; }

    }
}
