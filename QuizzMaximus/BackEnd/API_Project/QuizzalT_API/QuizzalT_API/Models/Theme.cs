using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class Theme : Entity
    {
        public Theme()
        {
            Achievements = new HashSet<Achievement>();
            Questions = new HashSet<Question>();
        }

        [Key]
        public int ThemeId { get; set; }
        [Required]
        public string ThemeName { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        public override List<int> ReturnId() => new List<int>() { ThemeId };
    }
}
