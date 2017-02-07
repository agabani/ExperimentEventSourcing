using System;
using System.Collections.Generic;

namespace domain.Infrastructure
{
    public abstract class EventSourced
    {
        protected IReadOnlyDictionary<Type, Action<Event, object>> Actions { get; set; }

        public void Apply(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public void Apply(Event @event)
        {
            Actions[@event.GetType()].Invoke(@event, this);
        }

        public static T LoadFrom<T>(List<Event> events) where T : EventSourced, new()
        {
            var eventSourced = new T();
            foreach (var @event in events)
            {
                eventSourced.Apply(@event);
            }
            return eventSourced;
        }
    }
}