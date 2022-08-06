using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EFCore
{
    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        
        public string Description { get; set; }


        public override string ToString() => $"{CategoryID} {CategoryName} {Description}";


    }
}
