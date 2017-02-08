using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class BirthedCommand : VersionedCommand
    {
        public BirthedCommand(DateTime when, ulong version, Gender gender)
            : base(when, version)
        {
            Gender = gender;
        }

        public Gender Gender { get; private set; }
    }
}