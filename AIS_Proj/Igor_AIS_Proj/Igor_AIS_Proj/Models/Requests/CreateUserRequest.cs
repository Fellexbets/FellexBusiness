
namespace Igor_AIS_Proj.Models
{
    public class CreateUserRequest : Entity
    {
  
        public string Email { get; set; }

        public string? FullName { get; set; }

        public string? UserPassword { get; set; } 

        public string? PasswordSalt { get; set; }

        public string Username { get; set; }

    }
}
