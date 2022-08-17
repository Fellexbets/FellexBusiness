using System;
using System.Collections.Generic;

namespace Igor_AIS_Proj.Models
{
    public partial class Transfer : Entity
    {
        public int Transferid { get; set; }
        public int Originaccountid { get; set; }
        public int Destinationaccountid { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime Transferdate { get; set; }
    }
}
