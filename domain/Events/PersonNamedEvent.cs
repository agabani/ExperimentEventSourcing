using System;

namespace domain.Events
{
    public class PersonNamedEvent : VersionedEvent
    {
        public PersonNamedEvent(string firstName, string lastName, DateTime when, ulong version) : base(when, version)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}