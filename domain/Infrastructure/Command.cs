using System;

namespace domain.Infrastructure
{
    public abstract class Command
    {
        protected Command(Guid eventSourcedId, DateTime when)
        {
            EventSourcedId = eventSourcedId;
            When = when;
        }

        public Guid EventSourcedId { get; private set; }
        public DateTime When { get; private set; }
    }
}