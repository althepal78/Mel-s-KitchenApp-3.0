using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MelsKitchen.Models
{
    public class Login
    {
        [Required]        
        [Display(Name = "Email: ")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]       
        public string LoginEmail { get; set; }

        [Required]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}
