﻿using System;

namespace domain.Events
{
    public class PersonStartedEducationEvent : VersionedEvent
    {
        public PersonStartedEducationEvent(string institutionName, DateTime when, ulong version) : base(when, version)
        {
            InstitutionName = institutionName;
        }

        public string InstitutionName { get; private set; }
    }
}