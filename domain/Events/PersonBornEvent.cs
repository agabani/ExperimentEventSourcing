using System;

namespace domain.Events
{
    public class PersonBornEvent : VersionedEvent
    {
        public PersonBornEvent(Gender gender, DateTime dateOfBirth, ulong version) : base(dateOfBirth, version)
        {
            Gender = gender;
        }

        public Gender Gender { get; }

        public DateTime DateOfBirth => When;
    }
}