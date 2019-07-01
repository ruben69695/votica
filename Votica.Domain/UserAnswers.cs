using System;
using System.Collections.Generic;
using Votica.Domain.Interfaces;

namespace Votica.Domain
{
    public class UserAnswer
    {
        public virtual string UserEmail { get; set; }
        public virtual User User { get; set; }
        public virtual int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
    }
}