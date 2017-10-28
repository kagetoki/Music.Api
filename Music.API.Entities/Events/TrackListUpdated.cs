using System;
using System.Collections.Immutable;

namespace Music.API.Entities.Events
{
    public class TrackListUpdated : Event
    {
        public ImmutableHashSet<string> TrackIds { get; private set; }
        public TrackListUpdated(ImmutableHashSet<string> trackIds)
        {
            TrackIds = trackIds;
            Timestamp = DateTime.UtcNow;
        }
    }
}
