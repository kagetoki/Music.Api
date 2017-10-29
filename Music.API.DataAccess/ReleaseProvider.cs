using Music.API.DataAccess.Abstractions;
using System;
using Music.API.Entities.States;
using Google.Cloud.Datastore.V1;
using System.Collections.Immutable;
using System.Linq;

namespace Music.API.DataAccess
{
    public class ReleaseProvider : IReleaseProvider
    {
        private const string PROJECT_ID = "musicapistorage";//better move to config
        private DatastoreDb Database;
        private KeyFactory ReleaseKeyFactory;
        private KeyFactory MetadataKeyFactory;
        private KeyFactory SubscriptionKeyFactory;
        public ReleaseProvider()
        {
            this.Database = DatastoreDb.Create(PROJECT_ID);
            ReleaseKeyFactory = this.Database.CreateKeyFactory(nameof(ReleaseState));
            MetadataKeyFactory = this.Database.CreateKeyFactory(nameof(MetadataState));
            SubscriptionKeyFactory = this.Database.CreateKeyFactory(nameof(SubscriptionState));
        }

        public ReleaseState Get(string releaseId)
        {
            var key = ReleaseKeyFactory.CreateKey(releaseId);
            var entity = Database.Lookup(key);
            if(entity == null)
            {
                return null;
            }

            return new ReleaseState(releaseId,
                                    entity.Properties[nameof(ReleaseState.Artist)].StringValue,
                                    entity.Properties[nameof(ReleaseState.Title)].StringValue,
                                    entity.Properties[nameof(ReleaseState.Genre)].StringValue,
                                    Guid.Parse(entity.Properties[nameof(ReleaseState.OwnerId)].StringValue),
                                    entity.Properties[nameof(ReleaseState.Cover)].BlobValue.ToByteArray(),
                                    ((Entity[])entity.Properties[nameof(ReleaseState.TrackList)].ArrayValue).Select(e => ToMetadata(e)),
                                    ToSubscription(entity.Properties[nameof(ReleaseState.Subscription)].EntityValue),
                                    entity.Properties[nameof(ReleaseState.Timestamp)].TimestampValue.ToDateTime()
                                    );
        }

        public void Store(ReleaseState state)
        {
            var entity = new Entity
            {
                Key = ReleaseKeyFactory.CreateKey(state.ReleaseId),
                [nameof(ReleaseState.Artist)] = state.Artist,
                [nameof(ReleaseState.Title)] = state.Title,
                [nameof(ReleaseState.Genre)] = state.Genre,
                [nameof(ReleaseState.OwnerId)] = state.OwnerId.ToString(),
                [nameof(ReleaseState.Cover)] = state.Cover,
                [nameof(ReleaseState.TrackList)] = state.TrackList.Select(t => ToEntity(t.Value)).ToArray(),
                [nameof(ReleaseState.Subscription)] = ToEntity(state.Subscription),
                [nameof(ReleaseState.Timestamp)] = state.Timestamp,
            };
            Database.Upsert(entity);
        }

        private Entity ToEntity(MetadataState metadata)
        {
            return new Entity
            {
                Key = MetadataKeyFactory.CreateKey(metadata.TrackId),
                [nameof(MetadataState.Artist)] = metadata.Artist,
                [nameof(MetadataState.Album)] = metadata.Album,
                [nameof(MetadataState.Title)] = metadata.Title,
                [nameof(MetadataState.Timestamp)] = metadata.Timestamp,
                [nameof(MetadataState.TrackId)] = metadata.TrackId,
                [nameof(MetadataState.ReleaseId)] = metadata.ReleaseId,
                [nameof(MetadataState.Number)] = metadata.Number
            };
        }

        private Entity ToEntity(SubscriptionState subscription)
        {
            return new Entity
            {
                Key = SubscriptionKeyFactory.CreateKey(subscription.SubscriptionId),
                [nameof(SubscriptionState.ReleaseId)] = subscription.ReleaseId,
                [nameof(SubscriptionState.SubscriptionId)] = subscription.SubscriptionId,
                [nameof(SubscriptionState.UtcExpiration)] = subscription.UtcExpiration,
                [nameof(SubscriptionState.Timestamp)] = subscription.Timestamp,
                [nameof(SubscriptionState.ShopIds)] = subscription.ShopIds.ToArray(),
            };
        }

        private MetadataState ToMetadata(Entity entity)
        {
            return new MetadataState(entity.Properties[nameof(MetadataState.TrackId)].StringValue,
                                     entity.Properties[nameof(MetadataState.ReleaseId)].StringValue,
                                     entity.Properties[nameof(MetadataState.Title)].StringValue,
                                     entity.Properties[nameof(MetadataState.Artist)].StringValue,
                                     entity.Properties[nameof(MetadataState.Album)].StringValue,
                                     entity.Properties[nameof(MetadataState.Genre)].StringValue,
                                     (int)entity.Properties[nameof(MetadataState.Number)].IntegerValue
                );
        }

        private SubscriptionState ToSubscription(Entity entity)
        {
            return new SubscriptionState(entity.Properties[nameof(SubscriptionState.SubscriptionId)].StringValue,
                                         (decimal)entity.Properties[nameof(SubscriptionState.Cost)].DoubleValue,
                                         entity.Properties[nameof(SubscriptionState.UtcExpiration)].TimestampValue.ToDateTime(),
                                         entity.Properties[nameof(SubscriptionState.ReleaseId)].StringValue,
                                         ((string[])entity.Properties[nameof(SubscriptionState.ShopIds)].ArrayValue).ToImmutableList()
                );
        }
    }
}
