using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

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

        public bool PlaySong(string artistName, string songTitle, string que)
        {
            return true;
        }


    }
}