using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Interface.States;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Music.API.Services.Actors
{
    public class ReleaseActor : ReceivePersistentActor
    {
        private Release _state;
        public ReleaseActor(long id, ReleaseCreateCommand createMessage)
        {
            Command<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            Recover<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            _state = new Release
            {
                Artist = createMessage.Artist,
                Cover = createMessage.Cover,
                Genre = createMessage.Genre,
                ReleaseId = id.ToString(),
                Timestamp = createMessage.Timestamp,
                Title = createMessage.Title
            };
        }

        public override string PersistenceId => _state.ReleaseId;
        
        private void HandleUpdateMessage(ReleaseUpdateCommand msg)
        {
            if (IsMessageValid(msg))
            {
                Persist(msg, m => 
                {
                    UpdateState(m);
                });
            }
        }

        private void UpdateState(params ReleaseUpdateCommand[] messages)
        {
            var stream = messages.OrderBy(m => m.Timestamp);
            foreach(var msg in stream)
            {
                if (!string.IsNullOrEmpty(msg.Title))
                {
                    _state.Title = msg.Title;
                }
                if (!string.IsNullOrEmpty(msg.Artist))
                {
                    _state.Artist = msg.Artist;
                }
                if (!string.IsNullOrEmpty(msg.Genre))
                {
                    _state.Genre = msg.Genre;
                }
                if (msg.Cover != null)
                {
                    _state.Cover = msg.Cover;
                }
                _state.Timestamp = msg.Timestamp;
            }
        }

        private bool IsMessageValid(ReleaseUpdateCommand msg)
        {
            if(msg == null || msg.ReleaseId != PersistenceId)
            { return false; }
            if(_state.Timestamp >= msg.Timestamp)
            {
                return false;
            }
            //Something has to be updated, otherwise nothing changes and we don't need this message
            return !string.IsNullOrEmpty(msg.Title)
                    || !string.IsNullOrEmpty(msg.Genre)
                    || !string.IsNullOrEmpty(msg.Artist)
                    || msg.Cover != null;
        }
    }
}
