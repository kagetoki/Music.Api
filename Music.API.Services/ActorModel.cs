using Akka.Actor;
using System;

namespace Music.API.Services
{
    internal static class ActorModel
    {
        internal static ActorSystem System = ActorSystem.Create("main");
        internal static ActorPath ReleasePapa = ActorPath.Parse("main/releases");
        internal static ActorPath TrackPapa = ActorPath.Parse("main/tracks");
        internal static ActorPath ReadStorageActor = ActorPath.Parse("main/readStorage");
        static ActorModel()
        {
            //System.ActorOf(Props.Create())
        }
    }
}
