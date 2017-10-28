using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class TrackCreateCommand : Command
    {
        public byte[] Binary { get; set; }
        public Guid OwnerId { get; set; }

        public TrackCreateCommand(byte[] track, Guid ownerId)
        {
            OwnerId = ownerId;
            Binary = track;
        }
        public TrackCreateCommand()
        {

        }
    }
}
