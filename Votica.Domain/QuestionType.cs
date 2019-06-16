using System;
using System.Collections.Generic;
using Votica.Domain.Interfaces;

namespace Votica.Domain
{
    public class QuestionType : IKeyable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Question> Questions { get; set; }
    }
}