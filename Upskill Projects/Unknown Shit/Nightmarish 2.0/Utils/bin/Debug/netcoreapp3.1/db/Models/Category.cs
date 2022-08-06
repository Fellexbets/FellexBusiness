using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Category : Entity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public override string ToString() => $"{CategoryId} {CategoryName} {Description}";

        public override string GetPrimaryKey() => CategoryId.ToString();

        public override string Header() => string.Format("{0, 20} | {1, 40} | {2, 80}\n-----------------------------------------------------------------------------------------------------------------", "Category ID", "Category Name", "Description");
    }
}
