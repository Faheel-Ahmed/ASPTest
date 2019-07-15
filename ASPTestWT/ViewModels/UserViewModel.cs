using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPTestWT.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "This is a required field")]
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This is a required field")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This is a required field")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "This is a required field")]
        public string Email { get; set; }

        public string Initials { get; set; }
    }
}