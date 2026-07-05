using Microsoft.AspNetCore.Mvc;
using RestSharp;
using AmiJukeBoxService.Database;
using AmiJukeBoxService.Images;
using AmiJukeBoxService.Models;
using AmiJukeBoxService.Mqtt;

namespace AmiJukeBoxService.Controllers;

[ApiController]
[Route("api/amijukebox")]
[Route("AmiJukeBoxRemote/api/amijukebox")]
public class JukeboxController : ControllerBase
{
    private readonly DatabaseFunctions _db;
    private readonly MqttService _mqtt;
    private readonly ImageStripService _images;
    private readonly IConfiguration _config;

    public JukeboxController(DatabaseFunctions db, MqttService mqtt, ImageStripService images, IConfiguration config)
    {
        _db = db;
        _mqtt = mqtt;
        _images = images;
        _config = config;
    }

    [HttpGet("cancel")]
    public bool CancelRecordPlaying()
    {
        _mqtt.SendCancel();
        return true;
    }

    [HttpPut("playsongonjukebox")]
    public bool PlaySongOnJukebox(JukeboxModel model)
    {
        _mqtt.PlaySelection(model);
        return true;
    }

    [HttpGet("getalljukeboxselections")]
    public IActionResult GetAllJukeboxSelections()
    {
        try
        {
            TurnJukeboxOnOff(1);
        }
        catch { /* non-critical, don't fail the request */ }

        try
        {
            return Ok(_db.GetAllSelections());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("getallarchivedjukeboxselections")]
    public List<JbSelectionModel> GetAllArchivedJukeboxSelections()
    {
        return _db.GetAllArchivedSelections();
    }

    [HttpPut("archiveselection")]
    public bool ArchiveSelection(JbSelectionModel model)
    {
        if (!_db.ArchiveSelection(model.Id)) return false;
        CreateAllStrips();
        return true;
    }

    [HttpPut("reinstateselection")]
    public bool ReinstateSelection(JbSelectionModel model)
    {
        var ok = _db.ReinstateSelection(model);
        CreateAllStrips();
        return ok;
    }

    [HttpPut("savestrip")]
    public bool SaveStrip(JbSelectionModel model)
    {
        if (_db.SaveToDataBase(model) < 0) return false;
        model.ImageStripName = _images.CreateStrip(model);
        _db.UpdateImagePath(model);
        return true;
    }

    [HttpPut("updateselection")]
    public GenericAPIResultModel UpdateSelection(JbSelectionModel model)
    {
        if (!_db.UpdateInDataBase(model))
            return new GenericAPIResultModel { Success = false, Message = "Could not update in database" };
        CreateAllStrips();
        return new GenericAPIResultModel { Success = true };
    }

    [HttpGet("createstrips")]
    public bool CreateAllStrips()
    {
        _images.CleanImagesDirectory();
        var all = _db.GetAllSelections();
        all.AddRange(_db.GetAllArchivedSelections());
        foreach (var m in all)
        {
            m.ImageStripName = _images.CreateStrip(m);
            _db.UpdateImagePath(m);
        }
        return true;
    }

    [HttpPut("turnonoff")]
    public void TurnOnOff(OnOffObj model) => TurnJukeboxOnOff(model.OnOff);

    // TP-Link smart plug control
    private void TurnJukeboxOnOff(int onOff)
    {
        try
        {
            var token = GetTpLinkToken();
            if (string.IsNullOrEmpty(token)) return;

            var client = new RestClient($"https://eu-wap.tplinkcloud.com?token={token}");
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(new
            {
                method = "passthrough",
                @params = new
                {
                    deviceId = "8006A2F2745745F6D47E153BE694623918E1044C",
                    requestData = $"{{\"system\":{{\"set_relay_state\":{{\"state\":{onOff}}}}}}}"
                }
            });
            client.Execute(request);
        }
        catch { /* non-critical */ }
    }

    private string GetTpLinkToken()
    {
        try
        {
            var username = _config["AppSettings:TpLink:Username"];
            var password = _config["AppSettings:TpLink:Password"];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return "";

            var client = new RestClient("https://wap.tplinkcloud.com");
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(new
            {
                method = "login",
                @params = new
                {
                    appType = "Kasa_Android",
                    cloudUserName = username,
                    cloudPassword = password,
                    terminalUUID = "f1fad613-9f2b-4e67-ba48-716e3709c136"
                }
            });
            var response = client.Execute(request);
            var content = response.Content ?? "";
            var idx = content.IndexOf("token\":\"");
            return idx >= 0 ? content.Substring(idx + 8, 32) : "";
        }
        catch { return ""; }
    }
}
