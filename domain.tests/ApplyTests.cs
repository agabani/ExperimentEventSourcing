using System;
using System.Linq;
using domain.Events;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class ApplyTests
    {
        [Test]
        public void Run()
        {
            var person = Person.Create();

            // Born
            person.Apply(new PersonBornEvent(person.Id, new DateTime(1990, 10, 7), 0, Gender.Male));
            Assert.That(person.Gender, Is.EqualTo(Gender.Male));
            Assert.That(person.DateOfBirth, Is.EqualTo(new DateTime(1990, 10, 7)));

            // Named
            person.Apply(new PersonNamedEvent(person.Id, new DateTime(1990, 10, 10), 1, "Ahmed", "Agabani"));
            Assert.That(person.FirstName, Is.EqualTo("Ahmed"));
            Assert.That(person.LastName, Is.EqualTo("Agabani"));

            // Nursary
            person.Apply(new PersonStartedEducationEvent(person.Id, new DateTime(1993, 9, 6), 2, "Evan Davis Nursary"));
            var education = person.EducationalHistory.Single(e => e.InstitutionName == "Evan Davis Nursary");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1993, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            person.Apply(new PersonFinishedEducationEvent(person.Id, new DateTime(1995, 7, 31), 3, "Evan Davis Nursary"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Evan Davis Nursary");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1993, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(1995, 7, 31)));

            // Primary School
            person.Apply(new PersonStartedEducationEvent(person.Id, new DateTime(1995, 9, 6), 4, "Harlesden Primary School"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Harlesden Primary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1995, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            person.Apply(new PersonFinishedEducationEvent(person.Id, new DateTime(2002, 7, 31), 5, "Harlesden Primary School"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Harlesden Primary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1995, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2002, 7, 31)));

            // Secondary School
            person.Apply(new PersonStartedEducationEvent(person.Id, new DateTime(2002, 9, 6), 6, "Preston Manor Secondary School"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor Secondary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2002, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            person.Apply(new PersonStartedExperienceEvent(person.Id, new DateTime(2006, 04, 01), 7, "Cancer Black Care", "Receptionist"));
            var experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Cancer Black Care" && e.Title == "Receptionist");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2006, 04, 01)));
            Assert.That(experience.EndDate, Is.Null);

            person.Apply(new PersonFinishedExperienceEvent(person.Id, new DateTime(2006, 04, 18), 8, "Cancer Black Care", "Receptionist"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Cancer Black Care" && e.Title == "Receptionist");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2006, 04, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2006, 04, 18)));

            person.Apply(new PersonFinishedEducationEvent(person.Id, new DateTime(2007, 7, 31), 9, "Preston Manor Secondary School"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor Secondary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2002, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2007, 7, 31)));

            // 6th Form
            person.Apply(new PersonStartedEducationEvent(person.Id, new DateTime(2007, 9, 6), 10, "Preston Manor 6th Form"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor 6th Form");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2007, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            person.Apply(new PersonFinishedEducationEvent(person.Id, new DateTime(2009, 7, 31), 11, "Preston Manor 6th Form"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor 6th Form");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2007, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2009, 7, 31)));

            // University
            person.Apply(new PersonStartedEducationEvent(person.Id, new DateTime(2009, 9, 6), 12, "University of Bristol"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "University of Bristol");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2009, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            person.Apply(new PersonStartedExperienceEvent(person.Id, new DateTime(2012, 07, 01), 13, "West One Food Ltd.", "Crew Member"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "West One Food Ltd." && e.Title == "Crew Member");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2012, 07, 01)));
            Assert.That(experience.EndDate, Is.Null);

            person.Apply(new PersonFinishedExperienceEvent(person.Id, new DateTime(2012, 09, 30), 14, "West One Food Ltd.", "Crew Member"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "West One Food Ltd." && e.Title == "Crew Member");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2012, 07, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2012, 09, 30)));

            person.Apply(new PersonFinishedEducationEvent(person.Id, new DateTime(2013, 7, 31), 15, "University of Bristol"));
            education = person.EducationalHistory.Single(e => e.InstitutionName == "University of Bristol");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2009, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2013, 7, 31)));

            // WorldRemit
            person.Apply(new PersonStartedExperienceEvent(person.Id, new DateTime(2014, 06, 30), 16, "WorldRemit", "Junior Back-End Developer"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Junior Back-End Developer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2014, 06, 30)));
            Assert.That(experience.EndDate, Is.Null);

            person.Apply(new PersonFinishedExperienceEvent(person.Id, new DateTime(2015, 09, 01), 17, "WorldRemit", "Junior Back-End Developer"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Junior Back-End Developer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2014, 06, 30)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2015, 09, 01)));

            person.Apply(new PersonStartedExperienceEvent(person.Id, new DateTime(2015, 09, 02), 18, "WorldRemit", "Software Engineer"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2015, 09, 02)));
            Assert.That(experience.EndDate, Is.Null);

            person.Apply(new PersonFinishedExperienceEvent(person.Id, new DateTime(2016, 07, 22), 19, "WorldRemit", "Software Engineer"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2015, 09, 02)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2016, 07, 22)));
            
            // Capital One
            person.Apply(new PersonStartedExperienceEvent(person.Id, new DateTime(2016, 07, 25), 20, "Capital One", "Software Engineer"));
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Capital One" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2016, 07, 25)));
            Assert.That(experience.EndDate, Is.Null);
        }
    }
}