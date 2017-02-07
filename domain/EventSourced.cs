using System;
using System.Collections.Generic;
using domain.Events;

namespace domain
{
    public class EventSourced
    {
        protected IReadOnlyDictionary<Type, Action<Event>> Actions;

        public void Apply(Event @event)
        {
            Actions[@event.GetType()].Invoke(@event);
        }

        public static Person LoadFrom(List<Event> events)
        {
            var person = new Person();
            foreach (var @event in events)
            {
                person.Apply(@event);
            }
            return person;
        }
    }
}