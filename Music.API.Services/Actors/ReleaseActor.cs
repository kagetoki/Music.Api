using Akka.Actor;
using Akka.Persistence;
using Music.API.Interface.Commands;
using Music.API.Services.Events;
using Music.API.Services.States;

namespace Music.API.Services.Actors
{
    public class ReleaseActor : ReceivePersistentActor
    {
        private ActorPath _readStorageUpdateActor;
        private ReleaseState _state;
        public ReleaseActor(ReleaseState state, ActorPath readStorageUpdateActor)
        {
            _readStorageUpdateActor = readStorageUpdateActor;
            Command<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            Recover<ReleaseUpdateCommand>(msg => HandleUpdateMessage(msg));
            _state = state;
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

        private bool UpdateState(MetadataUpdateCommand command)
        {
            
        }

        private bool IsMetadataAddValid(MetadataCreateCommand command)
        {
            if(command == null || command.ReleaseId != PersistenceId)
            {
                return false;
            }
            //TODO: ping track actor if the track with this ID exists
            return !string.IsNullOrEmpty(command.Title)
                    || !string.IsNullOrEmpty(command.Genre)
                    || !string.IsNullOrEmpty(command.Artist)
                    || !string.IsNullOrEmpty(command.TrackId)
                    || !string.IsNullOrEmpty(command.Album);
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
