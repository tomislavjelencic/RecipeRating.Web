using System.ComponentModel.DataAnnotations;

namespace RecipeRating.Web.Models
{
    public class RecipeInputModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ThumbnailUrl { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int DishId { get; set; }
        public int? ProviderAccountId { get; set; }
        public string AccountId { get; set; }
        public string AccountImageUrl { get; set; }
        public string AccountName { get; set; }
    }
}
