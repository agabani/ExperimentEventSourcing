using System;

namespace domain.Infrastructure
{
    public abstract class VersionedEvent : Event
    {
        protected VersionedEvent(DateTime when, ulong version) : base(when)
        {
            Version = version;
        }

        public ulong Version { get; protected set; }
    }
}