using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeRating.Model
{
    public class RecipeRating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        [Required]
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
    }
}
