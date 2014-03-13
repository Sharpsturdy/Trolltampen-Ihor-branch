using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Trolltampen.Models
{
    public class TokenPasswordModel
    {
        [Required]
        public string Token { get; set; }
        [Required(ErrorMessage = "Please input password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please confirm password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Confirmation is not correct")]
        public string CNewPassword { get; set; }

    }
}
