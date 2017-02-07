using System;

namespace domain.Infrastructure
{
    public abstract class VersionedEvent : Event
    {
        protected VersionedEvent(DateTime when, string type, ulong version)
            : base(when, type)
        {
            Version = version;
        }

        public ulong Version { get; protected set; }
    }
}