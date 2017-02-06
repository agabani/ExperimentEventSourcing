using System;

namespace domain
{
    public class Experience
    {
        public string InstitutionName { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}