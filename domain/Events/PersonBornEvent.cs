using System;
using domain.Infrastructure;

namespace domain.Events
{
    public class PersonBornEvent : VersionedEvent
    {
        public PersonBornEvent(DateTime dateOfBirth, ulong version, Gender gender) : base(dateOfBirth, version)
        {
            Gender = gender;
        }

        public Gender Gender { get; }

        public DateTime DateOfBirth => When;
    }
}