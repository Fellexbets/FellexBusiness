using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class Answer : Entity
    {
        [Key]
        public int AnswerId { get; set; }
        [ForeignKey("QuestionId"), Required]
        public int QuestionId { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public bool RightAnswer { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        [JsonIgnore]
        public virtual Question Question { get; set; }

        public override List<int> ReturnId() => new List<int>() { AnswerId };
    }
}
