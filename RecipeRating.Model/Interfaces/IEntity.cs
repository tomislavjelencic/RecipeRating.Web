using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeRating.Model.Interfaces
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
