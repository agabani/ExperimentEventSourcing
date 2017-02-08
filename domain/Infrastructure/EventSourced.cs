using System;
using System.Collections.Generic;
using System.Linq;

namespace domain.Infrastructure
{
    public abstract class EventSourced
    {

        private readonly IReadOnlyDictionary<Type, Action<Event, object>> _actions;
        protected EventSourced(Guid id, IReadOnlyDictionary<Type, Action<Event, object>> actions)
        {
            Id = id;
            _actions = actions;
        }

        public Guid Id { get; }

        public void Apply(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public void Apply(Event @event)
        {
            if (Id != @event.EventSourcedId)
            {
                throw new InvalidOperationException($"Expected {nameof(@event.EventSourcedId)} to be {Id} but was {@event.EventSourcedId}.");
            }

            _actions[@event.GetType()].Invoke(@event, this);
        }

        public static T LoadFrom<T>(List<Event> events) where T : EventSourced
        {
            var eventSourced = (T) Activator.CreateInstance(typeof(T), events.First().EventSourcedId);
            foreach (var @event in events)
            {
                eventSourced.Apply(@event);
            }
            return eventSourced;
        }
    }
}