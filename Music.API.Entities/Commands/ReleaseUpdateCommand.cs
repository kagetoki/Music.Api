using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class ReleaseUpdateCommand : Command
    {
        public string ReleaseId { get; set; }
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public ReleaseUpdateCommand(string releaseId, string artist, string title, string genre, byte[] cover = null)
        {
            ReleaseId = releaseId;
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
        }
        public ReleaseUpdateCommand()
        {

        }
    }
}
