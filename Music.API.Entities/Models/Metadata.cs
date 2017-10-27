using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entites.Models
{
    public class Metadata
    {
        public string TrackId { get; set; }
        public string ReleaseId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Number { get; set; }
    }
}
