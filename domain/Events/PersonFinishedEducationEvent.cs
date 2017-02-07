using System;

namespace domain.Events
{
    public class PersonFinishedEducationEvent : VersionedEvent
    {
        public PersonFinishedEducationEvent(string institutionName, DateTime when, ulong version) : base(when, version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}