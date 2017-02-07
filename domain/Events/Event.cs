using System;

namespace domain.Events
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