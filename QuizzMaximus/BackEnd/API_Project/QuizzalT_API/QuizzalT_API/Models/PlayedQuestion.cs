using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class PlayedQuestion : Entity
    {
        [Key]
        public int PlayedQuestionId { get; set; }
        [ForeignKey("UserId"), Required]
        public int UserId { get; set; }
        [ForeignKey("QuestionId"), Required]
        public int QuestionId { get; set; }
        [Required]
        public bool GotItRight { get; set; }
        [Required]
        public int Points { get; set; }
        public DateTime? DateAnswered { get; set; }

        public virtual Question Question { get; set; }
        public virtual User User { get; set; }

        public override List<int> ReturnId() => new List<int>() { PlayedQuestionId };
    }
}
