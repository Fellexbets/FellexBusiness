

using System.ComponentModel.DataAnnotations;

namespace BlazorOpenBank2.Data.Models
{
    public class CreateAccountRequest
    {
        [Required]
        public decimal Balance { get; set; }
        [Required, StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; }
    }
}
