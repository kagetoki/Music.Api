using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Commands
{
    public class PayForReleaseCommand : Command
    {
        public string ReleaseId { get; set; }
        public string[] ShopIds { get; set; }
        public PayForReleaseCommand(string releaseId, params string[] shopIds)
        {
            ReleaseId = releaseId;
            ShopIds = shopIds;
        }
        public PayForReleaseCommand()
        {

        }
    }
}
