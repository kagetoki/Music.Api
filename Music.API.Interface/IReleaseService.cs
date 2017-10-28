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
        void AddMetadata(MetadataCreateCommand createMetadata, Guid ownerId);
        void UpdateRelease(ReleaseUpdateCommand updateRelease, Guid ownerId);
        void UpdateMetadata(MetadataUpdateCommand updateMetadata, Guid ownerId);
        void UpdateTrack(TrackUpdateCommand trackUpdate);
        ReleaseState GetRelease(string id);
        TrackState GetTrack(string id);
        List<ReleaseState> GetPublishedReleases();
    }
}
