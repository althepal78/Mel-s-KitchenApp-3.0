using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MelsKitchen.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Recipe Name: ")]
        public string RecipeName { get; set; }

        [Required]
        [MaxLength(5000)]
        [Display(Name = "Ingredients: ")]
        public string Ingredients { get; set; }

        [Required]
        [MaxLength(15000)]
        [Display(Name = "Directions: ")]
        public string Directions { get; set; }

        public Chef RecipeMaker { get; set; }

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
