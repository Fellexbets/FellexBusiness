using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projecto3MVC.Models
{
    public class Ad_login
    {
        public int UserID { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage ="This field is required.")]
        public string Admin_id { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string Ad_Password { get; set; }    

        public string LoginErrorMessage { get; set; }
    }
}
