using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class QuizzQuestion : Entity
    {
        [Key, ForeignKey("QuizzId")]
        public int QuizzId { get; set; }
        [Key, ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
        [JsonIgnore]
        public virtual Question Question { get; set; }
        [JsonIgnore]
        public virtual Quizz Quizz { get; set; }

        public override List<int> ReturnId() => new List<int>() { QuizzId, QuestionId };
    }
}
