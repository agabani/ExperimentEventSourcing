using System;
using System.Collections.Generic;

namespace domain
{
    public class Person
    {
        public Gender Gender { set; get; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Education> EducationalHistory { get; set; } = new List<Education>();
        public List<Experience> ExperienceHistory { get; set; } = new List<Experience>();
    }
}