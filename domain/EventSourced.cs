using System;
using System.Collections.Generic;
using domain.Events;

namespace domain
{
    public abstract class EventSourced
    {
        protected IReadOnlyDictionary<Type, Action<Event>> Actions;

        public void Apply(Event @event)
        {
            Actions[@event.GetType()].Invoke(@event);
        }

        public static T LoadFrom<T>(List<Event> events) where T : EventSourced, new()
        {
            var eventSourced = new T();
            foreach (var @event in events) eventSourced.Apply(@event);
            return eventSourced;
        }
    }
}