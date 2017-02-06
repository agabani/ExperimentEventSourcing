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
        public void Apply(Event @event)
        {
            Actions[@event.GetType()].Invoke(this, @event);
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

    public partial class Person
    {
        private static readonly IReadOnlyDictionary<Type, Action<Person, Event>> Actions;

        static Person()
        {
            Actions = new Dictionary<Type, Action<Person, Event>>
            {
                {typeof(PersonBornEvent), PersonBornEvent},
                {typeof(PersonNamedEvent), PersonNamedEvent},
                {typeof(PersonStartedEductionEvent), PersonStartedEducationEvent},
                {typeof(PersonFinishedEducationEvent), PersonFinishedEducationEvent},
                {typeof(PersonStartedExperienceEvent), PersonStartedExperienceEvent},
                {typeof(PersonFinishedExperienceEvent), PersonFinishedExperienceEvent}
            };
        }

        private static void PersonBornEvent(Person person, Event @event)
        {
            var personBornEvent = (PersonBornEvent) @event;
            person.Gender = personBornEvent.Gender;
            person.DateOfBirth = personBornEvent.DateOfBirth;
        }

        private static void PersonNamedEvent(Person person, Event @event)
        {
            var personNamedEvent = (PersonNamedEvent) @event;
            person.FirstName = personNamedEvent.FirstName;
            person.LastName = personNamedEvent.LastName;
        }

        private static void PersonStartedEducationEvent(Person person, Event @event)
        {
            var personStartedEductionEvent = (PersonStartedEductionEvent) @event;
            person.EducationalHistory.Add(new Education
            {
                InstitutionName = personStartedEductionEvent.InstitutionName,
                StartDate = personStartedEductionEvent.When
            });
        }

        private static void PersonFinishedEducationEvent(Person person, Event @event)
        {
            var personFinishedEducationEvent = (PersonFinishedEducationEvent) @event;
            person.EducationalHistory
                .Single(e => e.InstitutionName == personFinishedEducationEvent.InstitutionName
                             && e.EndDate == null)
                .EndDate = @event.When;
        }

        private static void PersonStartedExperienceEvent(Person person, Event @event)
        {
            var personStartedExperienceEvent = (PersonStartedExperienceEvent) @event;
            person.ExperienceHistory.Add(new Experience
            {
                InstitutionName = personStartedExperienceEvent.InstitutionName,
                Title = personStartedExperienceEvent.Title,
                StartDate = personStartedExperienceEvent.When
            });
        }

        private static void PersonFinishedExperienceEvent(Person person, Event @event)
        {
            var personFinishedExperienceEvent = (PersonFinishedExperienceEvent) @event;
            person.ExperienceHistory
                .Single(e => e.InstitutionName == personFinishedExperienceEvent.InstitutionName
                             && e.Title == personFinishedExperienceEvent.Title
                             && e.EndDate == null)
                .EndDate = personFinishedExperienceEvent.When;
        }
    }
}