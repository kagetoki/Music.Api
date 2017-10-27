using Music.API.Entities.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.DataAccess.Abstractions
{
    public interface ITrackProvider
    {
        void Store(TrackState state);
        TrackState Get(string trackId);
    }
}
