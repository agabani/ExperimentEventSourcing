﻿using System;
using domain.Infrastructure;

namespace domain.Commands
{
    public class StartExperience : VersionedCommand
    {
        public StartExperience(Guid eventSourcedId, DateTime when, ulong version, string institutionName, string title)
            : base(eventSourcedId, when, version)
        {
            InstitutionName = institutionName;
            Title = title;
        }

        public string InstitutionName { get; private set; }
        public string Title { get; private set; }
    }
}