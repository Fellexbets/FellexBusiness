using Igor_AIS_Proj.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public class LoginUserRequest : Entity
    {
        
        public string Email { get; set; }
        
        public string UserPassword { get; set; }

        //public string Token { get; set; }

    }
}
