using System;
using System.Collections.Generic;
using Votica.Domain.Interfaces;

namespace Votica.Domain
{
    public class Option : IKeyable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Question Question { get; set; }
        public virtual IList<ParticipantOption> Participants{ get; set; }
    }
}