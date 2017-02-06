using System;

namespace domain.Events
{
    public class PersonNamedEvent : Event
    {
        public PersonNamedEvent(string firstName, string lastName, DateTime when)
            : base(when)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}