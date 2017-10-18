using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Messages
{
    public class ReleaseUpdateMessage : Message
    {
        public string ReleaseId { get; private set; }
        public string Artist { get; private set; }
        public byte[] Cover { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public ReleaseUpdateMessage(string releaseId, string artist, string title, string genre, byte[] cover = null)
        {
            ReleaseId = releaseId;
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
        }
    }
}
