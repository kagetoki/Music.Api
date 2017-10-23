using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Events
{
    public class MetadataCreated : CreatedEvent
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string TrackId { get; set; }
    }
}
