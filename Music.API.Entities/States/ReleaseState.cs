using Music.API.Entities.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Music.API.Entities.States
{
    public class ReleaseState : State
    {
        public Guid OwnerId { get; private set; }
        public string ReleaseId { get; private set; }
        public string Artist { get; private set; }
        public byte[] Cover { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public ImmutableDictionary<string, MetadataState> TrackList { get; private set; }
        public SubscriptionState Subscription { get; private set; }
        public bool IsPublished => this.Subscription != null && this.Subscription.UtcExpiration > DateTime.UtcNow;

        
        public ReleaseState(string releaseId, string artist, string title, string genre, Guid ownerId, byte[] cover = null)
        {
            ReleaseId = releaseId;
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
            OwnerId = ownerId;
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

        public ReleaseState(string releaseId,
                            string artist, 
                            string title, 
                            string genre, 
                            Guid ownerId, 
                            byte[] cover, 
                            IEnumerable<MetadataState> trackList, 
                            SubscriptionState subscription, 
                            DateTime timestamp)
        {
            ReleaseId = releaseId;
            Artist = artist;
            Title = title;
            Genre = genre;
            Cover = cover;
            OwnerId = ownerId;
            Timestamp = timestamp;
            TrackList = trackList.ToImmutableDictionary(m => m.TrackId);
            Subscription = subscription;
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

        public ReleaseState UpdateMetadata(MetadataUpdateCommand command)
        {
            if (!this.TrackList.ContainsKey(command.TrackId) || command.Timestamp <= this.Timestamp)
            {
                return this;
            }
            var state = new ReleaseState(this);
            state.Timestamp = command.Timestamp;
            var metadata = TrackList[command.TrackId];
            state.TrackList = state.TrackList.SetItem(command.TrackId, metadata.Update(command));
            return state;
        }

        public ReleaseState AddSubscription(string subscriptionId, SubscriptionCreateCommand command)
        {
            var state = new ReleaseState(this);
            state.Subscription = new SubscriptionState(subscriptionId, command);
            return state;
        }

        public ReleaseState ReplaceSubscription(SubscriptionReplaceCommand command)
        {
            var state = new ReleaseState(this);
            state.Subscription = new SubscriptionState(command);
            return state;
        }
    }
}
