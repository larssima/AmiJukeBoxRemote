using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SpotifyAPI.Local;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using Json = System.Web.Helpers.Json;

namespace AmiJukeBoxRemote.Spotify
{
    public class SpotifyInterface
    {
        public SpotifyInterface()
        {
            LoginToSpotify();
        }
        static async void LoginToSpotify()
        {
            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "0476e201675f4bcb926b5bfc6c5d21f1",
                Scope.UserReadPrivate,
                TimeSpan.FromSeconds(20)
            );

            try
            {
                //This will open the user's browser and returns once
                //the user is authorized.
                var _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
//                MessageBox.Show(ex.Message);
            }
            return;
        }

        public async System.Threading.Tasks.Task<bool> PlaySong(string artistName, string songTitle, string que)
        {
            var trackList = GetTrackList(artistName, songTitle, que);
            if (trackList.tracks.items.Count > 0)
            {
                var spotify = new SpotifyLocalAPI();
                spotify.Connect();
                await spotify.PlayURL(trackList.tracks.items[0].uri);
                return true;
            }
            return false;
        }

        public TracksRoot GetTrackList(string artistName, string songTitle, string que)
        {
            var spotifyUrl = ConfigurationManager.AppSettings["SpotifySearchUrl"];
            spotifyUrl += "artist:" + artistName.Replace(" ", "+") + "%20" + "track:" + songTitle.Replace(" ", "+") + "&type=track&market=SE";
            string tracks;


            using (var client = CreateClient())
            {
                tracks = client.DownloadString(spotifyUrl);
            }

            TracksRoot tracklist = JsonConvert.DeserializeObject<TracksRoot>(tracks);
            return tracklist;
        }

        private WebClient CreateClient()
        {
            var client = new TimedWebClient() { Credentials = CredentialCache.DefaultCredentials };
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Accept", "application/json");
            return client;
        }


    }
}