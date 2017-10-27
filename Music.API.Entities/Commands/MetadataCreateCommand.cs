using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class MetadataCreateCommand : Command
    {
        public string ReleaseId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string TrackId { get; set; }
        public int? Number { get; set; }

        public MetadataCreateCommand()
        {

        }
        public MetadataCreateCommand(string trackId, string artist, string album, string title, string genre, string releaseId, int number)
        {
            Artist = artist;
            Album = album;
            Title = title;
            Genre = genre;
            TrackId = trackId;
            ReleaseId = releaseId;
            Number = number;
        }

        public static bool IsValid(MetadataCreateCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.TrackId) || string.IsNullOrEmpty(command.ReleaseId))
            {
                return false;
            }
            return !string.IsNullOrEmpty(command.Title)
                    || !string.IsNullOrEmpty(command.Genre)
                    || !string.IsNullOrEmpty(command.Artist)
                    || command.Number.HasValue
                    || !string.IsNullOrEmpty(command.Album);
        }
    }
}
