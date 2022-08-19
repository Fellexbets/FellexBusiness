using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Igor_AIS_Proj.Models
{
    public class TransferRequest : Entity
    {
  
        public decimal Amount { get; set; }
       
        public string Currency { get; set; } 

        public int FromAccountId { get; set; }

        public int ToAccountId { get; set; }

    }
}
