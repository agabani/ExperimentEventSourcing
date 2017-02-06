using System;
using System.Collections.Generic;
using System.Linq;
using domain.Events;

namespace domain
{
    public partial class Person
    {
        public Gender Gender { set; get; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Education> EducationalHistory { get; set; } = new List<Education>();
        public List<Experience> ExperienceHistory { get; set; } = new List<Experience>();
    }

    public partial class Person
    {
        private static readonly IReadOnlyDictionary<Type, Action<Event, Person>> Actions;

        static Person()
        {
            Actions = new Dictionary<Type, Action<Event, Person>>
            {
                {typeof(PersonBornEvent), PersonBornEvent},
                {typeof(PersonNamedEvent), PersonNamedEvent},
                {typeof(PersonStartedEductionEvent), PersonStartedEducationEvent},
                {typeof(PersonFinishedEducationEvent), PersonFinishedEducationEvent},
                {typeof(PersonStartedExperienceEvent), PersonStartedExperienceEvent},
                {typeof(PersonFinishedExperienceEvent), PersonFinishedExperienceEvent}
            };
        }

        private static void PersonBornEvent(Event @event, Person person)
        {
            var personBornEvent = (PersonBornEvent) @event;
            person.Gender = personBornEvent.Gender;
            person.DateOfBirth = personBornEvent.DateOfBirth;
        }

        private static void PersonNamedEvent(Event @event, Person person)
        {
            var personNamedEvent = (PersonNamedEvent) @event;
            person.FirstName = personNamedEvent.FirstName;
            person.LastName = personNamedEvent.LastName;
        }

        private static void PersonStartedEducationEvent(Event @event, Person person)
        {
            var personStartedEductionEvent = (PersonStartedEductionEvent) @event;
            person.EducationalHistory.Add(new Education
            {
                InstitutionName = personStartedEductionEvent.InstitutionName,
                StartDate = personStartedEductionEvent.When
            });
        }

        private static void PersonFinishedEducationEvent(Event @event, Person person)
        {
            var personFinishedEducationEvent = (PersonFinishedEducationEvent) @event;
            person.EducationalHistory
                .Single(e => e.InstitutionName == personFinishedEducationEvent.InstitutionName
                             && e.EndDate == null)
                .EndDate = @event.When;
        }

        private static void PersonStartedExperienceEvent(Event @event, Person person)
        {
            var personStartedExperienceEvent = (PersonStartedExperienceEvent) @event;
            person.ExperienceHistory.Add(new Experience
            {
                InstitutionName = personStartedExperienceEvent.InstitutionName,
                Title = personStartedExperienceEvent.Title,
                StartDate = personStartedExperienceEvent.When
            });
        }

        private static void PersonFinishedExperienceEvent(Event @event, Person person)
        {
            var personFinishedExperienceEvent = (PersonFinishedExperienceEvent) @event;
            person.ExperienceHistory
                .Single(e => e.InstitutionName == personFinishedExperienceEvent.InstitutionName
                             && e.Title == personFinishedExperienceEvent.Title
                             && e.EndDate == null)
                .EndDate = personFinishedExperienceEvent.When;
        }

        public void Apply(Event @event)
        {
            Actions[@event.GetType()].Invoke(@event, this);
        }

        public static Person LoadFrom(List<Event> events)
        {
            var person = new Person();
            foreach (var @event in events)
            {
                person.Apply(@event);
            }
            return person;
        }
    }
}