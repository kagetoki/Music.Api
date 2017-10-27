using Music.API.Entites.Models;
using Music.API.Entities.Commands;
using Music.API.Entities.States;

namespace Music.API.Interface
{
    public interface IReleaseService
    {
        ReleaseState CreateRelease(ReleaseCreateCommand createRelease);
        string AddTrack(TrackCreateCommand createTrack);
        MetadataState AddMetadata(MetadataCreateCommand createMetadata);
        ReleaseState UpdateRelease(ReleaseUpdateCommand updateRelease);
        MetadataState UpdateMetadata(MetadataUpdateCommand updateMetadata);
        void UpdateTrack(TrackUpdateCommand trackUpdate);
    }
}
