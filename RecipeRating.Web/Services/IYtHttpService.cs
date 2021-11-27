using RecipeRating.Web.Models;
using System.Collections.Generic;
using static RecipeRating.Web.Models.YtSearchResponseModels;
using static RecipeRating.Web.Models.YtVideoResponseModels;

namespace RecipeRating.Web.Services
{
    public interface IYtHttpService
    {
        YtSearchResponse GetSearchResults(string keyword);
        YtVideoResponse GetVideo(string id);
        string GetChannelId(string token);
        List<string> GetSubscriptions(string id, string token);
    }
}
