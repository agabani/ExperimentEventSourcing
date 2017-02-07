using System;

namespace domain.Infrastructure
{
    public abstract class Event
    {
        protected Event(DateTime when)
        {
            When = when;
        }

        public DateTime When { get; protected set; }
    }
}