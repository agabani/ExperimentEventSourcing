using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class ChangeNameCommand : VersionedCommand
    {
        public ChangeNameCommand(Guid eventSourcedId, DateTime when, ulong version, string firstName, string lastName)
            : base(eventSourcedId, when, version)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}