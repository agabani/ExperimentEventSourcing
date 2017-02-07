using System;
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

            var versionedEvents = person.Execute(new BirthedCommand(new DateTime(1990, 10, 7), person.Version, Gender.Male));
            person.Apply(versionedEvents);
        }
    }
}