
namespace Igor_AIS_Proj.Models
{
    public class Account : Entity
    {


        public int AccountId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } 
        public decimal Balance { get; set; }
        public DateTime UpdatedAt { get; set; }

        //public User User { get; set; }
        public List<Movement>? Movements { get; set; }

    }
}
