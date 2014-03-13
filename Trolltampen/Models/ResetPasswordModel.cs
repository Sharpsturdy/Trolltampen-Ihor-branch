using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trolltampen.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage="Please input email")]
        [EmailAddress(ErrorMessage="Please input valid email")]
        public string Email { get; set; }
        public string ErrorMessage { get; set; }
    }
}