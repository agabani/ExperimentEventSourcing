using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class BirthedCommand : VersionedCommand
    {
        public BirthedCommand(Guid eventSourcedId, DateTime when, ulong version, Gender gender)
            : base(eventSourcedId, when, version)
        {
            Gender = gender;
        }

        public Gender Gender { get; private set; }
    }
}