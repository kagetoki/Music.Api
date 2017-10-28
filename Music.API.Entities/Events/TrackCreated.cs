﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Events
{
    public class TrackCreated : Event
    {
        public string TrackId { get; set; }
        public byte[] Binary { get; set; }
        public Guid OwnerId { get; set; }
    }
}
