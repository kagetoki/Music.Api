using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Services.States;

namespace Music.API.Services.Actors
{
    public class TrackActor : ReceivePersistentActor
    {
        public override string PersistenceId => _state.TrackId;
        private TrackState _state;
        private ActorPath _readStorageUpdateActor;
        public TrackActor(TrackState trackState, ActorPath readStorageUpdateActor)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            _state = trackState;
            Command<TrackUpdateCommand>(c => HandleUpdateCommand(c));
            TellStateUpdated();
        }

        private void HandleUpdateCommand(TrackUpdateCommand cmd)
        {
            if(cmd != null && cmd.Binary != null && cmd.Timestamp > _state.Timestamp)
            {
                PersistAsync(cmd, (c) =>
                {
                    _state = _state.Update(c);
                    TellStateUpdated();
                });
            }
        }

        private void TellStateUpdated()
        {
            Context.ActorSelection(_readStorageUpdateActor).Tell(_state);
        }
    }
}
