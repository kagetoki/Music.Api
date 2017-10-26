using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Services.Events;
using Music.API.Services.States;
using System;
using System.Collections.Immutable;

namespace Music.API.Services.Actors
{
    public class ReleaseCreatorActor : ReceivePersistentActor
    {
        public override string PersistenceId => "releases";
        private ActorPath _readStorageUpdateActor;
        private ImmutableHashSet<string> ExistingTrackIds { get; set; }
        public ReleaseCreatorActor(ActorPath readStorageUpdateActor, ImmutableHashSet<string> existingTrackIds)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            Command<ReleaseCreateCommand>(cmd => HandleCreateMessage(cmd));
            Recover<ReleaseCreated>(evt => RecoverOnReleaseCreated(evt));
            ExistingTrackIds = existingTrackIds;
        }

        private bool HandleCreateMessage(ReleaseCreateCommand cmd)
        {
            if (!IsMessageValid(cmd))
            {
                return false;
            }

            var releaseCreatedEvent = new ReleaseCreated
            {
                Artist = cmd.Artist,
                Cover = cmd.Cover,
                ReleaseId = Guid.NewGuid().ToString(),
                Genre = cmd.Genre,
                Title = cmd.Title,
                Timestamp = cmd.Timestamp
            };
            PersistAsync(releaseCreatedEvent, (evt) =>
            {
                var state = GenerateState(evt);
                var child = PlaceChildActor(state);
            });
            return true;
        }

        private bool RecoverOnReleaseCreated(ReleaseCreated releaseCreated)
        {
            if (!Context.Child(releaseCreated.ReleaseId).IsNobody())
            {
                return false;
            }
            var state = GenerateState(releaseCreated);
            PlaceChildActor(state);
            return true;
        }

        private ReleaseState GenerateState(ReleaseCreated evt)
        {
            return new ReleaseState
                (
                    evt.ReleaseId,
                    evt.Artist,
                    evt.Title,
                    evt.Genre,
                    evt.Cover
                );
        }

        private IActorRef PlaceChildActor(ReleaseState state)
        {
            return Context.ActorOf(Props.Create(() => new ReleaseActor(state, ExistingTrackIds, _readStorageUpdateActor)), state.ReleaseId);
        }
        private bool OnTrackCreated(TrackListUpdated trackListUpdated)
        {
            if(trackListUpdated == null || trackListUpdated.TrackIds == null)
            {
                return false;
            }
            this.ExistingTrackIds = trackListUpdated.TrackIds;
            TellAllChildren(trackListUpdated);
            return true;
        }

        private bool IsMessageValid(ReleaseCreateCommand cmd)
        {
            return cmd != null && !string.IsNullOrEmpty(cmd.Artist)
                               && !string.IsNullOrEmpty(cmd.Genre)
                               && !string.IsNullOrEmpty(cmd.Title);
        }

        private void TellAllChildren<T>(T message)
        {
            var children = Context.GetChildren();
            foreach(var child in children)
            {
                child.Tell(message);
            }
        }
    }
}
