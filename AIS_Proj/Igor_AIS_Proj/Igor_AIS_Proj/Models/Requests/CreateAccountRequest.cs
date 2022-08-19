using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Igor_AIS_Proj.Models
{
    public class CreateAccountRequest : Entity
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
       
    }
}
