using System.Collections.Generic;

namespace Votica.Domain
{
    public class User
    {
        public virtual string Email { get; set; }
        public virtual string Name { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
        public virtual IList<UserAnswer> QuestionAnswers { get; set; }
    }
}