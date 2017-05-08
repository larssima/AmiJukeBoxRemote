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
            jbmodel.ImageStripName = CreateJbStrip(jbmodel.ImageStripTemplate,jbmodel);
            return false;
        }

        [Route("createstrips")]
        [HttpGet]
        public bool CreateAllStrips()
        {
            List<JbSelectionModel> allSelections = _jbDb.GetAllSelections();
            return true;
        }

        private string CreateJbStrip(string jbmodelImageStripTemplate, JbSelectionModel jbmodel)
        {
            var imagepath = "~/gui/assets/images/" + jbmodelImageStripTemplate;
            Bitmap bitmap = null;
            var bitMapImage = new
                System.Drawing.Bitmap(imagepath);
            var graphicImage = Graphics.FromImage(bitMapImage);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            var textSize = TextRenderer.MeasureText(jbmodel.Artist1, new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point));
            graphicImage.DrawString(jbmodel.Artist1,
                new Font("Arial", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(150 - Math.Min(150, (int)Math.Round(textSize.Width * 0.5)), 40));
            textSize = TextRenderer.MeasureText(jbmodel.A1Song, new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point));
            graphicImage.DrawString(jbmodel.A1Song,
                new Font("Arial", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(150 - Math.Min(150, (int)Math.Round(textSize.Width * 0.5)), 15));
            textSize = TextRenderer.MeasureText(jbmodel.B1Song, new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point));
            if (textSize.Width > 250)
            {

            }
            graphicImage.DrawString(jbmodel.B1Song,
                new Font("Arial", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(150 - Math.Min(150, (int)Math.Round(textSize.Width * 0.5)), 70));

            //Save the new image to the response output stream.
            var imagepathcreated = "~/gui/assets/images/" + jbmodelImageStripTemplate + jbmodel.JbNumeric + ".png";
            bitMapImage.Save("~/gui/assets/images/" + jbmodelImageStripTemplate +jbmodel.JbNumeric, ImageFormat.Png);
            graphicImage.Dispose();
            bitMapImage.Dispose();

            //using (var stream = File.OpenRead(imagepath))
            //{
            //    bitmap = (Bitmap)Image.FromStream(stream);
            //}
            //using (bitmap)
            //using (var graphics = Graphics.FromImage(bitmap))
            //using (var font = new Font("Arial", 20, FontStyle.Regular))
            //{
            //    // Do what you want using the Graphics object here.
            //    graphics.DrawString("Hello World!", font, Brushes.Red, 0, 0);

            //    // Important part!
            //    bitmap.Save(imagepath);
            //}
            return imagepathcreated;
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