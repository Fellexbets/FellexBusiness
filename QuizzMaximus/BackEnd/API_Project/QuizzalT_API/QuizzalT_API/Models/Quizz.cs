using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class Quizz : Entity
    {
        public Quizz()
        {
            PlayedQuizzes = new HashSet<PlayedQuizz>();
            QuizzQuestions = new HashSet<QuizzQuestion>();
        }

        [Key]
        public int QuizzId { get; set; }
        [ForeignKey("UserId"), Required]
        public int UserId { get; set; }
        [Required]
        public string QuizzName { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PlayedQuizz> PlayedQuizzes { get; set; }
        public virtual ICollection<QuizzQuestion> QuizzQuestions { get; set; }

        public override List<int> ReturnId() => new List<int>() { QuizzId };
    }
}
