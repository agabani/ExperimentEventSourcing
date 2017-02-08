using System;
using domain.Infrastructure;

namespace domain.Events
{
    public class PersonFinishedEducationEvent : VersionedEvent
    {
        public PersonFinishedEducationEvent(Guid eventSourcedId, DateTime when, ulong version, string institutionName)
            : base(eventSourcedId, when, "person finished education v1", version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}