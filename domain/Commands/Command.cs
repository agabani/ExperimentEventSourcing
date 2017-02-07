using System;

namespace domain.Commands
{
    public abstract class Command
    {
        protected Command(DateTime when)
        {
            When = when;
        }

        public DateTime When { get; private set; }
    }
}