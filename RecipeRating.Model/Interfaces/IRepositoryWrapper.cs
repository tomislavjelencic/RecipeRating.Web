using System.Threading.Tasks;

namespace RecipeRating.Model.Interfaces
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; }
        void SaveBatchChanges();
        Task SaveBatchChangesAsync();
    }
}
