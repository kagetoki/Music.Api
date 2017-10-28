using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Music.API.Services.Services;
using Music.API.DataAccess;
using Music.API.Entities.Commands;
using Music.API.Services;

namespace Music.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ActorModel.Init();
            var service = new ReleaseService(new ReleaseProvider(), new TrackProvider());
            var command = new ReleaseCreateCommand("test artist", "test", "genre");
            service.CreateRelease(command);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
