namespace RecipeRating.Web.Models
{
    public class RecipesFilterModel
    {
        public int? dishFilter { get; set; }
        public int? currentDishFilter { get; set; }
        /*public string sortTaste { get; set; }
        public string sortComplex { get; set; }
        public string sortTime { get; set; }
        public string sortPrice { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }*/
        public string currentFilter { get; set; }
        public string subFilter { get; set; }
        public string searchString { get; set; }
        public int? pageNumber { get; set; }
    }
}
