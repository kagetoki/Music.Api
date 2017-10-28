using Akka.Actor;
using Akka.Persistence;
using Music.API.Entities.Events;
using System.Collections.Immutable;
using Music.API.Entities.States;
using Music.API.Entities.Commands;

namespace Music.API.Services.Actors
{
    public class TrackCreatorActor : ReceivePersistentActor
    {
        public override string PersistenceId => "tracks";
        private ActorPath _readStorageUpdateActor;
        private ActorPath _releaseCreatorActor;
        private ImmutableHashSet<string> _trackIds;
        public TrackCreatorActor(ActorPath readStorageUpdateActor, ActorPath releaseCreatorActor, ImmutableHashSet<string> trackIds)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            _releaseCreatorActor = readStorageUpdateActor;
            _trackIds = trackIds;
            Command<TrackCreated>(cmd => HandleTrackCreated(cmd));
            TellTrackListUpdated();
        }

        private bool HandleTrackCreated(TrackCreated trackCreatedEvt)
        {
            if (trackCreatedEvt == null || trackCreatedEvt.Binary == null)
            {
                return false;
            }
            
            PersistAsync(trackCreatedEvt, c =>
            {
                var state = new TrackState(trackCreatedEvt.TrackId, trackCreatedEvt.Binary, trackCreatedEvt.OwnerId);
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
