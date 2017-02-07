using System;
using System.Linq;
using domain.Commands;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class CommandThenApplyTests
    {
        [Test]
        public void Run()
        {
            var person = new Person();

            // Born
            var versionedEvents = person.Execute(new BirthedCommand(new DateTime(1990, 10, 7), person.Version, Gender.Male));
            person.Apply(versionedEvents);
            Assert.That(person.Gender, Is.EqualTo(Gender.Male));
            Assert.That(person.DateOfBirth, Is.EqualTo(new DateTime(1990, 10, 7)));

            // Named
            versionedEvents = person.Execute(new ChangeNameCommand(new DateTime(1990, 10, 10), person.Version, "Ahmed", "Agabani"));
            person.Apply(versionedEvents);
            Assert.That(person.FirstName, Is.EqualTo("Ahmed"));
            Assert.That(person.LastName, Is.EqualTo("Agabani"));

            // Nursary
            versionedEvents = person.Execute(new StartEducation(new DateTime(1993, 9, 6), person.Version, "Evan Davis Nursary"));
            person.Apply(versionedEvents);
            var education = person.EducationalHistory.Single(e => e.InstitutionName == "Evan Davis Nursary");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1993, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            versionedEvents = person.Execute(new FinishEducation(new DateTime(1995, 7, 31), person.Version, "Evan Davis Nursary"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Evan Davis Nursary");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1993, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(1995, 7, 31)));

            // Primary School
            versionedEvents = person.Execute(new StartEducation(new DateTime(1995, 9, 6), person.Version, "Harlesden Primary School"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Harlesden Primary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1995, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            versionedEvents = person.Execute(new FinishEducation(new DateTime(2002, 7, 31), person.Version, "Harlesden Primary School"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Harlesden Primary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(1995, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2002, 7, 31)));

            // Secondary School
            versionedEvents = person.Execute(new StartEducation(new DateTime(2002, 9, 6), person.Version, "Preston Manor Secondary School"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor Secondary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2002, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            versionedEvents = person.Execute(new StartExperience(new DateTime(2006, 04, 01), person.Version, "Cancer Black Care", "Receptionist"));
            person.Apply(versionedEvents);
            var experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Cancer Black Care" && e.Title == "Receptionist");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2006, 04, 01)));
            Assert.That(experience.EndDate, Is.Null);

            versionedEvents = person.Execute(new FinishExperience(new DateTime(2006, 04, 18), person.Version, "Cancer Black Care", "Receptionist"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Cancer Black Care" && e.Title == "Receptionist");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2006, 04, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2006, 04, 18)));

            versionedEvents = person.Execute(new FinishEducation(new DateTime(2007, 7, 31), person.Version, "Preston Manor Secondary School"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor Secondary School");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2002, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2007, 7, 31)));

            // 6th Form
            versionedEvents = person.Execute(new StartEducation(new DateTime(2007, 9, 6), person.Version, "Preston Manor 6th Form"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor 6th Form");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2007, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            versionedEvents = person.Execute(new FinishEducation(new DateTime(2009, 7, 31), person.Version, "Preston Manor 6th Form"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "Preston Manor 6th Form");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2007, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2009, 7, 31)));

            // University
            versionedEvents = person.Execute(new StartEducation(new DateTime(2009, 9, 6), person.Version, "University of Bristol"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "University of Bristol");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2009, 9, 6)));
            Assert.That(education.EndDate, Is.Null);

            versionedEvents = person.Execute(new StartExperience(new DateTime(2012, 07, 01), person.Version, "West One Food Ltd.", "Crew Member"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "West One Food Ltd." && e.Title == "Crew Member");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2012, 07, 01)));
            Assert.That(experience.EndDate, Is.Null);

            versionedEvents = person.Execute(new FinishExperience(new DateTime(2012, 09, 30), person.Version, "West One Food Ltd.", "Crew Member"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "West One Food Ltd." && e.Title == "Crew Member");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2012, 07, 01)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2012, 09, 30)));

            versionedEvents = person.Execute(new FinishEducation(new DateTime(2013, 7, 31), person.Version, "University of Bristol"));
            person.Apply(versionedEvents);
            education = person.EducationalHistory.Single(e => e.InstitutionName == "University of Bristol");
            Assert.That(education.StartDate, Is.EqualTo(new DateTime(2009, 9, 6)));
            Assert.That(education.EndDate, Is.EqualTo(new DateTime(2013, 7, 31)));

            // WorldRemit
            versionedEvents = person.Execute(new StartExperience(new DateTime(2014, 06, 30), person.Version, "WorldRemit", "Junior Back-End Developer"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Junior Back-End Developer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2014, 06, 30)));
            Assert.That(experience.EndDate, Is.Null);

            versionedEvents = person.Execute(new FinishExperience(new DateTime(2015, 09, 01), person.Version, "WorldRemit", "Junior Back-End Developer"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Junior Back-End Developer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2014, 06, 30)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2015, 09, 01)));

            versionedEvents = person.Execute(new StartExperience(new DateTime(2015, 09, 02), person.Version, "WorldRemit", "Software Engineer"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2015, 09, 02)));
            Assert.That(experience.EndDate, Is.Null);

            versionedEvents = person.Execute(new FinishExperience(new DateTime(2016, 07, 22), person.Version, "WorldRemit", "Software Engineer"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "WorldRemit" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2015, 09, 02)));
            Assert.That(experience.EndDate, Is.EqualTo(new DateTime(2016, 07, 22)));

            // Capital One
            versionedEvents = person.Execute(new StartExperience(new DateTime(2016, 07, 25), person.Version, "Capital One", "Software Engineer"));
            person.Apply(versionedEvents);
            experience = person.ExperienceHistory.Single(e => e.InstitutionName == "Capital One" && e.Title == "Software Engineer");
            Assert.That(experience.StartDate, Is.EqualTo(new DateTime(2016, 07, 25)));
            Assert.That(experience.EndDate, Is.Null);
        }
    }
}