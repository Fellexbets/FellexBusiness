
namespace Igor_AIS_Proj.Models
{
    public class UserRegistrationRequest : Entity
    {
  
        public string Email { get; set; } 
       
        public string UserPassword { get; set; } 

        public string PasswordSalt { get; set; }

    }
}
