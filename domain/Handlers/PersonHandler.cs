using System.Linq;
using domain.Events;

namespace domain.Handlers
{
    public class PersonHandler
    {
        public Person Person { get; private set; }

        public void Handle(PersonBornEvent @event)
        {
            Person = new Person
            {
                Gender = @event.Gender,
                DateOfBirth = @event.DateOfBirth
            };
        }

        public void Handle(PersonNamedEvent @event)
        {
            Person.FirstName = @event.FirstName;
            Person.LastName = @event.LastName;
        }

        public void Handle(PersonStartedEductionEvent @event)
        {
            Person.EducationalHistory.Add(new Education
            {
                InstitutionName = @event.InstitutionName,
                StartDate = @event.When
            });
        }

        public void Handle(PersonFinishedEducationEvent @event)
        {
            Person.EducationalHistory
                .Single(e => e.InstitutionName == @event.InstitutionName && e.EndDate == null)
                .EndDate = @event.When;
        }

        public void Handle(PersonStartedExperienceEvent @event)
        {
            Person.ExperienceHistory.Add(new Experience
            {
                InstitutionName = @event.InstitutionName,
                Title = @event.Title,
                StartDate = @event.When
            });
        }

        public void Handle(PersonFinishedExperienceEvent @event)
        {
            Person.ExperienceHistory
                .Single(e => e.InstitutionName == @event.InstitutionName && e.Title == @event.Title && e.EndDate == null)
                .EndDate = @event.When;
        }
    }
}