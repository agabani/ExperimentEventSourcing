using System;

namespace domain.Infrastructure
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