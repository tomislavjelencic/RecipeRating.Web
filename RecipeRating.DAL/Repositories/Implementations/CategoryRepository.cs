using RecipeRating.DAL;
using RecipeRating.DAL.Repositories;
using RecipeRating.Model;
using RecipeRating.Model.Interfaces;

namespace RecipeRating.DAL.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(RecipeRatingDbContext context) : base(context)
        {
        }
    }
}
