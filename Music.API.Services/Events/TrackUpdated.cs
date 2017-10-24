using Music.API.Services.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Events
{
    public class TrackUpdated : Event
    {
        public TrackState OldValue { get; set; }
        public TrackState NewValue { get; set; }
    }
}
