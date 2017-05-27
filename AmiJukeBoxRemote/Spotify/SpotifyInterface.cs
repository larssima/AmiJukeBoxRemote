using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using System.Management;

namespace AmiJukeBoxRemote.Spotify
{

    public class SpotifyInterface
    {
        private PlayList _playList = new PlayList();
        private SpotifyLocalAPI _spotify = new SpotifyLocalAPI();
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
                var _spotifyWeb = await webApiFactory.GetWebApi();
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
                _spotify.Connect();
                var status = _spotify.GetStatus();
                if (status == null)
                {
                    if (!StartSpotify())
                    {
                        return false;
                    }
                    _spotify.Connect();
                }
                _spotify.ListenForEvents = true;
                await _spotify.PlayURL(trackList.tracks.items[0].uri);
                return true;
            }
            return false;
        }

        private bool StartSpotify()
        {
            try
            {
                var p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = @"C:\Users\DiNer0-2\AppData\Roaming\Spotify\Spotify.exe";
                //p.WindowStyle = ProcessWindowStyle.Hidden;
                //p.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private string GetMainModuleFilepath(int processId)
        {
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (var results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return (string)mo["ExecutablePath"];
                    }
                }
            }
            return null;
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


        public class PlayList : List<string>
        {
            public void AddSong(string uri)
            {
                Add(uri);
            }

        }


    }
}