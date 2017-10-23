using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
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
            PersistAsync(_nextTrackId, c => { });
            PersistAsync(cmd, c =>
            {
                var trackId = (_nextTrackId).ToString();
                Context.ActorOf(Props.Create(() => 
                                new TrackActor(trackId, cmd, _readStorageUpdateActor)), trackId);
            });
            return true;
        }
    }
}
