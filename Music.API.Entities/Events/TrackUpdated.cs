using Music.API.Entities.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Events
{
    public class TrackUpdated : Event
    {
        public TrackState OldValue { get; set; }
        public TrackState NewValue { get; set; }
    }
}
