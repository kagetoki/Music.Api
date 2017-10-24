using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Services.Events;
using Music.API.Services.States;

namespace Music.API.Services.Actors
{
    public class ReleaseCreatorActor : ReceivePersistentActor
    {
        private long _nextReleaseId;
        public override string PersistenceId => "releases";
        private ActorPath _readStorageUpdateActor;
        public ReleaseCreatorActor(ActorPath readStorageUpdateActor)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            Command<ReleaseCreateCommand>(cmd => HandleCreateMessage(cmd));
        }

        private bool HandleCreateMessage(ReleaseCreateCommand cmd)
        {
            if (!IsMessageValid(cmd))
            {
                return false;
            }
            PersistAsync(cmd, c => { });
            _nextReleaseId++;
            var releaseCreatedEvent = new ReleaseCreated
            {
                Artist = cmd.Artist,
                Cover = cmd.Cover,
                ReleaseId = _nextReleaseId.ToString(),
                Genre = cmd.Genre,
                Title = cmd.Title,
                Timestamp = cmd.Timestamp
            };
            PersistAsync(releaseCreatedEvent, (evt) =>
            {
                var state = new ReleaseState
                (
                    evt.ReleaseId,
                    evt.Artist,
                    evt.Title,
                    evt.Genre,
                    evt.Cover
                );
                var child = Context.ActorOf(Props.Create(() => new ReleaseActor(state, _readStorageUpdateActor)));
                
            });
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
