using Music.API.Entities.States;

namespace Music.API.DataAccess.Abstractions
{
    public interface IReleaseProvider
    {
        void Store(ReleaseState state);
        ReleaseState Get(string releaseId);
    }
}
