using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class ContactUsModel
    {
        
            [Required(ErrorMessage = "Name Required")]
            public string name { get; set; }
            [Required(ErrorMessage = "Email Required")]
            //[EmailAddress(ErrorMessage = "Email not valid")]
            public string email { get; set; }
            [Required(ErrorMessage = "Contact Number Required")]
            public string contactNo { get; set; }
            [Required(ErrorMessage = "Message Required")]
            public string messgae { get; set; }
        
    }
}