using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Igor_AIS_Proj.Models
{
    public class UserRegistrationRequest
    {
  
        public string Email { get; set; } = null!;
       
        public string Userpassword { get; set; } = null!;

    }
}
