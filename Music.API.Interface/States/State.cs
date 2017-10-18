using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.States
{
    public class State
    {
        public DateTime Timestamp { get; private set; }
        public State()
        {
            UpdateTimestamp();
        }
        protected void UpdateTimestamp()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
