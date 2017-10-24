﻿namespace Music.API.Services.Events
{
    public class ReleaseCreated : Event
    {
        public string ReleaseId { get; set; }
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
    }
}
