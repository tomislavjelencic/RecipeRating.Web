using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RecipeRating.Web.Models;
using RestSharp;
using static RecipeRating.Web.Models.YtSearchResponseModels;
using static RecipeRating.Web.Models.YtVideoResponseModels;

namespace RecipeRating.Web.Services
{
    public class YtHttpService : IYtHttpService
    {
        private readonly IConfiguration _config;

        public YtHttpService(IConfiguration config)
        {
            _config = config;
        }

        public YtSearchResponse GetSearchResults(string keyword, string token)
        {
            var client = new RestClient(_config["Authentication:Google:YtEndpoint"]);
            var request = new RestRequest("/search", Method.GET);

            request.AddQueryParameter("part", "snippet");
            request.AddQueryParameter("maxResults", "15");
            request.AddQueryParameter("regionCode", "us");
            request.AddQueryParameter("q", keyword);
            request.AddQueryParameter("key", _config["Authentication:Google:ApiKey"]);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<YtSearchResponse>(response.Content);
            return data;
        }

        public YtVideoResponse GetVideo(string id, string token)
        {
            var client = new RestClient(_config["Authentication:Google:YtEndpoint"]);
            var request = new RestRequest("/videos", Method.GET);

            request.AddQueryParameter("part", "snippet");
            request.AddQueryParameter("id", id);
            request.AddQueryParameter("key", _config["Authentication:Google:ApiKey"]);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<YtVideoResponse>(response.Content);
            return data;
        }
    }
}
