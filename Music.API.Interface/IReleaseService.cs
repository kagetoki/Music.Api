using Music.API.Interface.Commands;
using Music.API.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface
{
    public interface IReleaseService
    {
        Release CreateRelease(ReleaseCreateCommand createRelease);
        string AddTrack(TrackCreateCommand createTrack);
        Metadata AddMetadata(MetadataCreateCommand createMetadata);
        Release UpdateRelease(ReleaseUpdateCommand updateRelease);
        Metadata UpdateMetadata(MetadataUpdateCommand updateMetadata);
        void UpdateTrack(TrackUpdateCommand trackUpdate);
    }
}
