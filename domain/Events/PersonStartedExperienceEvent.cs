using System;
using domain.Infrastructure;

namespace domain.Events
{
    public class PersonStartedExperienceEvent : VersionedEvent
    {
        public PersonStartedExperienceEvent(Guid eventSourcedId, DateTime when, ulong version, string institutionName, string title)
            : base(eventSourcedId, when, "person started experience v1", version)
        {
            InstitutionName = institutionName;
            Title = title;
        }

        public string InstitutionName { get; private set; }
        public string Title { get; private set; }
    }
}