using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MelsKitchen.Models
{
    public class Chef
    {
      
        [Key]
        public int ChefID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(350)]
        [Display(Name = "Email: ")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(8)]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [NotMapped]
        [Display(Name = "Confirm Password: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password and Password did not Match")]
        public string ConfirmPassword { get; set; }


        public List<Recipe> Dishes { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
