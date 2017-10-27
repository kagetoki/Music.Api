using Music.API.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Music.API.Entities.States
{
    public class SubscriptionState : State
    {
        public string SubscriptionId { get; private set; }
        public string ReleaseId { get; private set; }
        public DateTime UtcExpiration { get; private set; }
        public decimal Cost { get; private set; }
        public ImmutableList<string> ShopIds { get; private set; }

        public SubscriptionState(string subscriptionId, decimal cost, DateTime utcExpiration, string releaseId, ImmutableList<string> shopIds)
        {
            SubscriptionId = subscriptionId;
            UtcExpiration = utcExpiration;
            ReleaseId = releaseId;
            ShopIds = shopIds;
            Cost = cost;
        }

        public SubscriptionState(SubscriptionReplaceCommand command)
        {
            SubscriptionId = command.SubscriptionId;
            UtcExpiration = command.UtcExpiration;
            ReleaseId = command.ReleaseId;
            ShopIds = command.ShopIds;
            Cost = command.Cost;
            Timestamp = command.Timestamp;
        }

        public SubscriptionState(string subscriptionId, SubscriptionCreateCommand command)
        {
            SubscriptionId = subscriptionId;
            UtcExpiration = command.UtcExpiration;
            ReleaseId = command.ReleaseId;
            ShopIds = command.ShopIds;
            Cost = command.Cost;
            Timestamp = command.Timestamp;
        }
    }
}
