using System;
using System.Collections.Generic;
using domain.Infrastructure;

namespace domain
{
    public partial class Person : VersionedEventSourced
    {
        public Gender Gender { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<Education> EducationalHistory { get; } = new List<Education>();
        public List<Experience> ExperienceHistory { get; } = new List<Experience>();
    }
}