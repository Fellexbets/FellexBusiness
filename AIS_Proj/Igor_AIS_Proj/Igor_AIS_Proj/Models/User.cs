using System;
using System.Collections.Generic;

namespace Igor_AIS_Proj.Models
{
    public partial class User : Entity
    {
        public User()
        {
            Accounts = new HashSet<Account>();
        }

        public int Userid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Userpassword { get; set; } = null!;

        public virtual ICollection<Account>? Accounts { get; set; }

        public override List<int> ReturnId() => new List<int>() { Userid };

    }
}
