using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igor_AIS_Proj.Models
{
    public class Movement : Entity
    {
        public int Movementid { get; set; }

        [ForeignKey("Accountid")]
        public int Accountid { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime Movimenttime { get; set; }

        public Account Account { get; set; } 
    }
}
