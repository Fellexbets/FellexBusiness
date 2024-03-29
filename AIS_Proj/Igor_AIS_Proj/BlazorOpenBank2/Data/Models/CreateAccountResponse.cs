﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorOpenBank2.Data.Models
{
    public class CreateAccountResponse
    {
        public int AccountId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } 
        public decimal Balance { get; set; }

    }
}
