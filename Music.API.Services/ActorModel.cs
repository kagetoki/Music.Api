using Akka.Actor;
using Music.API.Services.Actors;
using System.Collections.Immutable;
using Music.API.DataAccess.Abstractions;

namespace Music.API.Services
{
    public static class ActorModel
    {
        internal static ActorSystem System;
        internal static ActorPath ReleasePapaPath = ActorPath.Parse("akka://main/user/releases");
        internal static ActorPath TrackPapaPath = ActorPath.Parse("akka://main/user/tracks");
        internal static ActorPath ReadStorageActorPath = ActorPath.Parse("akka://main/user/readStorage");
        
        public static void Init(IReleaseProvider releaseProvider, ITrackProvider trackProvider)
        {
            
            var config = Akka.Configuration.ConfigurationFactory.ParseString(AKKA_GCP_CONFIG);
            System = ActorSystem.Create("main", config);
            System.ActorOf(Props.Create(() => new ReleaseCreatorActor(ReadStorageActorPath, ImmutableHashSet<string>.Empty)), "releases");
            System.ActorOf(Props.Create(() => new TrackCreatorActor(ReadStorageActorPath, ReleasePapaPath, ImmutableHashSet<string>.Empty)), "tracks");
            System.ActorOf(Props.Create(() => new ReadStorageActor(releaseProvider, trackProvider)), "readStorage");
        }

        internal static void Tell(ActorPath path, object message)
        {
            var selection = System.ActorSelection(path);
            selection.Tell(message);
        }

        internal static void Tell(string path, object message)
        {
            var selection = System.ActorSelection(path);
            selection.Tell(message);
        }

        internal static void TellReleaseActor(string releaseId, object message)
        {
            var selection = SelectReleaseActor(releaseId);
            selection.Tell(message);
        }

        internal static void TellTrackActor(string trackId, object message)
        {
            var selection = SelectTrackActor(trackId);
            selection.Tell(message);
        }

        internal static ActorSelection SelectReleaseActor(string releaseId)
        {
            return System.ActorSelection($"{ReleasePapaPath.ToString()}/{releaseId}");
        }

        internal static ActorSelection SelectTrackActor(string trackId)
        {
            return System.ActorSelection($"{TrackPapaPath.ToString()}/{trackId}");
        }

        private const string AKKA_GCP_CONFIG = @"akka.persistence.journal.datastore-journal {
  # Type name of the cassandra journal plugin
  class = ""akka.persistence.gcp.datastore.journal.DatastoreJournal, akka.persistence.gcp.datastore""
  project-id = ""musicapistore""
  namespace-id = ""default""
  # use managed(cloud) or datastore emulator
  use-managed = ""on""
  journalentity-kind = ""Journal""
  gcp-cold-storage = ""off""
  }

    akka.persistence.snapshot-store.datastore-snapshot-store {
  # Type name of the cassandra journal plugin
  class = ""akka.persistence.gcp.datastore.snapshot.DatastoreSnapshotStore, akka.persistence.gcp.datastore""
  project-id = ""musicapistore""
  namespace-id = ""default""
  # use managed(cloud) or datastore emulator
  use-managed = ""on""
  snapshotentity-kind = ""Snapshot""
  }";
    }
}
