using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Commands
{
    public class MetadataUpdateCommand : Command
    {
        public string TrackId { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public int? Number { get; private set; }
        public MetadataUpdateCommand(string trackId, string artist, string album, string title, string genre, int? number)
        {
            TrackId = trackId;
            Artist = artist;
            Album = album;
            Title = title;
            Genre = genre;
            Number = number;
        }
    }
}
