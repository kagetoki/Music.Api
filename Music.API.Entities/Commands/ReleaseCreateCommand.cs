using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class ReleaseCreateCommand : Command
    {
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public Guid OwnerId { get; set; }
        public ReleaseCreateCommand(string artist, string title, string genre, byte[] cover = null)
        {
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
        }
        public ReleaseCreateCommand()
        {

        }
    }
}
