using System;
using System.Collections.Generic;

namespace Igor_AIS_Proj.Models
{
    public partial class Movement : Entity
    {
        public int Movementid { get; set; }
        public int Accountid { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime Movimenttime { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
