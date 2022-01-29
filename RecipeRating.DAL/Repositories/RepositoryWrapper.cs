using Microsoft.Extensions.Configuration;
using RecipeRating.DAL.Repositories.Implementations;
using RecipeRating.Model.Interfaces;
using System.Threading.Tasks;

namespace RecipeRating.DAL.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RecipeRatingDbContext _context;
        public ICategoryRepository Category { get; }

        public RepositoryWrapper(RecipeRatingDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
        }

        public void SaveBatchChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveBatchChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
