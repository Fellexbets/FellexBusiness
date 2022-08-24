

namespace Igor_AIS_Proj.Models
{
    public class CreateAccountRequest : Entity
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
