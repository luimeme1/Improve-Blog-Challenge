using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SimpleBlog.API.Models 
{
    public class Post 
    {
        public static Random BgRand = new Random(0);

        [JsonIgnore]
        public Guid Guid { 
            get { return System.Guid.NewGuid(); }
            set {} 
        }

        [JsonProperty("slug")]
        public string Slug
        {
            get
            {
                // blank space
                var slug = Title.Replace(" ", "_");
                // special characters
                slug = Regex.Replace(slug, "[^a-zA-Z0-9_]", "");
                return $"{slug.ToLowerInvariant()}_n{Id}";
            }
            set { }
        }

        [JsonProperty("colour")]
        public int Colour {
            get { return BgRand.Next(100000, 1000000); }
            set {}
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("image")]
        public string Image => "https://via.placeholder.com/150x150";
    }
}