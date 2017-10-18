using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class MetadataCreateMessage : Message
    {
        public string TrackId { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
    }
}
