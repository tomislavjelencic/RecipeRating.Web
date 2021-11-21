namespace RecipeRating.Web.Models
{
    public class RatingPartialModel
    {
        public RatingPartialModel(string minLabel, string maxLabel, string name)
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
