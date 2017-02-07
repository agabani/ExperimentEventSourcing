using System;

namespace domain.Events
{
    public class PersonStartedEducationEvent : VersionedEvent
    {
        public PersonStartedEducationEvent(DateTime when, ulong version, string institutionName) : base(when, version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}