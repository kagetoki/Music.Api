using Music.API.Entities.Commands;
using System;

namespace Music.API.Entities.States
{
    public class TrackState : State
    {
        public string TrackId { get; private set; }
        public byte[] Binary { get; private set; }
        public Guid OwnerId { get; private set; }
        public TrackState(string trackId, byte[] binary, Guid ownerId)
        {
            this.OwnerId = ownerId;
            this.TrackId = trackId;
            this.Binary = binary;
            this.Timestamp = DateTime.UtcNow;
        }

        public TrackState(TrackState copyFrom)
        {
            this.Timestamp = copyFrom.Timestamp;
            this.TrackId = copyFrom.TrackId;
            this.Binary = copyFrom.Binary;
            this.OwnerId = copyFrom.OwnerId;
        }

        public TrackState Update(TrackUpdateCommand command)
        {
            var state = new TrackState(this);
            if (command.Timestamp > state.Timestamp)
            {
                state.Binary = command.Binary;
                state.Timestamp = command.Timestamp;
            }
            return state;
        }
    }
}
