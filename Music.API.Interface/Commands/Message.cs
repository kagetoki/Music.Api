using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Commands
{
    public class Command
    {
        public DateTime Timestamp { get; private set; }
        public Command()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
