using System;

namespace domain.Infrastructure
{
    public abstract class Event
    {
        protected Event(Guid eventSourcedId, DateTime when, string type)
        {
            EventSourcedId = eventSourcedId;
            When = when;
            Type = type;
        }

        public Guid EventSourcedId { get; private set; }
        public DateTime When { get; private set; }
        public string Type { get; private set; }
    }
}