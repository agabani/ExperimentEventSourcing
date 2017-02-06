using System;
using System.Collections.Generic;
using System.Linq;
using domain.Events;
using domain.Handlers;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class LoadFromTests
    {
        [Test]
        public void Run()
        {
            var events = new List<Event>
            {
                new PersonBornEvent(Gender.Male, new DateTime(1990, 10, 7)),
                new PersonNamedEvent("Ahmed", "Agabani", new DateTime(1990, 10, 10)),
                new PersonStartedEductionEvent("Evan Davis Nursary", new DateTime(1993, 9, 6)),
                new PersonFinishedEducationEvent("Evan Davis Nursary", new DateTime(1995, 7, 31)),
                new PersonStartedEductionEvent("Harlesden Primary School", new DateTime(1995, 9, 6)),
                new PersonFinishedEducationEvent("Harlesden Primary School", new DateTime(2002, 7, 31)),
                new PersonStartedEductionEvent("Preston Manor Secondary School", new DateTime(2002, 9, 6)),
                new PersonStartedExperienceEvent("Cancer Black Care", "Receptionist", new DateTime(2006, 04, 01)),
                new PersonFinishedExperienceEvent("Cancer Black Care", "Receptionist", new DateTime(2006, 04, 18)),
                new PersonFinishedEducationEvent("Preston Manor Secondary School", new DateTime(2007, 7, 31)),
                new PersonStartedEductionEvent("Preston Manor 6th Form", new DateTime(2007, 9, 6)),
                new PersonFinishedEducationEvent("Preston Manor 6th Form", new DateTime(2009, 7, 31)),
                new PersonStartedEductionEvent("University of Bristol", new DateTime(2009, 9, 6)),
                new PersonStartedExperienceEvent("West One Food Ltd.", "Crew Member", new DateTime(2012, 07, 01)),
                new PersonFinishedExperienceEvent("West One Food Ltd.", "Crew Member", new DateTime(2012, 09, 30)),
                new PersonFinishedEducationEvent("University of Bristol", new DateTime(2013, 7, 31)),
                new PersonStartedExperienceEvent("WorldRemit", "Junior Back-End Developer", new DateTime(2014, 06, 30)),
                new PersonFinishedExperienceEvent("WorldRemit", "Junior Back-End Developer", new DateTime(2015, 09, 01)),
                new PersonStartedExperienceEvent("WorldRemit", "Software Engineer", new DateTime(2015, 09, 02)),
                new PersonFinishedExperienceEvent("WorldRemit", "Software Engineer", new DateTime(2016, 07, 22)),
                new PersonStartedExperienceEvent("Capital One", "Software Engineer", new DateTime(2016, 07, 25))
            };

            var person = Person.LoadFrom(events);

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