using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igor_AIS_Proj.Models
{
    public partial class User : Entity
    {
        public User()
        {
            Accounts = new HashSet<Account>();
        }
        [Key]
        public int Userid { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
       
        public string Userpassword { get; set; } = null!;

        
        public string PasswordSalt { get; set; } 

        public DateTime UpdatedAt { get; set; }

        public string? UserToken { get; set; }

        public virtual ICollection<Account>? Accounts { get; set; }

        public override List<int> ReturnId() => new List<int>() { Userid };

    }
}
