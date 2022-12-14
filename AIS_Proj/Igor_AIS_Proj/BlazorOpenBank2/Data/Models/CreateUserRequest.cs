
using System.ComponentModel.DataAnnotations;

namespace BlazorOpenBank2.Data.Models
{
    public class CreateUserRequest
    {

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(200, MinimumLength = 3)]
        public string? FullName { get; set; }
        [Required, MinLength(5)]
        public string? UserPassword { get; set; }

        [Required, MinLength(5)]
        public string Username { get; set; }

    }
}
