using Music.API.Interface.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.States
{
    public class MetadataState : State
    {
        public string TrackId { get; private set; }
        public string ReleaseId { get; private set; }
        public string Artist { get; private set; }
        public string Title { get; private set; }
        public string Album { get; private set; }
        public string Genre { get; private set; }
        public int Number { get; private set; }

        public MetadataState(string trackId, string releaseId, string title, string artist, string album, string genre, int number)
        {
            ReleaseId = releaseId;
            TrackId = trackId;
            Title = title;
            Artist = artist;
            Album = album;
            Genre = genre;
            Number = number;
        }

        public MetadataState(MetadataState copyFrom)
        {
            ReleaseId = copyFrom.ReleaseId;
            TrackId = copyFrom.TrackId;
            Title = copyFrom.Title;
            Artist = copyFrom.Artist;
            Album = copyFrom.Album;
            Genre = copyFrom.Genre;
            Number = copyFrom.Number;
        }

        public MetadataState Update(MetadataUpdateCommand cmd)
        {
            var state = new MetadataState(this);
            if(cmd.Timestamp > state.Timestamp)
            {
                state.TrackId = cmd.TrackId ?? state.TrackId;
                state.Title = cmd.Title ?? state.Title;
                state.Artist = cmd.Artist ?? state.Artist;
                state.Album = cmd.Album ?? state.Album;
                state.Genre = cmd.Genre ?? state.Genre;
                state.Number = cmd.Number ?? state.Number;
                state.Timestamp = cmd.Timestamp;
            }
            return state;
        }
    }
}
