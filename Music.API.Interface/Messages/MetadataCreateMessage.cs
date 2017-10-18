using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class MetadataCreateMessage : Message
    {
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public MetadataCreateMessage(string trackId, string artist, string album, string title, string genre)
        {
            Artist = artist;
            Album = album;
            Title = title;
            Genre = genre;
        }
    }
}
