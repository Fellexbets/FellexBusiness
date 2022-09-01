using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class Question : Entity
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            PlayedQuestions = new HashSet<PlayedQuestion>();
            QuizzQuestions = new HashSet<QuizzQuestion>();
        }

        [Key]
        public int QuestionId { get; set; }
        [ForeignKey("UserId"), Required]
        public int UserId { get; set; }
        [ForeignKey("ThemeId"), Required]
        public int ThemeId { get; set; }
        [Required]
        public string QuestionName { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public int Difficulty { get; set; }
        //[Required]
        //public int Points { get; set; }
        [Required]
        public string Status { get; set; }

        #nullable enable
        public byte[]? QuestionImage { get; set; }
        #nullable disable

        [NotMapped]
        public string QuestionImageString { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }

        public virtual Theme Theme { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<PlayedQuestion> PlayedQuestions { get; set; }
        public virtual ICollection<QuizzQuestion> QuizzQuestions { get; set; }

        public override List<int> ReturnId() => new List<int>() { QuestionId };
    }
}
