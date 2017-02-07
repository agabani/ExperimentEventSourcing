using System;

namespace domain.Events
{
    public class PersonStartedExperienceEvent : VersionedEvent
    {
        public PersonStartedExperienceEvent(string institutionName, string title, DateTime when, ulong version) : base(when, version)
        {
            InstitutionName = institutionName;
            Title = title;
        }

        public string InstitutionName { get; private set; }
        public string Title { get; private set; }
    }
}