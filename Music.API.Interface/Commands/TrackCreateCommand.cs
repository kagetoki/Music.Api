using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Commands
{
    public class TrackCreateCommand : Command
    {
        public byte[] Binary { get; private set; }
        public TrackCreateCommand(byte[] track = null)
        {
            Binary = track;
        }
    }
}
