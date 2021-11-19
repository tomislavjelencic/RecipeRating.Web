using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeRating.Model
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
