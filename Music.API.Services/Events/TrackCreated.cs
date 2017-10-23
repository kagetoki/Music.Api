using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Events
{
    public class TrackCreated : CreatedEvent
    {
        public string TrackId { get; set; }
        public string ReleaseId { get; set; }
        public byte[] Binary { get; set; }
    }
}
