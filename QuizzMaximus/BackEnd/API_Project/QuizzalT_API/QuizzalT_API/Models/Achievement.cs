using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class Achievement : Entity
    {
        [Key, ForeignKey("ThemeId")]
        public int ThemeId { get; set; }
        [Key, ForeignKey("UserId")]
        public int UserId { get; set; }
        public int GainedPoints { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual User User { get; set; }

        public override List<int> ReturnId() => new List<int>() { ThemeId, UserId };
    }
}
