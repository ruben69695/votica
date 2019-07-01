using System;
using System.Threading.Tasks;
using Votica.Data.Infrastructure;
using Votica.Domain;

namespace Votica.Data.IntegrationTests.Utils
{
    public static class DataAccessHelpers
    {
        public static async Task<Poll> AddPollAsync(IRepositoryBase<Poll> repo, string name, string description, DateTimeOffset expirationDate)
        {
            var poll = new Poll
            {
                Name = name,
                Description = description,
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = expirationDate
            };

            return await repo.AddAsync(poll);
        }

        public static async Task<QuestionType> AddQuestionTypeAsync(IRepositoryBase<QuestionType> repo, string name)
        {
            var qType = new QuestionType {
                Name = name
            };

            return await repo.AddAsync(qType);
        }

        public static async Task<Question> AddQuestionAsync(IRepositoryBase<Question> repo, string questionName, QuestionType questionType, Poll poll)
        {
            var question = new Question {
                Name = questionName,
                Type = questionType,
                Poll = poll
            };

            return await repo.AddAsync(question);
        }
    }
}