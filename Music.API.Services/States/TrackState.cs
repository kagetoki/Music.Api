using Music.API.Interface.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music.API.Services.States
{
    public class TrackState : State
    {
        public string TrackId { get; private set; }
        public byte[] Binary { get; private set; }
        public TrackState(string trackId, byte[] binary)
        {
            this.TrackId = trackId;
            this.Binary = binary;
            this.Timestamp = DateTime.UtcNow;
        }

        public TrackState(TrackState copyFrom)
        {
            this.Timestamp = copyFrom.Timestamp;
            this.TrackId = copyFrom.TrackId;
            this.Binary = copyFrom.Binary;
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
