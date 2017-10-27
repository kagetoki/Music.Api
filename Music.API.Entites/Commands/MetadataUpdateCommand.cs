using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class MetadataUpdateCommand : Command
    {
        public string ReleaseId { get; private set; }
        public string TrackId { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public int? Number { get; private set; }
        public MetadataUpdateCommand(string trackId, string artist, string album, string title, string genre, string releaseId, int? number)
        {
            TrackId = trackId;
            Artist = artist;
            Album = album;
            Title = title;
            Genre = genre;
            Number = number;
            ReleaseId = releaseId;
        }

        public static bool IsValid(MetadataUpdateCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.TrackId) || string.IsNullOrEmpty(command.ReleaseId))
            {
                return false;
            }
            return !string.IsNullOrEmpty(command.Title)
                    || !string.IsNullOrEmpty(command.Genre)
                    || !string.IsNullOrEmpty(command.Artist)
                    || !string.IsNullOrEmpty(command.Album)
                    || command.Number.HasValue;
        }
    }
}
