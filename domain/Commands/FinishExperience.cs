using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class FinishExperience : VersionedCommand
    {
        public FinishExperience(DateTime when, ulong version, string institutionName, string title)
            : base(when, version)
        {
            InstitutionName = institutionName;
            Title = title;
        }

        public string InstitutionName { get; private set; }
        public string Title { get; private set; }
    }
}