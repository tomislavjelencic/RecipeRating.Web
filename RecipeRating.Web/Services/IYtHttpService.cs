using RecipeRating.Web.Models;
using static RecipeRating.Web.Models.YtSearchResponseModels;
using static RecipeRating.Web.Models.YtVideoResponseModels;

namespace RecipeRating.Web.Services
{
    public interface IYtHttpService
    {
        YtSearchResponse GetSearchResults(string keyword, string token);
        YtVideoResponse GetVideo(string id, string token);
    }
}
