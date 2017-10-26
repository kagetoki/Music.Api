using Akka.Actor;
using Music.API.DataAccess.Abstractions;
using Music.API.Entities;
using Music.API.Entities.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Actors
{
    public class ReadStorageActor : ReceiveActor
    {
        private readonly IReleaseProvider _releaseProvider;
        private readonly ITrackProvider _trackProvider;

        public ReadStorageActor(IReleaseProvider releaseProvider, ITrackProvider trackProvider)
        {
            _releaseProvider = releaseProvider;
            _trackProvider = trackProvider;
            Receive<ReleaseState>(rs => OnReleaseStateChange(rs));
            Receive<TrackState>(ts => OnTrackStateChange(ts));
        }

        public void OnReleaseStateChange(ReleaseState state)
        {
            InMemoryAppState.AddOrUpdate(state);
            _releaseProvider.Store(state);
        }

        public void OnTrackStateChange(TrackState state)
        {
            _trackProvider.Store(state);
        }
    }
}
