using System;

namespace domain.Events
{
    public class PersonBornEvent : Event
    {
        public PersonBornEvent(Gender gender, DateTime dateOfBirth) : base(dateOfBirth)
        {
            Gender = gender;
        }

        public Gender Gender { get; }

        public DateTime DateOfBirth => When;
    }
}