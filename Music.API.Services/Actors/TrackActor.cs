using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Services.Messages;
using Music.API.Services.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Actors
{
    public class TrackActor : ReceivePersistentActor
    {
        public override string PersistenceId => _state.TrackId;
        private TrackState _state;
        private ActorPath _readStorageUpdateActor;
        public TrackActor(string trackId, TrackCreateCommand cmd, ActorPath readStorageUpdateActor)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            _state = new TrackState(trackId, cmd.Binary);
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
