using System;
using System.Collections.Generic;
using domain.Commands;
using domain.Events;
using domain.Infrastructure;

namespace domain
{
    public partial class Person
    {
        public IEnumerable<Event> Execute(ChangeNameCommand command)
        {
            ValidateVersion(this, command);

            Func<DateTime, DateTime> eighteen = dateOfBirth => new DateTime(dateOfBirth.Year + 18, dateOfBirth.Month, dateOfBirth.Day);

            return DateTime.UtcNow > eighteen(DateOfBirth)
                ? (IEnumerable<Event>) new[] {new PersonNamedEvent(command.When, Version + 1, command.FirstName, command.LastName)}
                : new List<Event>();
        }
    }
}