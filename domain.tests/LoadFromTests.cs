using System;
using System.Collections.Generic;
using System.Linq;
using domain.Events;
using domain.Infrastructure;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class LoadFromTests
    {
        [Test]
        public void Run()
        {
            var id = Guid.NewGuid();

            var events = new List<VersionedEvent>
            {
                new PersonBornEvent(id, new DateTime(1990, 10, 7), 0, Gender.Male),
                new PersonNamedEvent(id, new DateTime(1990, 10, 10), 1, "Ahmed", "Agabani"),
                new PersonStartedEducationEvent(id, new DateTime(1993, 9, 6), 2, "Evan Davis Nursary"),
                new PersonFinishedEducationEvent(id, new DateTime(1995, 7, 31), 3, "Evan Davis Nursary"),
                new PersonStartedEducationEvent(id, new DateTime(1995, 9, 6), 4, "Harlesden Primary School"),
                new PersonFinishedEducationEvent(id, new DateTime(2002, 7, 31), 5, "Harlesden Primary School"),
                new PersonStartedEducationEvent(id, new DateTime(2002, 9, 6), 6, "Preston Manor Secondary School"),
                new PersonStartedExperienceEvent(id, new DateTime(2006, 04, 01), 7, "Cancer Black Care", "Receptionist"),
                new PersonFinishedExperienceEvent(id, new DateTime(2006, 04, 18), 8, "Cancer Black Care", "Receptionist"),
                new PersonFinishedEducationEvent(id, new DateTime(2007, 7, 31), 9, "Preston Manor Secondary School"),
                new PersonStartedEducationEvent(id, new DateTime(2007, 9, 6), 10, "Preston Manor 6th Form"),
                new PersonFinishedEducationEvent(id, new DateTime(2009, 7, 31), 11, "Preston Manor 6th Form"),
                new PersonStartedEducationEvent(id, new DateTime(2009, 9, 6), 12, "University of Bristol"),
                new PersonStartedExperienceEvent(id, new DateTime(2012, 07, 01), 13, "West One Food Ltd.", "Crew Member"),
                new PersonFinishedExperienceEvent(id, new DateTime(2012, 09, 30), 14, "West One Food Ltd.", "Crew Member"),
                new PersonFinishedEducationEvent(id, new DateTime(2013, 7, 31), 15, "University of Bristol"),
                new PersonStartedExperienceEvent(id, new DateTime(2014, 06, 30), 16, "WorldRemit", "Junior Back-End Developer"),
                new PersonFinishedExperienceEvent(id, new DateTime(2015, 09, 01), 17, "WorldRemit", "Junior Back-End Developer"),
                new PersonStartedExperienceEvent(id, new DateTime(2015, 09, 02), 18, "WorldRemit", "Software Engineer"),
                new PersonFinishedExperienceEvent(id, new DateTime(2016, 07, 22), 19, "WorldRemit", "Software Engineer"),
                new PersonStartedExperienceEvent(id, new DateTime(2016, 07, 25), 20, "Capital One", "Software Engineer")
            };

            var person = VersionedEventSourced.LoadFrom<Person>(events);

            var education = person.EducationalHistory.Single(e => e.InstitutionName == "Evan Davis Nursary");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1993, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(1995, 7, 31)));

            education = person.EducationalHistory.Single(e => e.InstitutionName == "Harlesden Primary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1995, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2002, 7, 31)));

            var experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Cancer Black Care" && e.Title == "Receptionist");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2006, 04, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2006, 04, 18)));

            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor Secondary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2002, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2007, 7, 31)));

            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor 6th Form");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2007, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2009, 7, 31)));

            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "West One Food Ltd." && e.Title == "Crew Member");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2012, 07, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2012, 09, 30)));

            education = person.EducationalHistory.Single(e => e.InstitutionName == "University of Bristol");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2009, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2013, 7, 31)));

            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Junior Back-End Developer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2014, 06, 30)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2015, 09, 01)));

            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2015, 09, 02)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2016, 07, 22)));

            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Capital One" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2016, 07, 25)));
            Assert.That(experience.EndDate, Is.Null);
        }
    }
}