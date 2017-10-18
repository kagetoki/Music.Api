using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.States
{
    public class Release : State
    {
        public string ReleaseId { get; set; }
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
    }
}
