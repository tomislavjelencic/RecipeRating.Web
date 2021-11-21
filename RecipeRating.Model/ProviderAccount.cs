using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeRating.Model
{
    public class ProviderAccount
    {
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider {get; set;}
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
