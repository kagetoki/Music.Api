using Music.API.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Music.API.Entities.States;

namespace Music.API.DataAccess
{
    public class TrackProvider : ITrackProvider
    {
        public TrackState Get(string trackId)
        {
            return new TrackState(trackId, new byte[1024 * 1024 * 8], Guid.NewGuid());
        }

        public void Store(TrackState state)
        {
            
        }
    }
}
