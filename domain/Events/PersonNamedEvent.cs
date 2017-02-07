using System;

namespace domain.Events
{
    public class PersonNamedEvent : VersionedEvent
    {
        public PersonNamedEvent(DateTime when, ulong version, string firstName, string lastName) : base(when, version)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}