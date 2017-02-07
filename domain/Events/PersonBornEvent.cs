using System;
using domain.Infrastructure;

namespace domain.Events
{
    public class PersonBornEvent : VersionedEvent
    {
        public PersonBornEvent(DateTime when, ulong version, Gender gender) : base(when, version)
        {
            Gender = gender;
        }

        public Gender Gender { get; }
    }
}