using System;
using System.Collections.Generic;
using System.Linq;
using domain.Commands;
using domain.Events;
using domain.Infrastructure;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void Run()
        {
            var id = Guid.NewGuid();

            var person = VersionedEventSourced.LoadFrom<Person>(new List<VersionedEvent>
            {
                new PersonNamedEvent(id, new DateTime(1998, 3, 20), 0, "Amjad", "Agabani")
            });

            Assert.That(person.FirstName, Is.EqualTo("Amjad"));
            Assert.That(person.LastName, Is.EqualTo("Agabani"));

            var command = new ChangeNameCommand(new DateTime(1998, 3, 24), 1, "Amjed", "Agabani");
            var @event = (PersonNamedEvent) person.Execute(command).Single();

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