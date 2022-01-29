namespace RecipeRating.Web.Models
{
    public class DishesFilterModel
    {
        public int? categoryFilter { get; set; }
        public int? currentCategoryFilter { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? pageNumber { get; set; }
    }
}
