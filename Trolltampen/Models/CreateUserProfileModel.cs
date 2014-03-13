using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trolltampen.Models
{
    public class CreateUserProfileModel
    {
        [Required(ErrorMessage = "Please input password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please input password confirmation")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password confirmation is wrong")]
        public string CPassword { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please input user name")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please input first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please input last name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please input email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}