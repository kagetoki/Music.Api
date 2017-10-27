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
        public ImmutableList<string> ShopIds { get; private set; }

        public SubscriptionState(string subscriptionId, DateTime utcExpiration, string releaseId, ImmutableList<string> shopIds)
        {
            SubscriptionId = subscriptionId;
            UtcExpiration = utcExpiration;
            ReleaseId = releaseId;
            ShopIds = shopIds;
        }


    }
}
