using System;
using domain.Infrastructure;

namespace domain.Events
{
    public class PersonFinishedExperienceEvent : VersionedEvent
    {
        public PersonFinishedExperienceEvent(DateTime when, ulong version, string institutionName, string title)
            : base(when, "person finished experience v1", version)
        {
            InstitutionName = institutionName;
            Title = title;
        }

        public string InstitutionName { get; private set; }
        public string Title { get; private set; }
    }
}