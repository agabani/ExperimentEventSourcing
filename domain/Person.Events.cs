using System;
using System.Collections.Generic;
using System.Linq;
using domain.Events;
using domain.Infrastructure;

namespace domain
{
    public partial class Person
    {
        private static readonly IReadOnlyDictionary<Type, Action<Event, object>> Actions = new Dictionary<Type, Action<Event, object>>
        {
            {typeof(PersonBornEvent), PersonBornEvent},
            {typeof(PersonNamedEvent), PersonNamedEvent},
            {typeof(PersonStartedEducationEvent), PersonStartedEducationEvent},
            {typeof(PersonFinishedEducationEvent), PersonFinishedEducationEvent},
            {typeof(PersonStartedExperienceEvent), PersonStartedExperienceEvent},
            {typeof(PersonFinishedExperienceEvent), PersonFinishedExperienceEvent}
        };

        public Person(Guid id) : base(id, Actions)
        {
        }

        public static Person Create()
        {
            return new Person(Guid.NewGuid());
        }

        private static void PersonBornEvent(Event @event, object @object)
        {
            var person = (Person) @object;
            var personBornEvent = (PersonBornEvent) @event;
            person.Gender = personBornEvent.Gender;
            person.DateOfBirth = personBornEvent.When;
        }

        private static void PersonNamedEvent(Event @event, object @object)
        {
            var person = (Person) @object;
            var personNamedEvent = (PersonNamedEvent) @event;
            person.FirstName = personNamedEvent.FirstName;
            person.LastName = personNamedEvent.LastName;
        }

        private static void PersonStartedEducationEvent(Event @event, object @object)
        {
            var person = (Person) @object;
            var personStartedEductionEvent = (PersonStartedEducationEvent) @event;
            person.EducationalHistory.Add(new Education
            {
                InstitutionName = personStartedEductionEvent.InstitutionName,
                StartDate = personStartedEductionEvent.When
            });
        }

        private static void PersonFinishedEducationEvent(Event @event, object @object)
        {
            var person = (Person) @object;
            var personFinishedEducationEvent = (PersonFinishedEducationEvent) @event;
            person.EducationalHistory
                .Single(e => e.InstitutionName == personFinishedEducationEvent.InstitutionName
                             && e.EndDate == null)
                .EndDate = @event.When;
        }

        private static void PersonStartedExperienceEvent(Event @event, object @object)
        {
            var person = (Person) @object;
            var personStartedExperienceEvent = (PersonStartedExperienceEvent) @event;
            person.ExperienceHistory.Add(new Experience
            {
                InstitutionName = personStartedExperienceEvent.InstitutionName,
                Title = personStartedExperienceEvent.Title,
                StartDate = personStartedExperienceEvent.When
            });
        }

        private static void PersonFinishedExperienceEvent(Event @event, object @object)
        {
            var person = (Person) @object;
            var personFinishedExperienceEvent = (PersonFinishedExperienceEvent) @event;
            person.ExperienceHistory
                .Single(e => e.InstitutionName == personFinishedExperienceEvent.InstitutionName
                             && e.Title == personFinishedExperienceEvent.Title
                             && e.EndDate == null)
                .EndDate = personFinishedExperienceEvent.When;
        }
    }
}