using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Commands
{
    public class TrackUpdateCommand : Command
    {
        public string TrackId { get; private set; }
        public byte[] Binary { get; private set; }
        public TrackUpdateCommand(string trackId, byte[] binary)
        {
            TrackId = trackId;
            Binary = binary;
        }
    }
}
