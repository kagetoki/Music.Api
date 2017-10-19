using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Messages;
using Music.API.Interface.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Actors
{
    public class ReleaseActor : ReceivePersistentActor
    {
        private Release _state;
        public ReleaseActor()
        {
            Command<ReleaseUpdateMessage>(msg => HandleUpdateMessage(msg));
            Recover<ReleaseUpdateMessage>(msg => HandleUpdateMessage(msg));
        }

        public override string PersistenceId => _state.ReleaseId;
        private void HandleUpdateMessage(ReleaseUpdateMessage msg)
        {
            if (IsMessageValid(msg))
            {
                Persist(msg, m => 
                {
                    //update state

                    //should I store here to db or it's automatic?
                });
            }
        }

        private bool IsMessageValid(ReleaseUpdateMessage msg)
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
