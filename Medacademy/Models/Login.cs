using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{

    public class Login
    {
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Entered e-mail is not a valid mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }

    }
}