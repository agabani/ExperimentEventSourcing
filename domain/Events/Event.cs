using System;

namespace domain.Events
{
    public class Event
    {
        protected Event(DateTime when)
        {
            When = when;
        }

        public DateTime When { get; protected set; }
    }
}