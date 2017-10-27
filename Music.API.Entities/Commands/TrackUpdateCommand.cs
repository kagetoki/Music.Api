using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class TrackUpdateCommand : Command
    {
        public string TrackId { get; set; }
        public byte[] Binary { get; set; }
        public TrackUpdateCommand(string trackId, byte[] binary)
        {
            TrackId = trackId;
            Binary = binary;
        }
        public TrackUpdateCommand()
        {

        }
    }
}
