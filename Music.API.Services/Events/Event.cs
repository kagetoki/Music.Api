using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Events
{
    public abstract class Event
    {
        public DateTime Timestamp { get; set; }
        public abstract EventType Type { get; }
    }

    public class CreatedEvent : Event
    {
        public override EventType Type => EventType.Created;
    }

    public class UpdatedEvent : Event
    {
        public override EventType Type => EventType.Updated;
    }

    public enum EventType
    {
        Created,
        Updated,
        Deleted
    }
}
