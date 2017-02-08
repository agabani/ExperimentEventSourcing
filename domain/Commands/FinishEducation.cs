using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class FinishEducation : VersionedCommand
    {
        public FinishEducation(Guid eventSourcedId, DateTime when, ulong version, string institutionName)
            : base(eventSourcedId, when, version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}