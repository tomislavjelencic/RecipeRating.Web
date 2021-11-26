using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RecipeRating.Model
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ThumbnailUrl { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        [Required]
        public int ProviderAccountId { get; set; }
        public ProviderAccount ProviderAccount { get; set; }
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public int? Complexity => Ratings.Count > 0 ? (int?)Math.Ceiling(Ratings.Select(r => r.Simplicity).Average()) : null;
        public int? Taste => Ratings.Count > 0 ? (int?)Math.Ceiling(Ratings.Select(r => r.Taste).Average()) : null;
        public int? Time => Ratings.Count > 0 ? (int?)Math.Ceiling(Ratings.Select(r => r.Time).Average()) : null;
        public int? Price => Ratings.Count > 0 ? (int?)Math.Ceiling(Ratings.Select(r => r.Price).Average()) : null;
    }
}
