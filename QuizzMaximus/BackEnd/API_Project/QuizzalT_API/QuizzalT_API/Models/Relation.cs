using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class Relation : Entity
    {
        [Key]
        public int RelationId { get; set; }
        [Required]
        public string RelationName { get; set; }
        public override List<int> ReturnId() => new List<int>() { RelationId };
    }
}
