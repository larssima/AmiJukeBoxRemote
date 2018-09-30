using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Windows.Forms;
using AmiJukeBoxRemote.Database;
using AmiJukeBoxRemote.Models;
using AmiJukeBoxRemote.Mqtt;
using AmiJukeBoxRemote.Spotify;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace AmiJukeboxRemote.webapi
{
    //public class IgnoreActivityViewModel
    //{
    //    public Guid Id { get; set; }
    //    public string ActivityCode { get; set; }
    //}

    [System.Web.Http.RoutePrefix("api/amijukebox")]
    public class JukeboxController : ApiController
    {
        private readonly Mqtt _mqtt = new Mqtt();
        private readonly DatabaseFunctions _jbDb = new DatabaseFunctions();
        private readonly SpotifyInterface _spotifyInterface = new SpotifyInterface();

        [Route("cancel")]
        [System.Web.Http.HttpGet]
        public bool CancelRecordPlaying()
        {
            _mqtt.SendCancelToSubscriber();
            return true;
        }

        [Route("spotifylogin")]
        [HttpGet]
        public void LoginSpotify()
        {
            var spotInt = new SpotifyInterface();
        }

        [Route("playsongonspotify")]
        [HttpPut]
        public System.Threading.Tasks.Task<bool> PlaySongOnSpotify(SongModel songmodel)
        {
            return _spotifyInterface.PlaySong(songmodel.Artist, songmodel.SongTitle,songmodel.Que);
        }

        [Route("playsongonjukebox")]
        [HttpPut]
        public bool PlaySongOnJukebox(JukeboxModel jukeboxmodel)
        {
            _mqtt.PlaySelectionOnJukebox(jukeboxmodel);
            return true;
        }

        [Route("archiveselection")]
        [HttpPut]
        public bool ArchiveSelection(JbSelectionModel jbmodel)
        {
            if (!_jbDb.ArchiveSelection(jbmodel.Id)) return false;
            CreateAllStrips();
            return true;
        }

        [Route("reinstateselection")]
        [HttpPut]
        public bool ReinstateSelection(JbSelectionModel jbmodel)
        {
            var ok = _jbDb.ReinstateSelection(jbmodel);
            CreateAllStrips();
            return ok;
        }

        [Route("savestrip")]
        [HttpPut]
        public bool SaveStripToDb(JbSelectionModel jbmodel)
        {
            var ok = _jbDb.SaveToDataBase(jbmodel)>-1;
            if (!ok) return false;
            jbmodel.ImageStripName = CreateJbStrip(jbmodel);
            _jbDb.UpdateImagePath(jbmodel);
            return true;
        }

        [Route("updateselection")]
        [HttpPut]
        public GenericAPIResultModel UpdateSelectionInDb(JbSelectionModel jbmodel)
        {
            string errorMessage = string.Empty;
            var ok = _jbDb.UpdateInDataBase(jbmodel);
            if (ok)
            {
                CreateAllStrips();
            }
            else
            {
                errorMessage = "Could not update in database";
            }
            return new GenericAPIResultModel() { Success = errorMessage.Length == 0, Message = errorMessage };
        }

        [Route("createstrips")]
        [HttpGet]
        public bool CreateAllStrips()
        {
            CleanImagesDirectory();
            List<JbSelectionModel> allSelections = _jbDb.GetAllSelections();
            allSelections.AddRange(_jbDb.GetAllArchivedSelections());
            foreach (var jbmodel in allSelections)
            {
                jbmodel.ImageStripName = CreateJbStrip(jbmodel);
                _jbDb.UpdateImagePath(jbmodel);
            }
            return true;
        }

        private void CleanImagesDirectory()
        {
            var imagepath = @"C:\Users\DiNer0-2\Documents\GitHub\AmiJukeBoxRemote\AmiJukeBoxRemote\gui\assets\images\";
            DirectoryInfo dir = new DirectoryInfo(imagepath);
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
        }

        [Route("getalljukeboxselections")]
        [HttpGet]
        public List<JbSelectionModel> GetAllJukeboxSelectíons()
        {
            var token = GetTokenRest();
            TurnJukeboxOnOff(1,token);
            return _jbDb.GetAllSelections();
        }

        [Route("turnonoff")]
        [HttpPut]
        public void TurnOnOff(OnOffObj onoff)
        {
            TurnJukeboxOnOff(onoff.OnOff, GetTokenRest());
        }



        [Route("getallarchivedjukeboxselections")]
        [HttpGet]
        public List<JbSelectionModel> GetAllArchivedJukeboxSelectíons()
        {
            return _jbDb.GetAllArchivedSelections();
        }

        public class TokenObject 
        {
            public string regtime { get; set; }
            public string email { get; set; }
            public string token { get; set; }
        }


        public string GetTokenRest()
        {
            var client = new RestClient("https://wap.tplinkcloud.com");
            var request = new RestRequest(Method.POST);
//            request.AddHeader("postman-token", "9f021650-0a23-6424-aa3b-24222f4c7e15");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "text/plain");
            request.AddParameter("application/json", "{\r\n \"method\": \"login\",\r\n \"params\": {\r\n \"appType\": \"Kasa_Android\",\r\n \"cloudUserName\": \"diner0.2000@gmail.com\",\r\n \"cloudPassword\": \"cZO*dt57d&J8\",\r\n \"terminalUUID\": \"f1fad613-9f2b-4e67-ba48-716e3709c136\"\r\n }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var tokenstart = response.Content.IndexOf("token\":\"");
            var token = response.Content.Substring(tokenstart + 8, 32);
            return token;
        }

        public void TurnJukeboxOnOff(int onOff,string tokenstr)
        {
            var client = new RestClient("https://eu-wap.tplinkcloud.com?token=" + tokenstr);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "text/plain");
            var body = $"{{\r\n \"method\":\"passthrough\",\r\n \"params\":{{\r\n \"deviceId\":\"8006A2F2745745F6D47E153BE694623918E1044C\",\r\n \"requestData\":\"{{\\\"system\\\":{{\\\"set_relay_state\\\":{{\\\"state\\\":{onOff}}}}}}}\"\r\n }}\r\n}}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            var message = response.Content;
        }

        private string CreateJbStrip(JbSelectionModel jbmodel)
        {
            var templatepath = @"C:\Users\DiNer0-2\Documents\GitHub\AmiJukeBoxRemote\AmiJukeBoxRemote\gui\assets\templates\" + jbmodel.ImageStripTemplate;
            var imagepath = @"C:\Users\DiNer0-2\Documents\GitHub\AmiJukeBoxRemote\AmiJukeBoxRemote\gui\assets\images\";
            Bitmap bitmap = null;
            var bitMapImage = new
                System.Drawing.Bitmap(System.Drawing.Image.FromFile(templatepath));
            var graphicImage = Graphics.FromImage(bitMapImage);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

            if (jbmodel.Archived == 0)
            {
                // Set Jukebox Selection A
                int opacity = 168;
                graphicImage.DrawString(jbmodel.JbLetter + jbmodel.JbNumberA,
                    new Font("Traveling _Typewriter", 10, FontStyle.Bold),
                    new SolidBrush(Color.FromArgb(opacity, Color.Black)), new Point(6, 31));

                // Set Jukebox Selection B
                graphicImage.DrawString(jbmodel.JbLetter + jbmodel.JbNumberB,
                    new Font("Traveling _Typewriter", 10, FontStyle.Bold),
                    new SolidBrush(Color.FromArgb(opacity, Color.Black)), new Point(6, 51));
            }

            var textSize = TextRenderer.MeasureText(jbmodel.Artist1, new Font("Traveling _Typewriter", 12, FontStyle.Bold, GraphicsUnit.Point));
            graphicImage.DrawString(jbmodel.Artist1,
                new Font("Traveling _Typewriter", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(150 - Math.Min(150, (int)Math.Round(textSize.Width * 0.5)), 40));
            textSize = TextRenderer.MeasureText(jbmodel.A1Song, new Font("Traveling _Typewriter", 12, FontStyle.Bold, GraphicsUnit.Point));
            graphicImage.DrawString(jbmodel.A1Song,
                new Font("Traveling _Typewriter", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(150 - Math.Min(150, (int)Math.Round(textSize.Width * 0.5)), 15));
            textSize = TextRenderer.MeasureText(jbmodel.B1Song, new Font("Traveling _Typewriter", 12, FontStyle.Bold, GraphicsUnit.Point));
            if (textSize.Width > 250)
            {

            }
            graphicImage.DrawString(jbmodel.B1Song,
                new Font("Traveling _Typewriter", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(150 - Math.Min(150, (int)Math.Round(textSize.Width * 0.5)), 70));


            var fileEndName = jbmodel.Id + (jbmodel.Archived == 0 ? "" : "_arch");

            //Save the new image to the response output stream.
            var imagepathcreated = imagepath + jbmodel.ImageStripTemplate.Remove(jbmodel.ImageStripTemplate.Length-4) + "_" + fileEndName + ".png";
            bitMapImage.Save(imagepathcreated, ImageFormat.Png);
            graphicImage.Dispose();
            bitMapImage.Dispose();

            return jbmodel.ImageStripTemplate.Remove(jbmodel.ImageStripTemplate.Length - 4) + "_" + fileEndName + ".png";
        }


        public class SongModel
        {
            public string Artist { get; set; }
            public string SongTitle { get; set; }
            public string Que { get; set; }
        }

        public class JukeboxModel
        {
            public string JbLetter { get; set; }
            public string JbNumber { get; set; }
        }

        public class OnOffObj
        {
            public int OnOff { get; set; }
        }
    }
}