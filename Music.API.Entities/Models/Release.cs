using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entites.Models
{
    public class Release
    {
        public string ReleaseId { get; set; }
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public List<Metadata> Tracks { get; set; }
        public Subscription Subscription { get; set; }
        public Release()
        {
            Tracks = new List<Metadata>();
        }
    }
}
