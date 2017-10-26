using Music.API.Entities.States;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities
{
    public static class InMemoryAppState
    {
        internal static ConcurrentDictionary<string, ReleaseState> Releases = new ConcurrentDictionary<string, ReleaseState>();

        public static ReleaseState Get(string releaseId)
        {
            ReleaseState result;
            Releases.TryGetValue(releaseId, out result);
            return result;
        }

        internal static void AddOrUpdate(ReleaseState release)
        {
            if (Releases.ContainsKey(release.ReleaseId))
            {
                Releases[release.ReleaseId] = release;
            }
            else
            {
                Releases.TryAdd(release.ReleaseId, release);
            }
        }
    }
}
