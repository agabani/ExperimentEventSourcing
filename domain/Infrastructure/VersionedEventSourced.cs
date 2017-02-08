using System;
using System.Collections.Generic;
using System.Linq;

namespace domain.Infrastructure
{
    public abstract class VersionedEventSourced : EventSourced
    {
        protected VersionedEventSourced(Guid id, IReadOnlyDictionary<Type, Action<Event, object>> actions)
            : base(id, actions)
        {
        }

        public ulong Version { get; private set; }

        public void Apply(IEnumerable<VersionedEvent> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public void Apply(VersionedEvent @event)
        {
            if (@event.Version != Version)
            {
                throw new InvalidOperationException($"Expected event version {Version} but was {@event.Version}.");
            }

            Version++;
            base.Apply(@event);
        }

        public static T LoadFrom<T>(List<VersionedEvent> events) where T : VersionedEventSourced
        {
            var versionedEventSourced = (T) Activator.CreateInstance(typeof(T), events.First().EventSourcedId);
            foreach (var @event in events)
            {
                versionedEventSourced.Apply(@event);
            }
            return versionedEventSourced;
        }

        protected static void ValidateVersionFor(VersionedEventSourced source, VersionedCommand command)
        {
            EventSourced.ValidateVersionFor(source, command);

            if (command.Version != source.Version)
            {
                throw new InvalidOperationException($"Expected command version {source.Version} but was {command.Version}.");
            }
        }
    }
}