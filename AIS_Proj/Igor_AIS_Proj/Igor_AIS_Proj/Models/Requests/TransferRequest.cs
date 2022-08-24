
namespace Igor_AIS_Proj.Models
{
    public class TransferRequest : Entity
    {
  
        public decimal Amount { get; set; }
       
        public int FromAccountId { get; set; }

        public int ToAccountId { get; set; }

    }
}
