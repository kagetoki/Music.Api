using Music.API.Interface.Commands;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Music.API.Services.States
{
    public class ReleaseState : State
    {
        public string ReleaseId { get; private set; }
        public string Artist { get; private set; }
        public byte[] Cover { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public ImmutableDictionary<string, MetadataState> TrackList { get; private set; }
        
        public ReleaseState(string releaseId, string artist, string title, string genre, byte[] cover = null)
        {
            ReleaseId = releaseId;
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
            Timestamp = DateTime.UtcNow;
        }

        public ReleaseState(ReleaseState copyFrom)
        {
            this.ReleaseId = copyFrom.ReleaseId;
            this.Artist = copyFrom.Artist;
            this.Cover = copyFrom.Cover;
            this.Genre = copyFrom.Genre;
            this.Title = copyFrom.Title;
            this.Timestamp = copyFrom.Timestamp;
            this.TrackList = copyFrom.TrackList;
        }

        public ReleaseState Update(params ReleaseUpdateCommand[] commands)
        {
            var state = new ReleaseState(this);
            var stream = commands.OrderBy(m => m.Timestamp);
            foreach (var cmd in stream)
            {
                if(cmd.Timestamp <= state.Timestamp)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(cmd.Title))
                {
                    state.Title = cmd.Title;
                }
                if (!string.IsNullOrEmpty(cmd.Artist))
                {
                    state.Artist = cmd.Artist;
                }
                if (!string.IsNullOrEmpty(cmd.Genre))
                {
                    state.Genre = cmd.Genre;
                }
                if (cmd.Cover != null)
                {
                    state.Cover = cmd.Cover;
                }
                state.Timestamp = cmd.Timestamp;
            }
            return state;
        }

        public ReleaseState AddMetadata(MetadataCreateCommand command)
        {
            if (this.TrackList.ContainsKey(command.TrackId) || command.Timestamp <= this.Timestamp)
            {
                return this;
            }
            var state = new ReleaseState(this);
            state.TrackList = this.TrackList.Add(command.TrackId, new MetadataState(command.TrackId, 
                                                                                    command.ReleaseId, 
                                                                                    command.Title, 
                                                                                    command.Artist, 
                                                                                    command.Album, 
                                                                                    command.Genre, 
                                                                                    command.Number));
            state.Timestamp = command.Timestamp;
            return state;
        }
    }
}
