using System;

namespace domain.Events
{
    public class PersonStartedEductionEvent : Event
    {
        public PersonStartedEductionEvent(string institutionName, DateTime when)
            : base(when)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }

    public class PersonFinishedEducationEvent : Event
    {
        public PersonFinishedEducationEvent(string institutionName, DateTime when) : base(when)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}