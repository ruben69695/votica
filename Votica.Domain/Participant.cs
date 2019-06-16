using System;
using System.Collections.Generic;
using Votica.Domain.Interfaces;

namespace Votica.Domain
{
    public class Participant
    {
        public virtual string Email { get; set; }
        public virtual IList<ParticipantOption> Options { get; set; }
    }
}