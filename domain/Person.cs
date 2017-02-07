using System;
using System.Collections.Generic;
using System.Linq;
using domain.Events;

namespace domain
{
    public partial class Person
    {
        public Gender Gender { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<Education> EducationalHistory { get; } = new List<Education>();
        public List<Experience> ExperienceHistory { get; } = new List<Experience>();
    }

    public partial class Person
    {
        public Event ChangeName(string firstName, string lastName, DateTime when)
        {
            return new PersonNamedEvent(firstName, lastName, when);
        }
    }

    public partial class Person : EventSourced
    {
        public Person()
        {
            Actions = new Dictionary<Type, Action<Event>>
            {
                {typeof(PersonBornEvent), PersonBornEvent},
                {typeof(PersonNamedEvent), PersonNamedEvent},
                {typeof(PersonStartedEducationEvent), PersonStartedEducationEvent},
                {typeof(PersonFinishedEducationEvent), PersonFinishedEducationEvent},
                {typeof(PersonStartedExperienceEvent), PersonStartedExperienceEvent},
                {typeof(PersonFinishedExperienceEvent), PersonFinishedExperienceEvent}
            };
        }

        private void PersonBornEvent(Event @event)
        {
            var personBornEvent = (PersonBornEvent) @event;
            Gender = personBornEvent.Gender;
            DateOfBirth = personBornEvent.DateOfBirth;
        }

        private void PersonNamedEvent(Event @event)
        {
            var personNamedEvent = (PersonNamedEvent) @event;
            FirstName = personNamedEvent.FirstName;
            LastName = personNamedEvent.LastName;
        }

        private void PersonStartedEducationEvent(Event @event)
        {
            var personStartedEductionEvent = (PersonStartedEducationEvent) @event;
            EducationalHistory.Add(new Education
            {
                InstitutionName = personStartedEductionEvent.InstitutionName,
                StartDate = personStartedEductionEvent.When
            });
        }

        private void PersonFinishedEducationEvent(Event @event)
        {
            var personFinishedEducationEvent = (PersonFinishedEducationEvent) @event;
            EducationalHistory
                .Single(e => e.InstitutionName == personFinishedEducationEvent.InstitutionName
                             && e.EndDate == null)
                .EndDate = @event.When;
        }

        private void PersonStartedExperienceEvent(Event @event)
        {
            var personStartedExperienceEvent = (PersonStartedExperienceEvent) @event;
            ExperienceHistory.Add(new Experience
            {
                InstitutionName = personStartedExperienceEvent.InstitutionName,
                Title = personStartedExperienceEvent.Title,
                StartDate = personStartedExperienceEvent.When
            });
        }

        private void PersonFinishedExperienceEvent(Event @event)
        {
            var personFinishedExperienceEvent = (PersonFinishedExperienceEvent) @event;
            ExperienceHistory
                .Single(e => e.InstitutionName == personFinishedExperienceEvent.InstitutionName
                             && e.Title == personFinishedExperienceEvent.Title
                             && e.EndDate == null)
                .EndDate = personFinishedExperienceEvent.When;
        }
    }
}