using System.Collections.Generic;
using Newtonsoft.Json;

namespace _1000Best.Model
{
    public class Image
    {
        [JsonProperty("#text")]
        public string text { get; set; }
        public string size { get; set; }
    }

    public class Stats
    {
        public string listeners { get; set; }
        public string playcount { get; set; }
    }

    public class Image2
    {
        [JsonProperty("#text")]
        public string text { get; set; }
        public string size { get; set; }
    }

    public class Artist2
    {
        public string name { get; set; }
        public string url { get; set; }
        public List<Image2> image { get; set; }
    }

    public class Similar
    {
        public List<Artist2> artist { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Tags
    {
        public List<Tag> tag { get; set; }
    }

    public class Link
    {
        [JsonProperty("#text")]
        public string text { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Links
    {
        public Link link { get; set; }
    }

    public class Formation
    {
        public string yearfrom { get; set; }
        public string yearto { get; set; }
    }

    public class Bio
    {
        public Links links { get; set; }
        public string published { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
        public string yearformed { get; set; }
    }

    public class Artist
    {
        public string name { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
        public List<Image> image { get; set; }
    }

    public class RootObject
    {
        public Artist artist { get; set; }
    }
}
