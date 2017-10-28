using Music.API.DataAccess.Abstractions;
using System;
using Music.API.Entities.States;

namespace Music.API.DataAccess
{
    public class ReleaseProvider : IReleaseProvider
    {
        public ReleaseState Get(string releaseId)
        {
            return new ReleaseState(releaseId, "test artist", "Test title", "test genre", Guid.NewGuid());
        }

        public void Store(ReleaseState state)
        {
            
        }
    }
}
