

using System.ComponentModel.DataAnnotations;

namespace BlazorOpenBank2.Data.Models
{
    public class LoginUserRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(3), MaxLength(20)]
        public string UserPassword { get; set; }

    }
}
