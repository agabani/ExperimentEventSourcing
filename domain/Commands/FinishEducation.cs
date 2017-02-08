using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class FinishEducation : VersionedCommand
    {
        public FinishEducation(DateTime when, ulong version, string institutionName)
            : base(when, version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}