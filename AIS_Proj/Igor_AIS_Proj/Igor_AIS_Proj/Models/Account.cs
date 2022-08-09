using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igor_AIS_Proj.Models
{
    public class Account : Entity
    {
        public Account()
        {
            TransferDestinationaccounts = new HashSet<Transfer>();
            
        }

        [Key]
        public int Accountid { get; set; }
        [ForeignKey("UserId")]
        public int Userid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } = null!;
        public decimal Balance { get; set; }

        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Transfer> TransferDestinationaccounts { get; set; }

        public override List<int> ReturnId() => new List<int>() { Accountid };
    }
}
