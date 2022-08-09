using System;
using System.Collections.Generic;

namespace Igor_AIS_Proj.Models
{
    public class Account : Entity
    {
        public Account()
        {
            TransferDestinationaccounts = new HashSet<Transfer>();
            TransferOriginaccounts = new HashSet<Transfer>();
        }

        public int Accountid { get; set; }
        public int Userid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } = null!;
        public decimal Balance { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Transfer> TransferDestinationaccounts { get; set; }
        public virtual ICollection<Transfer> TransferOriginaccounts { get; set; }

        public override List<int> ReturnId() => new List<int>() { Accountid };
    }
}
