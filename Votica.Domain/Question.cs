using System;
using System.Collections.Generic;
using Votica.Domain.Interfaces;

namespace Votica.Domain
{
    public class Question : IKeyable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual QuestionType Type { get; set; }
        public virtual Poll Poll { get; set; }
        public virtual IList<Answer> Answers { get; set; }
    }
}