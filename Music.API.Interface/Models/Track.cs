using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Models
{
    public class Track
    {
        public string TrackId { get; set; }
        public string ReleaseId { get; set; }
        public byte[] Binary { get; set; }
    }
}
