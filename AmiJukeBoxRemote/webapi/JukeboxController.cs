using System.Web.Http;

namespace AmiJukeboxRemote.webapi
{
    //public class IgnoreActivityViewModel
    //{
    //    public Guid Id { get; set; }
    //    public string ActivityCode { get; set; }
    //}

    [System.Web.Http.RoutePrefix("api/jukebox")]
    public class JukeboxController : ApiController
    {

        [Route("cancel")]
        [System.Web.Http.HttpPost]
        public bool CancelRecordPlaying()
        {
            return false; 
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