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
        [MaxLength(150)]
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<ProviderAccount> ProviderAccounts { get; set; }
    }
}
