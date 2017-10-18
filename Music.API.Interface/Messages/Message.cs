using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class Message
    {
        public DateTime Timestamp { get; private set; }
        public Message()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
