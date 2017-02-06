using System;
using System.Collections.Generic;
using System.Linq;
using domain.Events;

namespace domain
{
    public class Person
    {
        private static readonly IReadOnlyDictionary<Type, Action<Event, Person>> Actions;

        static Person()
        {
            Actions = new Dictionary<Type, Action<Event, Person>>
            {
                {
                    typeof(PersonBornEvent), (@event, person) =>
                    {
                        var personBornEvent = (PersonBornEvent) @event;
                        person.Gender = personBornEvent.Gender;
                        person.DateOfBirth = personBornEvent.DateOfBirth;
                    }
                },
                {
                    typeof(PersonNamedEvent), (@event, person) =>
                    {
                        var personNamedEvent = (PersonNamedEvent) @event;
                        person.FirstName = personNamedEvent.FirstName;
                        person.LastName = personNamedEvent.LastName;
                    }
                },
                {
                    typeof(PersonStartedEductionEvent), (@event, person) =>
                    {
                        var personStartedEductionEvent = (PersonStartedEductionEvent) @event;
                        person.EducationalHistory.Add(new Education
                        {
                            InstitutionName = personStartedEductionEvent.InstitutionName,
                            StartDate = personStartedEductionEvent.When
                        });
                    }
                },
                {
                    typeof(PersonFinishedEducationEvent), (@event, person) =>
                    {
                        var personFinishedEducationEvent = (PersonFinishedEducationEvent) @event;
                        person.EducationalHistory
                            .Single(e => e.InstitutionName == personFinishedEducationEvent.InstitutionName
                                         && e.EndDate == null)
                            .EndDate = @event.When;
                    }
                },
                {
                    typeof(PersonStartedExperienceEvent), (@event, person) =>
                    {
                        var personStartedExperienceEvent = (PersonStartedExperienceEvent) @event;
                        person.ExperienceHistory.Add(new Experience
                        {
                            InstitutionName = personStartedExperienceEvent.InstitutionName,
                            Title = personStartedExperienceEvent.Title,
                            StartDate = personStartedExperienceEvent.When
                        });
                    }
                },
                {
                    typeof(PersonFinishedExperienceEvent), (@event, person) =>
                    {
                        var personFinishedExperienceEvent = (PersonFinishedExperienceEvent) @event;
                        person.ExperienceHistory
                            .Single(e => e.InstitutionName == personFinishedExperienceEvent.InstitutionName
                                         && e.Title == personFinishedExperienceEvent.Title
                                         && e.EndDate == null)
                            .EndDate = personFinishedExperienceEvent.When;
                    }
                }
            };
        }

        public Gender Gender { set; get; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Education> EducationalHistory { get; set; } = new List<Education>();
        public List<Experience> ExperienceHistory { get; set; } = new List<Experience>();

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