using System;
using System.Collections.Generic;

namespace Igor_AIS_Proj.Models
{
    public partial class Account : Entity
    {
        public Account()
        {
           
        }

        public int AccountId { get; set; }
        public int Userid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } 
        public decimal Balance { get; set; }
        public DateTime Updatedat { get; set; }

        public  User User { get; set; } 
        public List<Movement> Movements { get; set; }

    }
}
