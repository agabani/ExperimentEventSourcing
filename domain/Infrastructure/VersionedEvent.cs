using System;

namespace domain.Infrastructure
{
    public abstract class VersionedEvent : Event
    {
        protected VersionedEvent(Guid eventSourcedId, DateTime when, string type, ulong version)
            : base(eventSourcedId, when, type)
        {
            Version = version;
        }

        public ulong Version { get; private set; }
    }
}