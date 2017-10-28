using Akka.Actor;
using Akka.Persistence;
using Music.API.Entities.Events;
using Music.API.Entities.States;
using System.Collections.Immutable;
using Music.API.Entities.Commands;
using System;

namespace Music.API.Services.Actors
{
    public class ReleaseActor : ReceivePersistentActor
    {
        private ActorPath _readStorageUpdateActor;
        private ReleaseState _state;
        private ImmutableHashSet<string> TrackIds { get; set; }
        public ReleaseActor(ReleaseState state, ImmutableHashSet<string> exitstingTrackIds, ActorPath readStorageUpdateActor)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            Command<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            Command<TrackListUpdated>(msg => OnTrackListUpdated(msg));
            Command<MetadataCreateCommand>(m => HandleMetadataAdded(m));
            Command<MetadataUpdateCommand>(m => OnMetadataUpdated(m));
            Command<SubscriptionCreateCommand>(c => OnSubscriptionCreate(c));
            Command<SubscriptionReplaceCommand>(c => OnSubscriptionReplace(c));
            Recover<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            Recover<TrackListUpdated>(msg => OnTrackListUpdated(msg));
            Recover<MetadataCreateCommand>(m => HandleMetadataAdded(m));
            Recover<MetadataUpdateCommand>(m => OnMetadataUpdated(m));
            Recover<SubscriptionCreateCommand>(c => OnSubscriptionCreate(c));
            Recover<SubscriptionReplaceCommand>(c => OnSubscriptionReplace(c));
            _state = state;
            TrackIds = exitstingTrackIds;
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

        private bool HandleMetadataAdded(MetadataCreateCommand command)
        {
            if (!IsMetadataAddValid(command))
            {
                return false;
            }
            var evt = new MetadataCreated
            {
                TrackId = command.TrackId,
                Album = command.Album,
                Artist = command.Artist,
                Genre = command.Genre,
                Timestamp = command.Timestamp,
                Title = command.Title
            };
            Persist(evt, e => 
            {
                _state = _state.AddMetadata(command);
                TellStateUpdated();
            });
            return true;
        }

        private bool OnMetadataUpdated(MetadataUpdateCommand command)
        {
            if(!CommandValidator.Validate(command).Success 
                || command.ReleaseId != PersistenceId 
                || !_state.TrackList.ContainsKey(command.TrackId)
                || command.Timestamp <= _state.TrackList[command.TrackId].Timestamp)
            {
                return false;
            }
            Persist(command, cmd =>
            {
                _state = _state.UpdateMetadata(cmd);
                TellStateUpdated();
            });
            return true;
        }

        private bool OnSubscriptionCreate(SubscriptionCreateCommand command)
        {
            if (!IsSubscriptionValid(command) || _state.Subscription != null)
            {
                return false;
            }

            PersistAsync(command, cmd =>
            {
                _state = _state.AddSubscription(Guid.NewGuid().ToString(), command);
                TellStateUpdated();
            });
            return true;
        }

        private bool OnSubscriptionReplace(SubscriptionReplaceCommand command)
        {
            if (!IsSubscriptionValid(command))
            {
                return false;
            }

            PersistAsync(command, cmd =>
            {
                _state = _state.ReplaceSubscription(command);
                TellStateUpdated();
            });
            return true;
        }

        private bool IsSubscriptionValid(SubscriptionCreateCommand command)
        {
            return command != null && command.ReleaseId == PersistenceId && command.UtcExpiration > DateTime.UtcNow;
        }

        private bool IsSubscriptionValid(SubscriptionReplaceCommand command)
        {
            return command != null && command.ReleaseId == PersistenceId && command.UtcExpiration > DateTime.UtcNow
                && _state.Subscription != null && _state.Subscription.SubscriptionId == command.SubscriptionId;
        }

        private bool IsMetadataAddValid(MetadataCreateCommand command)
        {
            return CommandValidator.Validate(command).Success
                && command.ReleaseId == PersistenceId 
                && TrackIds.Contains(command.TrackId);
        }

        private bool IsMessageValid(ReleaseUpdateCommand cmd)
        {
            if(cmd == null || cmd.ReleaseId != PersistenceId)
            { return false; }
            if(_state.Timestamp >= cmd.Timestamp)
            {
                return false;
            }
            return CommandValidator.Validate(cmd).Success;
        }

        private bool OnTrackListUpdated(TrackListUpdated trackListUpdated)
        {
            if (trackListUpdated == null || trackListUpdated.TrackIds == null)
            {
                return false;
            }
            this.TrackIds = trackListUpdated.TrackIds;
            return true;
        }
    }
}
