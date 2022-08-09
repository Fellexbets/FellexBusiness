using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igor_AIS_Proj.Models
{
    public partial class Transfer : Entity
    {
        [Key]
        public int Transferid { get; set; }
        [Key, ForeignKey("AccountId")]
        public int Originaccountid { get; set; }
        [Key, ForeignKey("AccountId")]
        public int Destinationaccountid { get; set; }
        public decimal Transferamount { get; set; }
        public DateTime Transfertime { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual Account Destinationaccount { get; set; } = null!;
        public virtual Account Originaccount { get; set; } = null!;

        public override List<int> ReturnId() => new List<int>() { Transferid, Originaccountid, Destinationaccountid };

    }
}
