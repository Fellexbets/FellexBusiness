using System;
using System.Collections.Generic;

namespace Igor_AIS_Proj.Models
{
    public partial class Account : Entity
    {
        public Account()
        {
            Movements = new HashSet<Movement>();
        }

        public int Accountid { get; set; }
        public int Userid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } = null!;
        public decimal Balance { get; set; }
        public DateTime Updatedat { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Movement> Movements { get; set; }
    }
}
