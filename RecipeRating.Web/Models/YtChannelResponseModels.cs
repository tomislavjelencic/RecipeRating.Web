using System;

namespace RecipeRating.Web.Models
{
    public class YtChannelResponseModels
    {


        public class YtChannelResponse
        {
            public string kind { get; set; }
            public string etag { get; set; }
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
        }


    }
}
