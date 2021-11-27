using System;
using System.Collections.Generic;

namespace RecipeRating.Web.Models
{
    public class YtSubscriptionsResponseModels
    {



        public class YtSubscriptionsResponse
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string nextPageToken { get; set; }
            public Pageinfo pageInfo { get; set; }
            public Item[] items { get; set; }
        }

        public class Pageinfo
        {
            public int totalResults { get; set; }
            public int resultsPerPage { get; set; }
        }

        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Snippet snippet { get; set; }
        }

        public class Snippet
        {
            public DateTime publishedAt { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public Resourceid resourceId { get; set; }
            public string channelId { get; set; }
            //public Thumbnails thumbnails { get; set; }
        }

        public class Resourceid
        {
            public string kind { get; set; }
            public string channelId { get; set; }
        }

        public class Thumbnails
        {
            public Default _default { get; set; }
            public Medium medium { get; set; }
            public High high { get; set; }
        }

        public class Default
        {
            public string url { get; set; }
        }

        public class Medium
        {
            public string url { get; set; }
        }

        public class High
        {
            public string url { get; set; }
        }


    }
}
