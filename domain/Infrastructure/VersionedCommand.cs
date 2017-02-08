using System;

namespace domain.Infrastructure
{
    public class VersionedCommand : Command
    {
        public VersionedCommand(Guid eventSourcedId, DateTime when, ulong version)
            : base(eventSourcedId, when)
        {
            Version = version;
        }

        public ulong Version { get; private set; }
    }
}