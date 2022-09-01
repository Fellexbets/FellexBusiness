using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class UserRelation : Entity
    {
        [Key, ForeignKey("UserId")]
        public int UserId { get; set; }
        [Key, ForeignKey("UserId")]
        public int RelatedUserId { get; set; }
        [ForeignKey("RelationId")]
        public int RelationId { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual User RelatedUser { get; set; }
        public virtual User User { get; set; }

        public override List<int> ReturnId() => new List<int>() { UserId, RelatedUserId };
    }
}
