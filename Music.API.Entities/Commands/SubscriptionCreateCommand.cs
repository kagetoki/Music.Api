using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class SubscriptionCreateCommand : Command
    {
        public string ReleaseId { get; private set; }
        public DateTime UtcExpiration { get; private set; }
        public decimal Cost { get; private set; }
        public ImmutableList<string> ShopIds { get; private set; }
        public Guid OwnerId { get; set; }

        public SubscriptionCreateCommand(string releaseId, DateTime utcExpiration, decimal cost, IEnumerable<string> shopIds, Guid ownerId)
        {
            OwnerId = ownerId;
            ReleaseId = releaseId;
            UtcExpiration = utcExpiration;
            Cost = cost;
            ShopIds = shopIds.ToImmutableList();
        }
    }
}
