using System;

namespace domain.Infrastructure
{
    public class VersionedCommand : Command
    {
        public VersionedCommand(DateTime when, ulong version) : base(when)
        {
            Version = version;
        }

        public ulong Version { get; private set; }
    }
}