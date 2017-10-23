using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Interface.Models;
using Music.API.Services.Messages;
using Music.API.Services.States;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Music.API.Services.Actors
{
    public class ReleaseActor : ReceivePersistentActor
    {
        private ActorPath _readStorageUpdateActor;
        private ReleaseState _state;
        public ReleaseActor(long id, ReleaseCreateCommand createMessage, ActorPath readStorageUpdateActor)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            Command<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            Recover<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            _state = new ReleaseState
            (
                id.ToString(),
                createMessage.Artist,
                createMessage.Title,
                createMessage.Genre,
                createMessage.Cover
            );
            TellStateUpdated();
        }

        public override string PersistenceId => _state.ReleaseId;
        
        private bool HandleUpdateMessage(ReleaseUpdateCommand cmd)
        {
            if (IsMessageValid(cmd))
            {
                Persist(cmd, c => 
                {
                    UpdateState(c);
                    TellStateUpdated();
                });
                return true;
            }
            return false;
        }

        private void TellStateUpdated()
        {
            var selection = Context.ActorSelection(_readStorageUpdateActor);
            selection.Tell(_state, Context.Self);
        }

        private void UpdateState(ReleaseUpdateCommand command)
        {
            _state = _state.Update(command);
        }

        private bool IsMessageValid(ReleaseUpdateCommand cmd)
        {
            if(cmd == null || cmd.ReleaseId != PersistenceId)
            { return false; }
            if(_state.Timestamp >= cmd.Timestamp)
            {
                return false;
            }
            //Something has to be updated, otherwise nothing changes and we don't need this message
            return !string.IsNullOrEmpty(cmd.Title)
                    || !string.IsNullOrEmpty(cmd.Genre)
                    || !string.IsNullOrEmpty(cmd.Artist)
                    || cmd.Cover != null;
        }
    }
}
