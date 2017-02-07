using System;

namespace domain.Events
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