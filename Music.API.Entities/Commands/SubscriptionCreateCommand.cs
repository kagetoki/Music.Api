using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class SubscriptionCreateCommand : Command
    {
        public string ReleaseId { get; private set; }
    }
}
