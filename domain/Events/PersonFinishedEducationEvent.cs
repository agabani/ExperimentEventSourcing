using System;

namespace domain.Events
{
    public class PersonFinishedEducationEvent : VersionedEvent
    {
        public PersonFinishedEducationEvent(DateTime when, ulong version, string institutionName) : base(when, version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}