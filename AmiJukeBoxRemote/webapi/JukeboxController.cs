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

        [Route("cancel")]
        [System.Web.Http.HttpGet]
        public bool CancelRecordPlaying()
        {
            _mqtt.SendCancelToSubscriber();
            return true;
        }

        [Route("savestrip")]
        [HttpPost]
        public bool SaveStripToDb(JbSelectionModel jbmodel)
        {
            jbmodel.ImageStripName = CreateJbStrip(jbmodel);
            return false;
        }

        [Route("createstrips")]
        [HttpGet]
        public bool CreateAllStrips()
        {
            List<JbSelectionModel> allSelections = _jbDb.GetAllSelections();
            foreach (var jbmodel in allSelections)
            {
                jbmodel.ImageStripName = CreateJbStrip(jbmodel);
                _jbDb.UpdateImagePath(jbmodel);
            }
            return true;
        }

        [Route("getalljukeboxselections")]
        [HttpGet]
        public List<JbSelectionModel> GetAllJukeboxSelectíons()
        {
            return _jbDb.GetAllSelections();
        }


        private string CreateJbStrip(JbSelectionModel jbmodel)
        {
            var imagepath = @"C:\Users\DiNer0-2\Documents\GitHub\AmiJukeBoxRemote\AmiJukeBoxRemote\gui\assets\images\" + jbmodel.ImageStripTemplate;
            Bitmap bitmap = null;
            var bitMapImage = new
                System.Drawing.Bitmap(Image.FromFile(imagepath));
            var graphicImage = Graphics.FromImage(bitMapImage);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Set Jukebox Selection A
            int opacity = 168;
            graphicImage.DrawString(jbmodel.JbLetter+jbmodel.JbNumberA,
                new Font("Traveling _Typewriter", 10, FontStyle.Bold),
                new SolidBrush(Color.FromArgb(opacity, Color.Black)), new Point(3, 31));
            
            // Set Jukebox Selection B
            graphicImage.DrawString(jbmodel.JbLetter + jbmodel.JbNumberB,
                new Font("Traveling _Typewriter", 10, FontStyle.Bold),
                new SolidBrush(Color.FromArgb(opacity, Color.Black)), new Point(3, 51));

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

            //Save the new image to the response output stream.
            var imagepathcreated = @"C:\Users\DiNer0-2\Documents\GitHub\AmiJukeBoxRemote\AmiJukeBoxRemote\gui\assets\images\" + jbmodel.ImageStripTemplate.Remove(jbmodel.ImageStripTemplate.Length-4) + "_" + jbmodel.JbNumeric + ".png";
            bitMapImage.Save(imagepathcreated, ImageFormat.Png);
            graphicImage.Dispose();
            bitMapImage.Dispose();

            return jbmodel.ImageStripTemplate.Remove(jbmodel.ImageStripTemplate.Length - 4) + "_" + jbmodel.JbNumeric + ".png";
        }

        //[Route("pickup")]
        //[HttpGet]
        //public List<IgnoreActivityPickupModel> ListAllPickupIgnoreActivities()
        //{
        //    return (List<IgnoreActivityPickupModel>)CacheHandlerService.GetPickupIgnoreActivities();
        //}

        //[Route("insertactcodepassive")]
        //[HttpPut]
        //public bool InsertPassiveActivityCode(IgnoreActivityPassiveModel model)
        //{
        //    var ignoreAct = new IgnoreActivityPassiveModel();
        //    ignoreAct.ActivityCode = model.ActivityCode;
        //    return CacheHandlerService.InsertPassiveActivityCode(ignoreAct);
        //}

        //[Route("deleteactcodepassive")]
        //[HttpPut]
        //public bool DeletePassiveActivityCode(IgnoreActivityPassiveModel model)
        //{
        //    var ignoreAct = new IgnoreActivityPassiveModel();
        //    ignoreAct.ActivityCode = model.ActivityCode;
        //    return CacheHandlerService.DeletePassiveActivityCode(ignoreAct);
        //}
        //[Route("insertactcodepickup")]
        //[HttpPut]
        //public bool InsertPickupActivityCode(IgnoreActivityPickupModel model)
        //{
        //    var ignoreAct = new IgnoreActivityPickupModel();
        //    ignoreAct.ActivityCode = model.ActivityCode;
        //    return CacheHandlerService.InsertPickupActivityCode(ignoreAct);
        //}

        //[Route("deleteactcodepickup")]
        //[HttpPut]
        //public bool DeletePickupActivityCode(IgnoreActivityPickupModel model)
        //{
        //    var ignoreAct = new IgnoreActivityPickupModel();
        //    ignoreAct.ActivityCode = model.ActivityCode;
        //    return CacheHandlerService.DeletePickupActivityCode(ignoreAct);
        //}

    }
}