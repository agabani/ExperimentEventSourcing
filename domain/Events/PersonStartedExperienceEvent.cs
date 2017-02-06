using System;

namespace domain.Events
{
    public class PersonStartedExperienceEvent : Event
    {
        public PersonStartedExperienceEvent(string institutionName, string title, DateTime when) : base(when)
        {
            InstitutionName = institutionName;
            Title = title;
        }

        public string InstitutionName { get; private set; }
        public string Title { get; private set; }
    }
}