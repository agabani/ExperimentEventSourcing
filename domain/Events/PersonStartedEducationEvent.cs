using System;
using domain.Infrastructure;

namespace domain.Events
{
    public class PersonStartedEducationEvent : VersionedEvent
    {
        public PersonStartedEducationEvent(Guid eventSourcedId, DateTime when, ulong version, string institutionName)
            : base(eventSourcedId, when, "person started education v1", version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}