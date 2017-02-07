using System;
using System.Collections.Generic;
using System.Linq;
using domain.Commands;
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
        public IEnumerable<Event> Execute(ChangeNameCommand command)
        {
            Func<DateTime, DateTime> eighteen = dateOfBirth => new DateTime(dateOfBirth.Year + 18, dateOfBirth.Month, dateOfBirth.Day);

            return DateTime.UtcNow > eighteen(DateOfBirth)
                ? (IEnumerable<Event>) new[] {new PersonNamedEvent(command.FirstName, command.LastName, command.When)}
                : new List<Event>();
        }
    }

    public partial class Person : EventSourced
    {
        private new static readonly IReadOnlyDictionary<Type, Action<Event, object>> Actions = new Dictionary<Type, Action<Event, object>>
        {
            {typeof(PersonBornEvent), PersonBornEvent},
            {typeof(PersonNamedEvent), PersonNamedEvent},
            {typeof(PersonStartedEducationEvent), PersonStartedEducationEvent},
            {typeof(PersonFinishedEducationEvent), PersonFinishedEducationEvent},
            {typeof(PersonStartedExperienceEvent), PersonStartedExperienceEvent},
            {typeof(PersonFinishedExperienceEvent), PersonFinishedExperienceEvent}
        };

        public Person()
        {
            base.Actions = Actions;
        }

        private static void PersonBornEvent(Event @event, object @object)
        {
            var person = (Person) @object;
            var personBornEvent = (PersonBornEvent) @event;
            person.Gender = personBornEvent.Gender;
            person.DateOfBirth = personBornEvent.DateOfBirth;
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