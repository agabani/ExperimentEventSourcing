using System;
using System.Linq;
using domain.Events;
using domain.Handlers;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class ApplyTests
    {
        [Test]
        public void Run()
        {
            var handler = new PersonHandler();

            // Born
            handler.Apply(new PersonBornEvent(Gender.Male, new DateTime(1990, 10, 7)));
            var person = handler.Person;
            Assert.That(person.Gender, Is.EqualTo(Gender.Male));
            Assert.That(person.DateOfBirth, Is.EqualTo(new DateTime(1990, 10, 7)));

            // Named
            handler.Apply(new PersonNamedEvent("Ahmed", "Agabani", new DateTime(1990, 10, 10)));
            Assert.That(person.FirstName, Is.EqualTo("Ahmed"));
            Assert.That(person.LastName, Is.EqualTo("Agabani"));

            // Nursary
            handler.Apply(new PersonStartedEductionEvent("Evan Davis Nursary", new DateTime(1993, 9, 6)));
            var education = person.EducationalHistory.Single(e => e.InstitutionName == "Evan Davis Nursary");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1993, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            handler.Apply(new PersonFinishedEducationEvent("Evan Davis Nursary", new DateTime(1995, 7, 31)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Evan Davis Nursary");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1993, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(1995, 7, 31)));

            // Primary School
            handler.Apply(new PersonStartedEductionEvent("Harlesden Primary School", new DateTime(1995, 9, 6)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Harlesden Primary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1995, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            handler.Apply(new PersonFinishedEducationEvent("Harlesden Primary School", new DateTime(2002, 7, 31)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Harlesden Primary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1995, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2002, 7, 31)));

            // Secondary School
            handler.Apply(new PersonStartedEductionEvent("Preston Manor Secondary School", new DateTime(2002, 9, 6)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor Secondary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2002, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            handler.Apply(new PersonStartedExperienceEvent("Cancer Black Care", "Receptionist", new DateTime(2006, 04, 01)));
            var experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Cancer Black Care" && e.Title == "Receptionist");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2006, 04, 01)));
            Assert.That(experience.EndDate, Is.Null);

            handler.Apply(new PersonFinishedExperienceEvent("Cancer Black Care", "Receptionist", new DateTime(2006, 04, 18)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Cancer Black Care" && e.Title == "Receptionist");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2006, 04, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2006, 04, 18)));

            handler.Apply(new PersonFinishedEducationEvent("Preston Manor Secondary School", new DateTime(2007, 7, 31)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor Secondary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2002, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2007, 7, 31)));

            // 6th Form
            handler.Apply(new PersonStartedEductionEvent("Preston Manor 6th Form", new DateTime(2007, 9, 6)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor 6th Form");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2007, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            handler.Apply(new PersonFinishedEducationEvent("Preston Manor 6th Form", new DateTime(2009, 7, 31)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor 6th Form");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2007, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2009, 7, 31)));

            // University
            handler.Apply(new PersonStartedEductionEvent("University of Bristol", new DateTime(2009, 9, 6)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "University of Bristol");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2009, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            handler.Apply(new PersonStartedExperienceEvent("West One Food Ltd.", "Crew Member", new DateTime(2012, 07, 01)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "West One Food Ltd." && e.Title == "Crew Member");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2012, 07, 01)));
            Assert.That(experience.EndDate, Is.Null);

            handler.Apply(new PersonFinishedExperienceEvent("West One Food Ltd.", "Crew Member", new DateTime(2012, 09, 30)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "West One Food Ltd." && e.Title == "Crew Member");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2012, 07, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2012, 09, 30)));

            handler.Apply(new PersonFinishedEducationEvent("University of Bristol", new DateTime(2013, 7, 31)));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "University of Bristol");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2009, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2013, 7, 31)));

            // WorldRemit
            handler.Apply(new PersonStartedExperienceEvent("WorldRemit", "Junior Back-End Developer", new DateTime(2014, 06, 30)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Junior Back-End Developer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2014, 06, 30)));
            Assert.That(experience.EndDate, Is.Null);

            handler.Apply(new PersonFinishedExperienceEvent("WorldRemit", "Junior Back-End Developer", new DateTime(2015, 09, 01)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Junior Back-End Developer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2014, 06, 30)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2015, 09, 01)));

            handler.Apply(new PersonStartedExperienceEvent("WorldRemit", "Software Engineer", new DateTime(2015, 09, 02)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2015, 09, 02)));
            Assert.That(experience.EndDate, Is.Null);

            // Capital One
            handler.Apply(new PersonFinishedExperienceEvent("WorldRemit", "Software Engineer", new DateTime(2016, 07, 22)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2015, 09, 02)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2016, 07, 22)));

            handler.Apply(new PersonStartedExperienceEvent("Capital One", "Software Engineer", new DateTime(2016, 07, 25)));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Capital One" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2016, 07, 25)));
            Assert.That(experience.EndDate, Is.Null);
        }
    }
}