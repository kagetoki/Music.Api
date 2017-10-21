using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Commands
{
    public class ReleaseCreateCommand : Command
    {
        public string Artist { get; private set; }
        public byte[] Cover { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public ReleaseCreateCommand(string artist, string title, string genre, byte[] cover = null)
        {
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
        }
    }
}
