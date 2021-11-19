namespace RecipeRating.Web.Models
{
    public class RatingModel
    {
        public RatingModel(string minLabel, string maxLabel, string name)
        {
            MinLabel = minLabel;
            MaxLabel = maxLabel;
            Name = name;
        }

        public string MinLabel { get; set; }
        public string MaxLabel { get; set; }
        public string Name { get; set; }
    }
}
