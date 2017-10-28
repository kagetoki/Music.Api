using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entites.Models
{
    public class Subscription
    {
        public string SubscriptionId { get; set; }
        public string ReleaseId { get; set; }
        public DateTime UtcExpirationDate { get; set; }
        public List<string> ShopIds { get; set; }
        public decimal Price { get; set; }
        public Guid OwnerId { get; set; }
    }
}
