using System;
using System.Collections.Generic;
using domain.Events;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void Run()
        {
            var person = Person.LoadFrom(new List<Event>
            {
                new PersonNamedEvent("Amjad", "Agabani", new DateTime(1998, 3, 20))
            });

            Assert.That(person.FirstName, Is.EqualTo("Amjad"));
            Assert.That(person.LastName, Is.EqualTo("Agabani"));

            var @event = (PersonNamedEvent) person.ChangeName("Amjed", "Agabani", new DateTime(1998, 3, 24));

            Assert.That(@event.FirstName, Is.EqualTo("Amjed"));
            Assert.That(@event.LastName, Is.EqualTo("Agabani"));
            Assert.That(person.FirstName, Is.EqualTo("Amjad"));
            Assert.That(person.LastName, Is.EqualTo("Agabani"));

            person.Apply(@event);
            Assert.That(person.FirstName, Is.EqualTo("Amjed"));
            Assert.That(person.LastName, Is.EqualTo("Agabani"));
        }
    }
}