using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Events
{
    public abstract class Event
    {
        public DateTime Timestamp { get; set; }
    }
    
}
