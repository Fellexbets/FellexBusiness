
using System.ComponentModel.DataAnnotations;

namespace BlazorOpenBank2.Data.Models
{
    public class TransferRequest
    {

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int FromAccountId { get; set; }

        [Required]
        public int ToAccountId { get; set; }

    }
}
