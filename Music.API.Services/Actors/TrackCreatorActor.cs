using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Services.Events;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Music.API.Services.Actors
{
    public class TrackCreatorActor : ReceivePersistentActor
    {
        public override string PersistenceId => "tracks";
        private long _nextTrackId;
        private ActorPath _readStorageUpdateActor;
        private ActorPath _releaseCreatorActor;
        private ImmutableHashSet<string> _trackIds;
        public TrackCreatorActor(ActorPath readStorageUpdateActor, ImmutableHashSet<string> trackIds)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            _trackIds = trackIds;
            Command<TrackCreateCommand>(cmd => HandleTrackCreated(cmd));
            TellTrackListUpdated();
        }

        private bool HandleTrackCreated(TrackCreateCommand cmd)
        {
            if (cmd == null || cmd.Binary == null)
            {
                return false;
            }
            ++_nextTrackId;
            var trackCreatedEvt = new TrackCreated
            {
                Binary = cmd.Binary,
                Timestamp = cmd.Timestamp,
                TrackId = _nextTrackId.ToString(),
            };
            var state = new States.TrackState(trackCreatedEvt.TrackId, trackCreatedEvt.Binary);
            PersistAsync(trackCreatedEvt, c =>
            {
                _trackIds = _trackIds.Add(c.TrackId);
                Context.ActorOf(Props.Create(() => 
                                new TrackActor(state, _readStorageUpdateActor)), trackCreatedEvt.TrackId);
                TellTrackListUpdated();
            });
            return true;
        }

        private void TellTrackListUpdated()
        {
            Context.ActorSelection(_releaseCreatorActor).Tell(new TrackListUpdated(_trackIds));
        }
    }
}
