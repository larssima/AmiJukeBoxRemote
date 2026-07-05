namespace AmiJukeBoxService.Models;

public class JbSelectionModel
{
    public int Id { get; set; }
    public string JbLetter { get; set; } = "";
    public string JbNumberA { get; set; } = "";
    public string JbNumberB { get; set; } = "";
    public int JbNumeric { get; set; }
    public string A1Song { get; set; } = "";
    public string A2Song { get; set; } = "";
    public string B1Song { get; set; } = "";
    public string B2Song { get; set; } = "";
    public string Artist1 { get; set; } = "";
    public string Artist2 { get; set; } = "";
    public string ImageStripName { get; set; } = "";
    public string MusicCategory { get; set; } = "";
    public string DiscogsLink { get; set; } = "";
    public string SpotifyUri { get; set; } = "";
    public int Archived { get; set; }
    public string ImageStripTemplate { get; set; } = "";
    public bool SelectedForPrint { get; set; } = false;
}

public class GenericAPIResultModel
{
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public int? Id { get; set; }
}

public class SongModel
{
    public string Artist { get; set; } = "";
    public string SongTitle { get; set; } = "";
    public string Que { get; set; } = "";
}

public class JukeboxModel
{
    public string JbLetter { get; set; } = "";
    public string JbNumber { get; set; } = "";
}

public class OnOffObj
{
    public int OnOff { get; set; }
}
