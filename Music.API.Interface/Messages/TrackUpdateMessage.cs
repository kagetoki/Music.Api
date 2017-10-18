using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class TrackUpdateMessage : Message
    {
        public long TrackId { get; private set; }

    }
}
