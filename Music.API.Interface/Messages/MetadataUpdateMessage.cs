using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class MetadataUpdateMessage : Message
    {
        public long TrackId { get; private set; }
        public string Artist { get; private set; }
    }
}
