using AutoMapper;
using Microsoft.Extensions.Configuration;
using RecipeRating.Model;
using RecipeRating.Web.Models.DTO;
using System.Linq;

namespace RecipeRating.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
