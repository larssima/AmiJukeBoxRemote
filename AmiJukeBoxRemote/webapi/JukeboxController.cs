using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Http;
using System.Windows.Forms;
using AmiJukeBoxRemote.Database;
using AmiJukeBoxRemote.Models;
using AmiJukeBoxRemote.Mqtt;
using AmiJukeBoxRemote.Spotify;

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
            //var alljbmodel = _jbDb.GetJbSelection(jbmodel.Id);
            //CreateJbStrip(alljbmodel);
            //_jbDb.UpdateImagePath(alljbmodel);
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
        
        [Route("createstrips")]
        [HttpGet]
        public bool CreateAllStrips()
        {
            CleanImagesDIrectory();
            List<JbSelectionModel> allSelections = _jbDb.GetAllSelections();
            allSelections.AddRange(_jbDb.GetAllArchivedSelections());
            foreach (var jbmodel in allSelections)
            {
                jbmodel.ImageStripName = CreateJbStrip(jbmodel);
                _jbDb.UpdateImagePath(jbmodel);
            }
            return true;
        }

        private void CleanImagesDIrectory()
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
            return _jbDb.GetAllSelections();
        }

        [Route("getallarchivedjukeboxselections")]
        [HttpGet]
        public List<JbSelectionModel> GetAllArchivedJukeboxSelectíons()
        {
            return _jbDb.GetAllArchivedSelections();
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
    }
}