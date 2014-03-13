using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trolltampen.Models
{
    public class EditUserModel
    {
        
        [Display(Name = " Old password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password confirmation is wrong")]
        public string CNewPassword { get; set; }

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

        public int ID { get; set; }
        public string ErrorMessage { get; set; }
    }
}