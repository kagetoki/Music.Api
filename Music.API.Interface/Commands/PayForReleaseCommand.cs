using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface.Commands
{
    public class PayForReleaseCommand : Command
    {
        public string ReleaseId { get; private set; }
        public string[] ShopIds { get; private set; }
        public PayForReleaseCommand(string releaseId, params string[] shopIds)
        {
            ReleaseId = releaseId;
            ShopIds = shopIds;
        }
    }
}
