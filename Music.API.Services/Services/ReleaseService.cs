using Music.API.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Music.API.Entities.Commands;
using Music.API.Entities.States;

namespace Music.API.Services.Services
{
    public class ReleaseService : IReleaseService
    {
        public MetadataState AddMetadata(MetadataCreateCommand createMetadata)
        {
            throw new NotImplementedException();
        }

        public string AddTrack(TrackCreateCommand createTrack)
        {
            throw new NotImplementedException();
        }

        public ReleaseState CreateRelease(ReleaseCreateCommand createRelease)
        {
            throw new NotImplementedException();
        }

        public MetadataState UpdateMetadata(MetadataUpdateCommand updateMetadata)
        {
            throw new NotImplementedException();
        }

        public ReleaseState UpdateRelease(ReleaseUpdateCommand updateRelease)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrack(TrackUpdateCommand trackUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
