using System;
using System.Collections.Generic;

#nullable disable

namespace Project3API.Models
{
    public partial class CustomerGroupThreshold
    {
        public string CustomerGroupName { get; set; }
        public decimal? RangeBottom { get; set; }
        public decimal? RangeTop { get; set; }
    }
}
