using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trolltampen.Models
{
    public class LoginModel
    {
        
        [Required(ErrorMessage = "Please provide user name")]
        public string Name { get; set; }
        [Required(ErrorMessage="Please provide password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string LoginFailedResult { get; set; }
    }
}