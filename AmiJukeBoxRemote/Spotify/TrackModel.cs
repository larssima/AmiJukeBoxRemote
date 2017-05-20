using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiJukeBoxRemote.Spotify
{
    //    public class TrackModel
    //    {
    //        public int duration_ms { get; set; }
    //        public string href { get; set; } 
    //        public string id { get; set; }
    //        public bool is_playable { get; set; }
    ////        public string name { get; set; }
    //        public int popularity { get; set; }
    //        public string preview_url { get; set; }
    //        public int track_number { get; set; }
    //        public string type { get; set; }
    //        public string uri { get; set; }
    //    }

    public class Image2
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }
    public class ExternalUrls4
    {
        public string spotify { get; set; }
    }
    public class Album
    {
        public string album_type { get; set; }
        public List<object> available_markets { get; set; }
        public ExternalUrls4 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image2> images { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
    public class ExternalUrls5
    {
        public string spotify { get; set; }
    }
    public class Artist
    {
        public ExternalUrls5 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
    public class ExternalIds
    {
        public string isrc { get; set; }
    }

    public class ExternalUrls6
    {
        public string spotify { get; set; }
    }
    public class Track
    {
        public Album album { get; set; }
        public List<Artist> artists { get; set; }
        public List<object> available_markets { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls6 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class ExternalUrls3
    {
        public string spotify { get; set; }
    }
    public class AddedBy
    {
        public ExternalUrls3 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
    public class Item
    {
        public string added_at { get; set; }
        public AddedBy added_by { get; set; }
        public bool is_local { get; set; }
        public Track track { get; set; }
    }

    public class Tracks
    {
        public string href { get; set; }
        public List<Item> items { get; set; }
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }

    public class RootObject
    {
        public Tracks tracks { get; set; }
    }
}