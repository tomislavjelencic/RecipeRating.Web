using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeRating.Model
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        [Required]
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string ProviderUserId { get; set; }
        public string ProviderUserName { get; set; }
        public virtual ICollection<RecipeRating> RecipeRatings { get; set; }
    }
}
