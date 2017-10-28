using Akka.Actor;
using Music.API.DataAccess;
using Music.API.Services.Actors;
using System;
using System.Collections.Immutable;

namespace Music.API.Services
{
    public static class ActorModel
    {
        internal static ActorSystem System = ActorSystem.Create("main");
        internal static ActorPath ReleasePapaPath = ActorPath.Parse("akka://main/user/releases");
        internal static ActorPath TrackPapaPath = ActorPath.Parse("akka://main/user/tracks");
        internal static ActorPath ReadStorageActorPath = ActorPath.Parse("akka://main/user/readStorage");
        
        public static void Init()
        {
            System.ActorOf(Props.Create(() => new ReleaseCreatorActor(ReadStorageActorPath, ImmutableHashSet<string>.Empty)), "releases");
            System.ActorOf(Props.Create(() => new TrackCreatorActor(ReadStorageActorPath, ReleasePapaPath, ImmutableHashSet<string>.Empty)), "tracks");
            System.ActorOf(Props.Create(() => new ReadStorageActor(new ReleaseProvider(), new TrackProvider())), "readStorage");
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
    }
}
