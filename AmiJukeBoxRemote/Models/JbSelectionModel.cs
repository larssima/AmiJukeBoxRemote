using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiJukeBoxRemote.Models
{
    public class JbSelectionModel
    {
        public int Id { get; set;}
        public string JbLetter { get; set; }
        public string JbNumberA { get; set; }
        public string JbNumberB { get; set; }
        public int JbNumeric { get; set; }
        public string A1Song { get; set; }
        public string A2Song { get; set; }
        public string B1Song { get; set; }
        public string B2Song { get; set; }
        public string Artist1 { get; set; }
        public string Artist2 { get; set; }
        public string ImageStripName { get; set; }
        public string MusicCategory { get; set; }
        public string DiscogsLink { get; set; }
        public string SpotifyUri { get; set; }
        public int Archived { get; set; }
        public string ImageStripTemplate { get; set; }
        
        // Local class variable
        public bool SelectedForPrint { get; set; } = false;
    }
}