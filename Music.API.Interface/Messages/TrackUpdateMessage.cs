using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class TrackUpdateMessage : Message
    {
        public string TrackId { get; private set; }
        public byte[] Binary { get; private set; }
        public TrackUpdateMessage(string trackId, byte[] binary)
        {
            TrackId = trackId;
            Binary = binary;
        }
    }
}
