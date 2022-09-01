using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class User : Entity
    {
        public User()
        {
            Achievements = new HashSet<Achievement>();
            PlayedQuestions = new HashSet<PlayedQuestion>();
            PlayedQuizzes = new HashSet<PlayedQuizz>();
            Questions = new HashSet<Question>();
            Quizzes = new HashSet<Quizz>();
            UserRelationRelatedUsers = new HashSet<UserRelation>();
            UserRelationUsers = new HashSet<UserRelation>();
        }

        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        public DateTime? DateCreated { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        #nullable enable
        public byte[]? Photo { get; set; }
        #nullable disable
        [NotMapped]
        public string PhotoString { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<PlayedQuestion> PlayedQuestions { get; set; }
        public virtual ICollection<PlayedQuizz> PlayedQuizzes { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Quizz> Quizzes { get; set; }
        public virtual ICollection<UserRelation> UserRelationRelatedUsers { get; set; }
        public virtual ICollection<UserRelation> UserRelationUsers { get; set; }

        public override List<int> ReturnId() => new List<int>() { UserId };
    }
}
