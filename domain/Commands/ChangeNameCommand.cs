using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class ChangeNameCommand : Command
    {
        public ChangeNameCommand(string firstName, string lastName, DateTime when)
            : base(when)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}