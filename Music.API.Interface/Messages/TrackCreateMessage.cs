using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class TrackCreateMessage : Message
    {
        public byte[] Binary { get; private set; }
        public TrackCreateMessage(byte[] track = null)
        {
            Binary = track;
        }
    }
}
