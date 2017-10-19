using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Messages;

namespace Music.API.Services.Actors
{
    public class ReleaseCreatorActor : ReceivePersistentActor
    {
        private long _nextReleaseId;
        public override string PersistenceId => "release";

        public ReleaseCreatorActor()
        {
            Command<ReleaseCreateMessage>(msg => HandleCreateMessage(msg));
        }

        private bool HandleCreateMessage(ReleaseCreateMessage msg)
        {
            if (!IsMessageValid(msg))
            {
                return false;
            }
            
            Persist(_nextReleaseId, (nextId) =>
            {
                var child = Context.ActorOf(Props.Create(() => new ReleaseActor(_nextReleaseId, msg)));
                _nextReleaseId++;
            });
            
            return true;
        }

        private bool IsMessageValid(ReleaseCreateMessage msg)
        {
            return msg != null && !string.IsNullOrEmpty(msg.Artist)
                               && !string.IsNullOrEmpty(msg.Genre)
                               && !string.IsNullOrEmpty(msg.Title);
        }
    }
}
