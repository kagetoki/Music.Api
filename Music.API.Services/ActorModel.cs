using Akka.Actor;
using Music.API.DataAccess;
using Music.API.Services.Actors;
using System;
using System.Collections.Immutable;

namespace Music.API.Services
{
    internal static class ActorModel
    {
        internal static ActorSystem System = ActorSystem.Create("main");
        internal static ActorPath ReleasePapaPath = ActorPath.Parse("main/releases");
        internal static ActorPath TrackPapaPath = ActorPath.Parse("main/tracks");
        internal static ActorPath ReadStorageActorPath = ActorPath.Parse("main/readStorage");
        static ActorModel()
        {
            System.ActorOf(Props.Create(() => new ReleaseCreatorActor(ReadStorageActorPath, ImmutableHashSet<string>.Empty)), "releases");
            System.ActorOf(Props.Create(() => new TrackCreatorActor(ReadStorageActorPath, ReleasePapaPath, ImmutableHashSet<string>.Empty)), "tracks");
            System.ActorOf(Props.Create(() => new ReadStorageActor(new ReleaseProvider(), new TrackProvider())), "readStorage");
        }

        public static void Tell(ActorPath path, object message)
        {
            var selection = System.ActorSelection(path);
            selection.Tell(message);
        }

        public static void Tell(string path, object message)
        {
            var selection = System.ActorSelection(path);
            selection.Tell(message);
        }

        public static void TellReleaseActor(string releaseId, object message)
        {
            var selection = SelectReleaseActor(releaseId);
            selection.Tell(message);
        }

        public static void TellTrackActor(string trackId, object message)
        {
            var selection = SelectTrackActor(trackId);
            selection.Tell(message);
        }

        public static ActorSelection SelectReleaseActor(string releaseId)
        {
            return System.ActorSelection($"{ReleasePapaPath.ToString()}/{releaseId}");
        }

        public static ActorSelection SelectTrackActor(string trackId)
        {
            return System.ActorSelection($"{TrackPapaPath.ToString()}/{trackId}");
        }
    }
}
