using System;

namespace domain.Infrastructure
{
    public abstract class Event
    {
        protected Event(Guid eventSourcedId, DateTime when, string type)
        {
            When = when;
            Type = type;
            EventSourcedId = eventSourcedId;
        }

        public DateTime When { get; private set; }

        public string Type { get; private set; }

        public Guid EventSourcedId { get; private set; }
    }
}