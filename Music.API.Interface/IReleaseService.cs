using Music.API.Entites.Models;
using Music.API.Entities.Commands;
using Music.API.Entities.States;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Music.API.Interface
{
    public interface IReleaseService
    {
        string CreateRelease(ReleaseCreateCommand createRelease);
        string AddTrack(TrackCreateCommand createTrack);
        void AddMetadata(MetadataCreateCommand createMetadata);
        void UpdateRelease(ReleaseUpdateCommand updateRelease);
        void UpdateMetadata(MetadataUpdateCommand updateMetadata);
        void UpdateTrack(TrackUpdateCommand trackUpdate);
        ReleaseState GetRelease(string id);
        TrackState GetTrack(string id);
        List<ReleaseState> GetPublishedReleases();
    }
}
