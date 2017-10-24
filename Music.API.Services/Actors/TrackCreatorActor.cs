using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Services.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Actors
{
    public class TrackCreatorActor : ReceivePersistentActor
    {
        public override string PersistenceId => "tracks";
        private long _nextTrackId;
        private ActorPath _readStorageUpdateActor;
        public TrackCreatorActor(ActorPath readStorageUpdateActor)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            Command<TrackCreateCommand>(cmd => HandleTrackCreated(cmd));
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
                Context.ActorOf(Props.Create(() => 
                                new TrackActor(state, _readStorageUpdateActor)), trackCreatedEvt.TrackId);
            });
            return true;
        }
    }
}
