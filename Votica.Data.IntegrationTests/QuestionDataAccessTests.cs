using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Votica.Data.Infrastructure;
using Votica.Domain;
using System.Threading.Tasks;

namespace Votica.Data.IntegrationTests
{
    [TestFixture]
    public class QuestionDataAccessTests
    {
        private UnitOfWork _uow;
        private IRepositoryBase<Poll> _pollRepo;
        private IRepositoryBase<Question> _questionRepo;
        private IRepositoryBase<QuestionType> _questionType;

        [SetUp]
        public void Setup()
        {
            _uow = new UnitOfWork(PrepareTestingLab.TestContext);
            _pollRepo = _uow.GetRepository<Poll>();
            _questionRepo = _uow.GetRepository<Question>();
            _questionType = _uow.GetRepository<QuestionType>();
        }

        [Test]
        public async Task AddAsync_AddNewQuestionToPoll_ShouldInsertQuestionIntoDatabase()
        {
            var poll = await AddPollAsync("Event 1", "Short description", DateTimeOffset.Now.AddDays(5.0));
            var qType = await AddQuestionTypeAsync("Simple");
            var question = new Question {
                Name = "Do you want to hang out this afternoon?",
                Type = qType,
                Poll = poll
            };

            var insertedQuestion = await _questionRepo.AddAsync(question);

            Assert.That(insertedQuestion.Name, Is.EqualTo(question.Name));
        }

        [Test]
        public async Task FindByAsync_FindItemByExpression_ShouldReturnTheCorrectItem()
        {
            var poll = await AddPollAsync("Event 2", "Short description", DateTimeOffset.Now.AddDays(6.0));
            var qType = await AddQuestionTypeAsync("Simple");
            var question = new Question {
                Name = "Do you want to come to my pool tonight?",
                Type = qType,
                Poll = poll
            };
            var insertedQuestion = await _questionRepo.AddAsync(question);

            var questionFound = await _questionRepo.FindByAsync(q => q.Id == insertedQuestion.Id);

            Assert.That(questionFound.Name, Is.EqualTo(insertedQuestion.Name));
        }

        [Test]
        public async Task FindWhere_FindQuestionsByExpression_ShouldFindTheCorrectQuestions()
        {
            var poll = await AddPollAsync("Summer event 2019", "Short description", DateTimeOffset.Now.AddDays(6.0));
            var qType = await AddQuestionTypeAsync("Simple");
            var questionOne = new Question {
                Name = "What do you want to do on Saint John the Baptist Day?",
                Type = qType,
                Poll = poll
            };
            var questionTwo = new Question {
                Name = "What do you want to do for your birthday?",
                Type = qType,
                Poll = poll
            };
            await _questionRepo.AddAsync(questionOne);
            await _questionRepo.AddAsync(questionTwo);

            var questionsFound = await _questionRepo.FindWhere(q => q.Poll.Id == poll.Id).ToListAsync();

            Assert.That(questionsFound.Count, Is.GreaterThan(1));
        }

        [Test]
        public async Task FindByIdAsync_FindById_ShouldReturnTheCorrectItem()
        {
            var poll = await AddPollAsync("Summer event 2020", "Short description", DateTimeOffset.Now.AddDays(6.0));
            var qType = await AddQuestionTypeAsync("Simple");
            var questionOne = new Question {
                Name = "What do you want to do on Saint John the Baptist Day?",
                Type = qType,
                Poll = poll
            };
            var questionAdded = await _questionRepo.AddAsync(questionOne);

            var questionFound = await _questionRepo.GetByIdAsync(questionAdded.Id);

            Assert.That(questionFound, Is.Not.Null);
        }

        [Test]
        public async Task RemoveAsync_RemoveQuestionFromPoll_ShouldRemoveIt()
        {
            var poll = await AddPollAsync("Summer event 2021", "Short description", DateTimeOffset.Now.AddDays(6.0));
            var qType = await AddQuestionTypeAsync("Simple");
            var questionOne = new Question {
                Name = "What do you want to do on Saint John the Baptist Day?",
                Type = qType,
                Poll = poll
            };
            var questionAdded = await _questionRepo.AddAsync(questionOne);

            await _questionRepo.RemoveAsync(questionAdded, questionAdded.Id);
            var questionFound = await _questionRepo.GetByIdAsync(questionAdded.Id);

            Assert.That(questionFound, Is.Null);
        }

        [Test]
        public async Task UpdateAync_UpdateQuestion_ShouldUpdateIt()
        {
            var poll = await AddPollAsync("Summer event 2022", "Short description", DateTimeOffset.Now.AddDays(6.0));
            var qType = await AddQuestionTypeAsync("Simple");
            var questionOne = new Question {
                Name = "What do you want to do on Saint John the Baptist Day?",
                Type = qType,
                Poll = poll
            };
            var questionAdded = await _questionRepo.AddAsync(questionOne);
            
            questionAdded.Name = "Fake Name";
            var questionResult = await _questionRepo.UpdateAsync(questionAdded, questionAdded.Id);

            Assert.That(questionResult.Name, Is.EqualTo("Fake Name"));
        }

        private async Task<Poll> AddPollAsync(string name, string description, DateTimeOffset expirationDate)
        {
            var poll = new Poll
            {
                Name = name,
                Description = description,
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = expirationDate
            };

            return await _pollRepo.AddAsync(poll);
        }

        private async Task<QuestionType> AddQuestionTypeAsync(string name)
        {
            var qType = new QuestionType {
                Name = name
            };

            return await _questionType.AddAsync(qType);
        }

    }
}