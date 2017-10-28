using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class SubscriptionReplaceCommand : Command
    {
        public string SubscriptionId { get; set; }
        public string ReleaseId { get; set; }
        public DateTime UtcExpiration { get; set; }
        public decimal Cost { get; set; }
        public ImmutableList<string> ShopIds { get; set; }
        public Guid OwnerId { get; set; }

        public SubscriptionReplaceCommand(string subscriptionId, string releaseId, DateTime utcExpiration, decimal cost, IEnumerable<string> shopIds, Guid ownerId)
        {
            OwnerId = ownerId;
            ReleaseId = releaseId;
            UtcExpiration = utcExpiration;
            Cost = cost;
            ShopIds = shopIds.ToImmutableList();
        }

        public SubscriptionReplaceCommand()
        {

        }
    }
}
