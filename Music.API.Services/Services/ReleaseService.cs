using Music.API.Interface;
using System;
using System.Collections.Generic;
using Music.API.Entities.Commands;
using Music.API.Entities.States;
using Music.API.DataAccess.Abstractions;
using Music.API.Entities.Events;

namespace Music.API.Services.Services
{
    public class ReleaseService : IReleaseService
    {
        private readonly IReleaseProvider _releaseProvider;
        private readonly ITrackProvider _trackProvider;
        public ReleaseService(IReleaseProvider releaseProvider, ITrackProvider trackProvider)
        {
            _releaseProvider = releaseProvider;
            _trackProvider = trackProvider;
        }
        public void AddMetadata(MetadataCreateCommand createMetadata)
        {
            WithValidation(() => CommandValidator.Validate(createMetadata));

            ActorModel.TellReleaseActor(createMetadata.ReleaseId, createMetadata);
        }

        public string AddTrack(TrackCreateCommand createTrack)
        {
            WithValidation(() => CommandValidator.Validate(createTrack));
            var trackId = Guid.NewGuid().ToString();
            var trackCreated = new TrackCreated
            {
                Binary = createTrack.Binary,
                OwnerId = createTrack.OwnerId,
                Timestamp = createTrack.Timestamp,
                TrackId = trackId,
            };
            ActorModel.Tell(ActorModel.TrackPapaPath, trackCreated);
            return trackId;
        }

        public string CreateRelease(ReleaseCreateCommand createRelease)
        {
            WithValidation(() => CommandValidator.Validate(createRelease));
            var releaseId = Guid.NewGuid().ToString();
            var releaseCreated = new ReleaseCreated
            {
                ReleaseId = releaseId,
                Artist = createRelease.Artist,
                Cover = createRelease.Cover,
                Genre = createRelease.Genre,
                OwnerId = createRelease.OwnerId,
                Timestamp = createRelease.Timestamp,
                Title = createRelease.Title
            };
            ActorModel.Tell(ActorModel.ReleasePapaPath, releaseCreated);
            return releaseId;
        }

        public List<ReleaseState> GetPublishedReleases()
        {
            return InMemoryAppState.GetPublishedReleases();
        }

        public ReleaseState GetRelease(string id)
        {        
            //possibly no need for provider call
            return InMemoryAppState.Get(id) ?? _releaseProvider.Get(id);
        }

        public TrackState GetTrack(string id)
        {
            return _trackProvider.Get(id);
        }

        public void UpdateMetadata(MetadataUpdateCommand updateMetadata)
        {
            WithValidation(() => CommandValidator.Validate(updateMetadata));
            ActorModel.TellReleaseActor(updateMetadata.ReleaseId, updateMetadata);
        }

        public void UpdateRelease(ReleaseUpdateCommand updateRelease)
        {
            WithValidation(() => CommandValidator.Validate(updateRelease));
            ActorModel.TellReleaseActor(updateRelease.ReleaseId, updateRelease);
        }

        public void UpdateTrack(TrackUpdateCommand trackUpdate)
        {
            WithValidation(() => CommandValidator.Validate(trackUpdate));
            ActorModel.TellTrackActor(trackUpdate.TrackId, trackUpdate);
        }

        private void WithValidation(Func<ValidationResult> validation)
        {
            var vr = validation();
            if (!vr.Success)
            {
                throw new Exception(vr.ErrorMessage);
            }
        }
    }
}
