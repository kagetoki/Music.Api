using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.API.Interface;
using Music.API.Entities.Commands;
using Music.API.Entities.States;

namespace Music.Api.Controllers
{
    [Produces("application/json")]
    public class ReleaseController : Controller
    {
        private readonly IReleaseService _releaseService;

        public ReleaseController(IReleaseService releaseService)
        {
            _releaseService = releaseService;
        }

        [HttpPost, Route("releases")]
        public string CreateRelease([FromBody] ReleaseCreateCommand command)
        {
            return _releaseService.CreateRelease(command);
        }

        [HttpPut, Route("releases")]
        public void UpdateRelease([FromBody] ReleaseUpdateCommand command)
        {
            _releaseService.UpdateRelease(command);
        }

        [HttpPost, Route("releases/metadata")]
        public void AddMetadata([FromBody] MetadataCreateCommand command)
        {
            _releaseService.AddMetadata(command);
        }

        [HttpPut, Route("releases/metadata")]
        public void UpdateMetadata([FromBody] MetadataUpdateCommand command)
        {
            _releaseService.UpdateMetadata(command);
        }

        [HttpGet, Route("releases/{id}")]
        public ReleaseState Get(string id)
        {
            return _releaseService.GetRelease(id);
        }

        [HttpGet, Route("releases")]
        public List<ReleaseState> Get()
        {
            return _releaseService.GetPublishedReleases();
        }

        [HttpGet, Route("tracks/{id}")]
        public TrackState GetTrack(string id)
        {
            return _releaseService.GetTrack(id);
        }

        [HttpPost, Route("tracks")]
        public string UploadTrack([FromBody] TrackCreateCommand track)
        {
            return _releaseService.AddTrack(track);
        }

        [HttpPut, Route("tracks")]
        public void UpdateTrack([FromBody] TrackUpdateCommand track)
        {
            _releaseService.UpdateTrack(track);
        }
    }
}