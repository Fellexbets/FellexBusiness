using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Igor_AIS_Proj.Models
{
    public class CreateUserRequest
    {
        
        [Key]
        public int Userid { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
       
        public string Userpassword { get; set; } = null!;

    }
}
