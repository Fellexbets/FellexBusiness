using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class PlayedQuizz : Entity
    {
        [Key]
        public int PlayedQuizzId { get; set; }
        [ForeignKey("UserId"), Required]
        public int UserId { get; set; }
        [ForeignKey("QuizzId"), Required]
        public int QuizzId { get; set; }
        [Required]
        public int TotalPoints { get; set; }
        public DateTime? DateAnswered { get; set; }

        public virtual Quizz Quizz { get; set; }
        public virtual User User { get; set; }

        public override List<int> ReturnId() => new List<int>() { PlayedQuizzId };
    }
}
