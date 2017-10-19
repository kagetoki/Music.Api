using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Messages;
using System;
using System.Collections.Generic;
using System.Text;

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
            //Pass some Props with info of creation and id. Maybe generate initial state of actor
            //combining info from creation and _nextReleaseId
            Context.ActorOf<ReleaseActor>();
            throw new NotImplementedException();
        }

        private bool IsMessageValid(ReleaseCreateMessage msg)
        {
            return msg != null && !string.IsNullOrEmpty(msg.Artist)
                               && !string.IsNullOrEmpty(msg.Genre)
                               && !string.IsNullOrEmpty(msg.Title);
        }
    }
}
