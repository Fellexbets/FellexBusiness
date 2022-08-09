using System;
using System.Collections.Generic;

namespace Igor_AIS_Proj.Models
{
    public partial class Transfer : Entity
    {
        public int Transferid { get; set; }
        public int Originaccountid { get; set; }
        public int Destinationaccountid { get; set; }
        public decimal Transferamount { get; set; }
        public DateTime Transfertime { get; set; }

        public virtual Account Destinationaccount { get; set; } = null!;
        public virtual Account Originaccount { get; set; } = null!;

        public override List<int> ReturnId() => new List<int>() { Transferid, Originaccountid, Destinationaccountid };

    }
}
