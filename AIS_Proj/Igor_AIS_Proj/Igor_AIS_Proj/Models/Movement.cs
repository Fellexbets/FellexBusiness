using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igor_AIS_Proj.Models
{
    public class Movement : Entity
    {
        public int MovementId { get; set; }

        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } 
        public DateTime MovimentTime { get; set; }

        public Account Account { get; set; } 
    }
}
