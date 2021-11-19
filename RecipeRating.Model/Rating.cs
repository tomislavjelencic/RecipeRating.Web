using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeRating.Model
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Simplicity { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public int Taste { get; set; }
        public virtual ICollection<RecipeRating> RecipeRatings { get; set; }
    }
}
