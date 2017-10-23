﻿using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;

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
            PersistAsync(_nextReleaseId, (nextId) =>
            {
                var child = Context.ActorOf(Props.Create(() => new ReleaseActor(nextId, cmd, _readStorageUpdateActor)));
                
            });
            return true;
        }

        private bool IsMessageValid(ReleaseCreateCommand cmd)
        {
            return cmd != null && !string.IsNullOrEmpty(cmd.Artist)
                               && !string.IsNullOrEmpty(cmd.Genre)
                               && !string.IsNullOrEmpty(cmd.Title);
        }
    }
}
