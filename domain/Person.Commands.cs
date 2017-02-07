using System;
using System.Collections.Generic;
using System.Linq;
using domain.Commands;
using domain.Events;
using domain.Infrastructure;

namespace domain
{
    public partial class Person
    {
        public IEnumerable<VersionedEvent> Execute(BirthedCommand command)
        {
            ValidateVersionFor(this, command);

            return new[] {new PersonBornEvent(DateOfBirth, Version, Gender)};
        }

        public IEnumerable<VersionedEvent> Execute(ChangeNameCommand command)
        {
            ValidateVersionFor(this, command);

            Func<DateTime, DateTime> eighteen = dateOfBirth => new DateTime(dateOfBirth.Year + 18, dateOfBirth.Month, dateOfBirth.Day);

            var their18ThBirthDay = eighteen(DateOfBirth);

            if (DateTime.UtcNow < their18ThBirthDay)
            {
                throw new InvalidOperationException($"Person must wait until {their18ThBirthDay} to change their name.");
            }

            return new[] {new PersonNamedEvent(command.When, Version + 1, command.FirstName, command.LastName)};
        }

        public IEnumerable<VersionedEvent> Execute(StartEducation command)
        {
            ValidateVersionFor(this, command);

            if (EducationalHistory.Any(e => e.InstitutionName == command.InstitutionName
                                            && e.EndDate == null))
            {
                throw new InvalidOperationException($"Person cannot start new educational experience at \"{command.InstitutionName}\" until they have completed their current experience at \"{command.InstitutionName}\".");
            }

            return new[] {new PersonStartedEducationEvent(command.When, Version, command.InstitutionName)};
        }

        public IEnumerable<VersionedEvent> Execute(FinishEducation command)
        {
            ValidateVersionFor(this, command);

            var education = EducationalHistory
                .SingleOrDefault(e => e.InstitutionName == command.InstitutionName
                                      && e.EndDate == null);

            if (education == null)
            {
                throw new InvalidOperationException($"Person cannot finish their educational experience at \"{command.InstitutionName}\" because they are not currently attending at \"{command.InstitutionName}\".");
            }

            return new[] {new PersonFinishedEducationEvent(command.When, Version, command.InstitutionName)};
        }

        public IEnumerable<VersionedEvent> Execute(StartExperience command)
        {
            ValidateVersionFor(this, command);

            var experience = ExperienceHistory.SingleOrDefault(e => e.InstitutionName == command.InstitutionName && e.EndDate == null);

            if (experience != null)
            {
                throw new InvalidOperationException($"Person cannot start a new experience at \"{command.InstitutionName}\" until they have completed their experience at \"{experience.InstitutionName}\" as \"{experience.Title}\".");
            }

            return new[] {new PersonStartedExperienceEvent(command.When, Version, command.InstitutionName, command.Title)};
        }

        public IEnumerable<VersionedEvent> Execute(FinishExperience command)
        {
            ValidateVersionFor(this, command);

            var experience = ExperienceHistory
                .SingleOrDefault(e => e.InstitutionName == command.InstitutionName
                                      && e.Title == command.Title
                                      && e.EndDate == null);

            if (experience == null)
            {
                throw new InvalidOperationException($"Person cannot complete their experience at \"{command.InstitutionName}\" as \"{command.Title}\" because they are not currently undergoing an experience at \"{command.InstitutionName}\" as \"{command.Title}\".");
            }

            return new[] {new PersonFinishedExperienceEvent(command.When, Version, command.InstitutionName, command.Title)};
        }
    }
}