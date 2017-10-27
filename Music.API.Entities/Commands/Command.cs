using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class Command
    {
        public DateTime Timestamp { get; set; }
        public Command()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
