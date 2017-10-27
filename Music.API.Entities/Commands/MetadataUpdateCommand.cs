using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class MetadataUpdateCommand : Command
    {
        public string ReleaseId { get; set; }
        public string TrackId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int? Number { get; set; }

        public MetadataUpdateCommand()
        {

        }
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
