using System;
using System.Collections.Generic;
using Votica.Domain.Interfaces;

namespace Votica.Domain
{
    public class ParticipantOption
    {
        public virtual string ParticipantEmail { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual int OptionId { get; set; }
        public virtual Option Option { get; set; }
    }
}