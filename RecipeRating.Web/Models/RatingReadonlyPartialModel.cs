namespace RecipeRating.Web.Models
{
    public class RatingReadonlyPartialModel
    {
        public RatingReadonlyPartialModel(string label, string image, int? score)
        {
            Label = label;
            Image = image;
            Score = score;
        }

        public string Label { get; set; }
        public string Image { get; set; }
        public int? Score { get; set; }
    }
}
