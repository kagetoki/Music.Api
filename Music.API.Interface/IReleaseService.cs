using Music.API.Entites.Models;
using Music.API.Entities.Commands;

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
