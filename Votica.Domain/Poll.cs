using System;
using System.Collections.Generic;
using Votica.Domain.Interfaces;

namespace Votica.Domain
{
    public class Poll : IKeyable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTimeOffset CreationDate { get; set; }
        public virtual DateTimeOffset ExpirationDate { get; set; }
        public virtual IList<Question> Questions { get; set; }

    }
}
