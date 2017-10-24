using System.Collections.Immutable;

namespace Music.API.Services.Events
{
    class TrackListUpdated : Event
    {
        public ImmutableHashSet<string> TrackIds { get; private set; }
        public TrackListUpdated(ImmutableHashSet<string> trackIds)
        {
            TrackIds = trackIds;
        }
    }
}
