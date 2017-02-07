using System;
using System.Collections.Generic;

namespace domain.Infrastructure
{
    public abstract class VersionedEventSourced : EventSourced
    {
        public ulong Version { get; private set; }

        public void Apply(VersionedEvent @event)
        {
            if (@event.Version != Version + 1)
            {
                throw new InvalidOperationException($"Expected event version {Version + 1} but was {@event.Version}.");
            }

            Version++;
            base.Apply(@event);
        }

        public static T LoadFrom<T>(List<VersionedEvent> events) where T : VersionedEventSourced, new()
        {
            var versionedEventSourced = new T();
            foreach (var @event in events)
            {
                versionedEventSourced.Apply(@event);
            }
            return versionedEventSourced;
        }

        protected static void ValidateVersion(VersionedEventSourced source, VersionedCommand command)
        {
            if (command.Version != source.Version)
            {
                throw new InvalidOperationException($"Expected command version {source.Version} but was {command.Version}.");
            }
        }
    }
}