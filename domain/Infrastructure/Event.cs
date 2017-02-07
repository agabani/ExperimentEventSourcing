using System;

namespace domain.Infrastructure
{
    public abstract class Event
    {
        protected Event(DateTime when, string type)
        {
            When = when;
            Type = type;
        }

        public DateTime When { get; protected set; }

        public string Type { get; private set; }
    }
}