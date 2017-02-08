using System;
using domain.Infrastructure;

namespace domain.Events
{
    public class PersonBornEvent : VersionedEvent
    {
        public PersonBornEvent(Guid eventSourcedId, DateTime when, ulong version, Gender gender)
            : base(eventSourcedId, when, "person born v1", version)
        {
            Gender = gender;
        }

        public Gender Gender { get; }
    }
}