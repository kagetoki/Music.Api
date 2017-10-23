using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Models
{
    public class Subscription
    {
        public string ReleaseId { get; set; }
        public DateTime UtcExpirationDate { get; set; }
        public List<Shop> Shops { get; set; }
        public decimal Price { get; set; }
    }
}
