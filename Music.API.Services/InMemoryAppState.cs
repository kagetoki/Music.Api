using Music.API.Entities.States;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music.API.Services
{
    public static class InMemoryAppState
    {
        internal static ConcurrentDictionary<string, ReleaseState> Releases = new ConcurrentDictionary<string, ReleaseState>();
        internal static ConcurrentDictionary<string, ReleaseState> PublishedReleases = new ConcurrentDictionary<string, ReleaseState>();

        public static ReleaseState Get(string releaseId)
        {
            ReleaseState result;
            Releases.TryGetValue(releaseId, out result);
            return result;
        }

        public static List<ReleaseState> GetPublishedReleases()
        {
            return PublishedReleases.Values.Where(r => r.IsPublished).ToList();
        }

        internal static void AddOrUpdate(ReleaseState release)
        {
            AddOrUpdate(Releases, release);
            if(release.Subscription != null && release.IsPublished)
            {
                AddOrUpdate(PublishedReleases, release);
            }
        }

        private static void AddOrUpdate(ConcurrentDictionary<string, ReleaseState> storage, ReleaseState release)
        {
            if (storage.ContainsKey(release.ReleaseId))
            {
                storage[release.ReleaseId] = release;
            }
            else
            {
                storage.TryAdd(release.ReleaseId, release);
            }
        }
    }
}
