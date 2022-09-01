using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class RelatedUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; } = "";
        public string Role { get; } = "";
        public string Password { get; } = "";

        public RelatedUser(User user)
        {
            this.UserId = user.UserId;
            this.UserName = user.Username;
        }
    }
}



