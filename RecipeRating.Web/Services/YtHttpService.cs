using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RecipeRating.Web.Models;
using RestSharp;
using System.Collections.Generic;
using static RecipeRating.Web.Models.YtChannelResponseModels;
using static RecipeRating.Web.Models.YtSearchResponseModels;
using static RecipeRating.Web.Models.YtSubscriptionsResponseModels;
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

        public YtSearchResponse GetSearchResults(string keyword)
        {
            var client = new RestClient(_config["Authentication:Google:YtEndpoint"]);
            var request = new RestRequest("/search", Method.GET);

            request.AddQueryParameter("part", "snippet");
            request.AddQueryParameter("maxResults", "15");
            request.AddQueryParameter("regionCode", "us");
            request.AddQueryParameter("q", keyword);
            request.AddQueryParameter("key", _config["Authentication:Google:ApiKey"]);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<YtSearchResponse>(response.Content);
            return data;
        }

        public YtVideoResponse GetVideo(string id)
        {
            var client = new RestClient(_config["Authentication:Google:YtEndpoint"]);
            var request = new RestRequest("/videos", Method.GET);

            request.AddQueryParameter("part", "snippet");
            request.AddQueryParameter("id", id);
            request.AddQueryParameter("key", _config["Authentication:Google:ApiKey"]);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<YtVideoResponse>(response.Content);
            return data;
        }

        public string GetChannelId(string token)
        {
            var client = new RestClient(_config["Authentication:Google:YtEndpoint"]);
            var request = new RestRequest("/channels", Method.GET);

            request.AddQueryParameter("part", "snippet");
            request.AddQueryParameter("mine", "true");
            request.AddQueryParameter("key", _config["Authentication:Google:ApiKey"]);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<YtChannelResponse>(response.Content);
            return data.items[0].id;
        }

        public List<string> GetSubscriptions(string id, string token)
        {
            var client = new RestClient(_config["Authentication:Google:YtEndpoint"]);
            var request = new RestRequest("/subscriptions", Method.GET);

            request.AddQueryParameter("part", "snippet");
            request.AddQueryParameter("channelId", id);
            request.AddQueryParameter("key", _config["Authentication:Google:ApiKey"]);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<YtSubscriptionsResponse>(response.Content);
            List<string> subs = new List<string>();
            foreach(var item in data.items)
            {
                subs.Add(item.id);
            }
            return subs;
        }
    }
}
