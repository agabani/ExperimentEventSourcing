using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class StartEducation : VersionedCommand
    {
        public StartEducation(Guid eventSourcedId, DateTime when, ulong version, string institutionName)
            : base(eventSourcedId, when, version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}