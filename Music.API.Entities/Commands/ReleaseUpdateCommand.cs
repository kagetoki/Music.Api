using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class ReleaseUpdateCommand : Command
    {
        public string ReleaseId { get; private set; }
        public string Artist { get; private set; }
        public byte[] Cover { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public ReleaseUpdateCommand(string releaseId, string artist, string title, string genre, byte[] cover = null)
        {
            ReleaseId = releaseId;
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
        }
    }
}
